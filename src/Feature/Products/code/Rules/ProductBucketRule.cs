using Sitecore.Buckets.Rules.Bucketing;
using Sitecore.Data.Fields;
using Sitecore.Rules.Actions;

namespace Sitecore.Feature.Products.Rules
{
    public class ProductBucketRule<T> : RuleAction<T> where T : BucketingRuleContext
    {
        public const string ActiveFolderName = "Active";

        public const string InactiveFolderName = "Inactive";

        public override void Apply(T ruleContext)
        {
            string[] path = new string[2];
            var item = ruleContext.Database.GetItem(ruleContext.NewItemId);
            if (item != null && item.TemplateID == Templates.Product.ID)
            {
                var isActive = ((CheckboxField)item.Fields[Templates.Product.Fields.Active]).Checked;
                path[0] = isActive ? ActiveFolderName : InactiveFolderName;

                var productName = item.Fields[Templates.Product.Fields.InternalName].Value;
                path[1] = string.IsNullOrEmpty(productName) ? "#" : productName[0].ToString();

                ruleContext.ResolvedPath = string.Join(Buckets.Util.Constants.ContentPathSeperator, path);
            }
        }
    }
}