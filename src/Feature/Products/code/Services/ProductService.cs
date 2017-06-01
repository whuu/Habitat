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
            products.Add(new ImportedProduct { Code = "IAAD-0", Name = "Leather Jacket", Timestamp = DateTime.Now.AddHours(-1) });
            products.Add(new ImportedProduct { Code = "IAAD-1", Name = "Gentelmen's Hat", Timestamp = DateTime.Now.AddHours(-2) });
            products.Add(new ImportedProduct { Code = "IAAD-2", Name = "Socks", Timestamp = DateTime.Now.AddHours(-3) });
            products.Add(new ImportedProduct { Code = "IAAD-3", Name = "Elegant Trousers", Timestamp = new DateTime(2017, 05, 12, 11, 0, 0) });

            Random random = new Random();
            for (int i=4; i < 1000; i++)
            {
                var randomNumber = random.Next(0, 1000);
                var active = randomNumber <= 500;
                char let = (char)('A' + random.Next(0, 26));
                products.Add(new ImportedProduct { Code = "IAAD-00" + i, Name = let + "Product " + randomNumber, Timestamp = DateTime.Now.AddHours(-1), Active = active });
            }
            return products;
        }
    }
}