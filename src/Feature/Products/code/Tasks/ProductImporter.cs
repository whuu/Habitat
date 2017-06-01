namespace Sitecore.Feature.Products.Tasks
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using Data;
    using Data.Items;
    using Diagnostics;
    using SecurityModel;
    using Sitecore.Tasks;
    using ContentSearch;
    using ContentSearch.Maintenance;
    using ContentSearch.SearchTypes;
    using ContentSearch.Linq.Utilities;
    using Services;
    using Models;
    using Data.Fields;
    using Buckets.Managers;

    public class ProductImporter
    {
        public void Execute(Item[] items, CommandItem command, ScheduleItem schedule)
        {
            if (items == null || items.Length == 0)
            {
                Log.Warn($"{schedule.InnerItem.Paths.Path} tried to run without selected item", this);
                return;
            }

            using (new DatabaseSwitcher(Configuration.Factory.GetDatabase("master")))
            {
                using (new ContextItemSwitcher(items[0]))
                {
                    RunImporter();
                }
            } 
        }

        protected void RunImporter()
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            var repositoryRoot = Context.Item;
            Log.Info($"Product importer started in {repositoryRoot.Name}", this);
            var changedItems = new List<Item>();

            try
            {
                var existingProducts = SearchBucket(repositoryRoot, Templates.Product.ID);
                var productService = new ProductSerice();
                var importedProducts = productService.GetAll();
            
                IndexCustodian.PauseIndexing();
                var counter = 0;
                foreach (var newProduct in importedProducts)
                {
                    var sitecoreProduct = existingProducts.FirstOrDefault(x => x.Fields[Templates.Product.Fields.Code].Value == newProduct.Code);
                    if (sitecoreProduct == null || newProduct.Timestamp > ((DateField)sitecoreProduct.Fields[Templates.Product.Fields.Timestamp]).DateTime)
                    {
                        Map(newProduct, ref sitecoreProduct, repositoryRoot);
                        changedItems.Add(sitecoreProduct);
                    }
                    Log.Info($"Product importer processed {++counter} of {importedProducts.Count()} products", this);
                }
                
            }
            catch (Exception ex)
            {
                Log.Error("Product importer failed", ex, this);
            }
            finally
            {
                IndexCustodian.ResumeIndexing();
                if (changedItems.Any())
                {
                    SyncBucket(repositoryRoot);

                    foreach (var index in ContentSearchManager.Indexes.Where(x => x.Name.Contains("master")))
                    {
                        var changes = changedItems.Select(change => new SitecoreItemUniqueId(change.Uri));
                        IndexCustodian.IncrementalUpdate(index, changes);
                    }
                }
                watch.Stop();
                Log.Info($"Product importer completed, took {watch.ElapsedMilliseconds}  ms", this);
            }
        }

        protected void Map(ImportedProduct newProduct, ref Item sitecoreProduct, Item repositoryRoot)
        {
            using (new SecurityDisabler())
            {
                if (sitecoreProduct == null)
                {
                    var name = ItemUtil.ProposeValidItemName(newProduct.Code);
                    sitecoreProduct = repositoryRoot.Add(name, new TemplateID(Templates.Product.ID));
                    Log.Info($"--Product importer add new {name}", this);
                }

                try
                {
                    sitecoreProduct.Editing.BeginEdit();
                    sitecoreProduct[Templates.Product.Fields.Code] = newProduct.Code;
                    sitecoreProduct[Templates.Product.Fields.InternalName] = newProduct.Name;
                    sitecoreProduct[Templates.Product.Fields.Timestamp] = DateUtil.ToIsoDate(newProduct.Timestamp);
                    ((CheckboxField)sitecoreProduct.Fields[Templates.Product.Fields.Active]).Checked = newProduct.Active;
                    sitecoreProduct.Appearance.DisplayName = $"{newProduct.Code} - {newProduct.Name}";
                    sitecoreProduct.Editing.EndEdit();
                }catch (Exception ex)
                {
                    sitecoreProduct.Editing.CancelEdit();
                    throw ex;
                }
            }
        }

        private void SyncBucket(Item item)
        {
            if (item != null && BucketManager.IsBucket(item))
            {
                BucketManager.Sync(item);
            }
        }

        private List<Item> SearchBucket(Item bucketRootItem, ID templateID)
        {
            if (bucketRootItem == null || !BucketManager.IsBucket(bucketRootItem))
                return null;

            var indexableItem = new SitecoreIndexableItem(bucketRootItem);
            using (var searchContext = ContentSearchManager.GetIndex(indexableItem).CreateSearchContext())
            {
                var predicate = PredicateBuilder.Create<SearchResultItem>(
                    i => i.TemplateId == templateID && i.Path.StartsWith(bucketRootItem.Paths.Path));
                var queryable = searchContext.GetQueryable<SearchResultItem>().Where(predicate);
                return queryable.Select(i => i.GetItem()).ToList();
            }
        }
    }
}