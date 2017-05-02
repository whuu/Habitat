namespace Sitecore.Feature.AlternativeNews.Pipelines
{
    using System.Web.Mvc;
    using System.Web.Routing;
    using Sitecore.Pipelines;

    public class RegisterWebApiRoutes
    {
        public void Process(PipelineArgs args)
        {
            RouteTable.Routes.MapRoute("Feature.AlternativeNews.Api", "api/news/{action}", new { controller = "AlternativeNews" });
        }
    }
}