using FluentNHibernate.Mapping;

namespace SchoolSite.Core.DbModel.Mappings
{
    public class NewsMapping : ClassMap<News>
    {
        public NewsMapping()
        {
            Id(x => x.Id).UniqueKey("Id").GeneratedBy.Identity();
            Map(x => x.CreateDate);
            Map(x => x.Creater);
            Map(x => x.IsDelete);
            Map(x => x.LastModifier);
            Map(x => x.LastModifyDate);
            Map(x => x.IsPublish);
            Map(x => x.Content).Length(int.MaxValue);
            Map(x => x.Title);
            Map(x => x.ImageForTitle);
            Map(x => x.NewsTypeId);
            //References(x => x.NewsType, "NewsTypeId").Not.LazyLoad();
            //HasManyToMany(x => x.NewsTypeList)
            //  .Cascade.All()
            //  .Inverse()
            //  .Table("NewsType");
        }
    }
}
