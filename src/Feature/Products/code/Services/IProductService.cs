namespace Sitecore.Feature.Products.Services
{
    using System.Collections.Generic;
    using Models;

    public interface IProductService
    {
        IEnumerable<ImportedProduct> GetAll();
    }
}