using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using SchoolSite.Core.DbModel;
using SchoolSite.Core.IDAL;

namespace SchoolSite.Web.DAL.Manage
{
    public static  class NewsManage
    {
        private static INewsDal _newsDal;
        private static INewsTypeDal _newsTypeDal;

        static NewsManage()
        {
            _newsDal = DependencyResolver.Current.GetService<INewsDal>();
            _newsTypeDal = DependencyResolver.Current.GetService<INewsTypeDal>();
            _newsTypeList = _newsTypeDal.QueryAll();
        }

        private static IEnumerable<NewsType> _newsTypeList;

        public static  IEnumerable<NewsType> QueryAllNewsTypes()
        {
            return _newsTypeList;
        }

        public static NewsType QueryNewsTypeByName(string typeName)
        {
            return _newsTypeDal.FirstOrDefault(t => t.Name == typeName);
        }

        public static NewsType QueryNewsTypeById(int id)
        {
           return  _newsTypeDal.FirstOrDefault(t => t.Id == id);
        }

        public static string QueryNewsTypeNameById(int id)
        {
            var entity= _newsTypeDal.FirstOrDefault(t => t.Id == id);
            if (entity == null) return string.Empty;
            return entity.Name;
        }

        public static void RefreshNewsType()
        {
            Task.Factory.StartNew(NewTaskRefreshNewsType);
        }

        public static void NewTaskRefreshNewsType()
        {
            _newsTypeList = _newsTypeDal.QueryAll();
        }

        public static IEnumerable<News> QueryHomeIndexSliderWrapper(int count)
        {
            var entityList = _newsDal.QueryByFun(t => t.ImageForTitle != null);
            entityList = entityList.OrderByDescending(t => t.CreateDate);
            return entityList.Take(count);
        }

        public static IEnumerable<News> QueryHomeIndexNews(int count)
        {
            var entityList = _newsDal.QueryAll();
            entityList = entityList.OrderByDescending(t => t.CreateDate);
            return entityList.Take(count);
        }

    }
}