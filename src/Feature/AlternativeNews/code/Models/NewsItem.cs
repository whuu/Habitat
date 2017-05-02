using Glass.Mapper.Sc.Configuration.Attributes;

namespace Sitecore.Feature.AlternativeNews.Models
{
    [SitecoreType(TemplateId = Templates.NewsfeedArticle.ID)]
    public class NewsItem : IGlassBase
    {
        [SitecoreField(Templates.NewsfeedArticle.Fields.Title_FieldName)]
        public string Title { get; set; }

        [SitecoreField(Templates.NewsfeedArticle.Fields.Content_FieldName)]
        public string Content { get; set; }
    }
}