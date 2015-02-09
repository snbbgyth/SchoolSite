using Autofac;
using NHibernate;
using SchoolSite.Core.DAL;
using SchoolSite.Core.IDAL;

namespace SchoolSite.Core.BLL
{
    public class CoreModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            builder.RegisterType<FluentNHibernateDal>().As<IFluentNHibernate>().SingleInstance();
            builder.Register(c => c.Resolve<IFluentNHibernate>().GetSession()).As<ISession>();
            builder.RegisterType<OtherLogInfoDal>().As<IOtherLogInfo>();
            builder.RegisterType<NewsDal>().As<INewsDal>();
            builder.RegisterType<NewsTypeDal>().As<INewsTypeDal>();
            builder.RegisterType<CommentDal>().As<ICommentDal>();
            builder.RegisterType<WebContentDal>().As<IWebContentDal>();
            builder.RegisterType<WebContentTypeDal>().As<IWebContentTypeDal>();
        }
    }
}
