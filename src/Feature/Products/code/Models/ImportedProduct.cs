using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sitecore.Feature.Products.Models
{
    public class ImportedProduct
    {
        public string Code { get; set; }
        
        public string Name { get; set; }
        
        public DateTime Timestamp { get; set; } 
    }
}