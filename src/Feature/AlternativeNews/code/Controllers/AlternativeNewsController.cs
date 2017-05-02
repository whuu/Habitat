using Glass.Mapper.Sc.Web.Mvc;
using Sitecore.Feature.AlternativeNews.Repositories;
using System.Web.Mvc;
using Sitecore.Feature.AlternativeNews.Models;

namespace Sitecore.Feature.AlternativeNews.Controllers
{
    public class AlternativeNewsController : GlassController
    {
        private readonly INewsRepository _newsRepository;

        public AlternativeNewsController() : this(new NewsRepository())
        {
        }

        public AlternativeNewsController(INewsRepository newsRepository)
        {
            _newsRepository = newsRepository;
        }

        [HttpGet]
        public ActionResult NewsList()
        {
            var items = _newsRepository.LoadNews(ContextItem);
            return View("NewsFeedList", items);
        }

        [HttpPost]
        public ActionResult InsertNews(NewsItem item)
        {
            var result = _newsRepository.AddNews(item, ContextItem);
            return Json(result);
        }
    }
}