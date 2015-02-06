﻿using Autofac;
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
            builder.RegisterType<OrderDal>().As<IOrderDal>();
            builder.RegisterType<OrderItemDal>().As<IOrderItemDal>();
            builder.RegisterType<ProductDal>().As<IProductDal>();
            builder.RegisterType<ProductTypeDal>().As<IProductTypeDal>();
            builder.RegisterType<ShopCartItemDal>().As<IShopCartItemDal>();
            builder.RegisterType<PictureDal>().As<IPictureDal>();
            builder.RegisterType<ProductPictureDal>().As<IProductPictureDal>();
            builder.RegisterType<WebContentDal>().As<IWebContentDal>();
            builder.RegisterType<WebContentTypeDal>().As<IWebContentTypeDal>();
        }
    }
}