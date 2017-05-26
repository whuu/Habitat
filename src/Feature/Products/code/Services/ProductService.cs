namespace Sitecore.Feature.Products.Services
{
    using System.Collections.Generic;
    using Models;
    using System;
    public class ProductSerice : IProductService
    {
        public ProductSerice()
        {
        }

        public IEnumerable<ImportedProduct> GetAll()
        {
            var products = new List<ImportedProduct>();
            products.Add(new ImportedProduct { Code = "IAAD-011", Name = "Leather Jacket", Timestamp = DateTime.Now.AddHours(-1) });
            products.Add(new ImportedProduct { Code = "IAAD-012", Name = "Gentelmen's Hat", Timestamp = DateTime.Now.AddHours(-2) });
            products.Add(new ImportedProduct { Code = "IAAD-013", Name = "Socks", Timestamp = DateTime.Now.AddHours(-3) });
            products.Add(new ImportedProduct { Code = "IAAD-014", Name = "Elegant Trousers", Timestamp = new DateTime(2017, 05, 12, 11, 0, 0) });

            Random random = new Random();
            for (int i=0; i < 496; i++)
            {
                int randomNumber = random.Next(0, 1000);
                products.Add(new ImportedProduct { Code = "IAAD-00" + randomNumber, Name = "Product " + randomNumber, Timestamp = DateTime.Now.AddHours(-1) });
                
            }
            return products;
        }
    }
}