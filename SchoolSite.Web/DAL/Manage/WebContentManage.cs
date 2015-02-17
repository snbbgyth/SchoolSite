using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using SchoolSite.Core.DbModel;
using SchoolSite.Core.IDAL;

namespace SchoolSite.Web.DAL.Manage
{
    public static class WebContentManage
    {
        private static IWebContentDal _webContentDal;
        private static IWebContentTypeDal _webContentTypeDal;
        private static IMenuTypeDal _menuTypeDal;

        private static List<WebContentType> _webContentTypeList;
        private static List<MenuType> _menuTypeList;

        static WebContentManage()
        {
            _webContentDal = DependencyResolver.Current.GetService<IWebContentDal>();
            _webContentTypeDal = DependencyResolver.Current.GetService<IWebContentTypeDal>();
            _menuTypeDal = DependencyResolver.Current.GetService<IMenuTypeDal>();
            InitWebContentTypes();
            InitMenuTypes();
        }

        public static void RefreshMenuTypes()
        {
            Task.Factory.StartNew(InitMenuTypes);
        }

        public static void RefreshWebContentTypes()
        {
            Task.Factory.StartNew(InitWebContentTypes);
        }

        public static IEnumerable<WebContentType> QueryWebContentTypes()
        {
            return _webContentTypeList;
        }

        public static IEnumerable<MenuType> QueryMenuTypes()
        {
            return _menuTypeList;
        }

        public static MenuType QueryMenuTypeById(int id)
        {
            return _menuTypeList.FirstOrDefault(t => t.Id == id);
        }

        public static WebContentType QueryWebContentTypeById(int id)
        {
            return _webContentTypeList.FirstOrDefault(t => t.Id == id);
        }

        public static WebContentType QueryWebContentTypeByName(string name)
        {
            if (string.IsNullOrEmpty(name)) return null;
            return _webContentTypeList.FirstOrDefault(t => t.Name == name);
        }

        private static void InitWebContentTypes()
        {
            _webContentTypeList = _webContentTypeDal.QueryAll().ToList();
        }

        private static void InitMenuTypes()
        {
            _menuTypeList = _menuTypeDal.QueryAll().ToList();
        }

        public static async Task<List<SelectListItem>> QuerySelectListWebContentTypesByMenuTypeId(string menuTypeId)
        {
            if (menuTypeId == null || menuTypeId == "0")
            {
                var webCotentTypsList = _webContentTypeList;// await _webContentTypeDal.QueryAllAsync();
                var webContentTypeListItemList =
                    webCotentTypsList.Select(t => new SelectListItem { Text = t.Name, Value = t.Id.ToString() }).ToList();
                webContentTypeListItemList.Insert(0, new SelectListItem { Text = "所有", Value = "0" });
                return webContentTypeListItemList;
            }
            else
            {
                //int menuId = Convert.ToInt32(menuTypeId);
                var webCotentTypsList = _webContentTypeList.FindAll(t => t.MenuTypeId == Convert.ToInt32(menuTypeId));// await _webContentTypeDal.QueryByFunAsync(t => t.MenuTypeId == menuId);
                var webContentTypeListItemList =
                    webCotentTypsList.Select(t => new SelectListItem { Text = t.Name, Value = t.Id.ToString() }).ToList();
                webContentTypeListItemList.Insert(0, new SelectListItem { Text = "所有", Value = "0" });
                return webContentTypeListItemList;
            }
        }

        public static IEnumerable<WebContentType> QueryWebContentTypesByMenuName(string menuName)
        {
            var menuType = _menuTypeList.FirstOrDefault(t => t.Name.Equals(menuName));
            if (menuType == null) return new List<WebContentType>();
            return _webContentTypeList.FindAll(t => t.MenuTypeId == menuType.Id);
        }

        public static async Task<List<SelectListItem>> QuerySelectListMenuTypes()
        {
            var menuTypeList = _menuTypeList;// await _menuTypeDal.QueryAllAsync();
            var menuTypeListItemList = menuTypeList.Select(t => new SelectListItem { Text = t.Name, Value = t.Id.ToString() }).ToList();
            menuTypeListItemList.Insert(0, new SelectListItem { Text = "所有", Value = "0" });
            return menuTypeListItemList;
        }

        public static async Task<IEnumerable<WebContent>> QueryByMenuTypeIdAndWebContentTypeId(string menuTypeId, string webContentTypeId)
        {
            IEnumerable<WebContent> entityList;
            if (menuTypeId == null || menuTypeId == "0")
            {
                if (webContentTypeId == null || webContentTypeId == "0")
                    entityList = await _webContentDal.QueryAllAsync();
                else
                    entityList = await _webContentDal.QueryByWebContentTypeIdsAsync(new List<int> { Convert.ToInt32(webContentTypeId) });
            }
            else
            {
                var webContentTypes = _webContentTypeList.FindAll(t => t.MenuTypeId == Convert.ToInt32(menuTypeId)); //await _webContentTypeDal.QueryByFunAsync(t => t.MenuTypeId == typeId);
                if (webContentTypeId == null || webContentTypeId == "0")
                {
                    entityList = await _webContentDal.QueryByWebContentTypeIdsAsync(webContentTypes.Select(t => t.Id).ToList());
                }
                else
                {
                    if (webContentTypes.Any(t => t.Id.Equals(Convert.ToInt32(webContentTypeId))))
                    {
                        entityList = await _webContentDal.QueryByWebContentTypeIdsAsync(new List<int> { Convert.ToInt32(webContentTypeId) });
                    }
                    else
                    {
                        entityList = new List<WebContent>();
                    }
                }
            }
            foreach (var entity in entityList)
            {
                entity.WebContentType = _webContentTypeList.Find(t => t.Id == entity.WebContentTypeId);
            }
            return entityList;
        }

        public static IEnumerable<WebContent> QueryByWebContentTypeNameAndCount(string webContentTypeName, int count)
        {
            var webContentType = _webContentTypeList.FirstOrDefault(t => t.Name == webContentTypeName);
            if (webContentType == null) return new List<WebContent>();
            var entityList = _webContentDal.QueryByFun(t => t.WebContentTypeId == webContentType.Id);
            entityList = entityList.OrderByDescending(t => t.CreateDate);
            if (entityList.Count() < count) return entityList;
            return entityList.Take(count);
        }
    }
}