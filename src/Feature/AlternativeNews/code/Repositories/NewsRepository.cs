namespace Sitecore.Feature.AlternativeNews.Repositories
{
    using System.Collections.Generic;
    using Sitecore.Data.Items;
    using Models;
 
    public class NewsRepository : INewsRepository
    {
        public NewsRepository()
        {

        }

        public IEnumerable<NewsItem> LoadNews(Item contextItem)
        {
            var list = new List<NewsItem>();
            list.Add(new NewsItem { Title = "Hello!", Content = "My first news" });
            list.Add(new NewsItem { Title = "Me Again!", Content = "Another news" });

            return list;
        }

        public bool AddNews(NewsItem item, Item contextItem)
        {
            return true;
        }
    }
}