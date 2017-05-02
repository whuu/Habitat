namespace Sitecore.Feature.AlternativeNews.Repositories
{
    using System.Collections.Generic;
    using Sitecore.Data.Items;
    using Models;

    public interface INewsRepository
    {
        IEnumerable<NewsItem> LoadNews(Item contextItem);
        bool AddNews(NewsItem item, Item contextItem);
    }
}