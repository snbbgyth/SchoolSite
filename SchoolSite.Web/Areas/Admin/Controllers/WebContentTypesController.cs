using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using PagedList;
using SchoolSite.Core.DbModel;
using SchoolSite.Core.IDAL;
using SchoolSite.Web.DAL;
using SchoolSite.Web.DAL.Manage;
using SchoolSite.Web.DAL.MySql;

namespace SchoolSite.Web.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin,Edit")]
    public class WebContentTypesController : BaseController
    {

        private IWebContentTypeDal _webContentTypeDal;
        private IMenuTypeDal _menuTypeDal;

        public WebContentTypesController()
        {
            _webContentTypeDal = DependencyResolver.Current.GetService<IWebContentTypeDal>();
            _menuTypeDal = DependencyResolver.Current.GetService<IMenuTypeDal>();
        }

        // GET: Admin/WebContentTypes
        public async Task<ActionResult> Index( string sortOrder, string currentFilter, string searchString, int? page)
        {

            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var entityList = WebContentManage.QueryWebContentTypes();//await _webContentTypeDal.QueryAllAsync();
            foreach (var entity in entityList)
            {
                entity.MenuType = WebContentManage.QueryMenuTypeById(entity.MenuTypeId);// await _menuTypeDal.QueryByIdAsync(entity.MenuTypeId);
            }
            if (entityList.Any())
            {
                if (!String.IsNullOrEmpty(searchString))
                {
                    entityList = entityList.Where(s => s.Name != null && s.Name.Contains(searchString));
                }
                switch (sortOrder)
                {
                    case "name_desc":
                        entityList = entityList.OrderByDescending(s => s.Name);
                        break;
                    default: // Name ascending 
                        entityList = entityList.OrderBy(s => s.Name);
                        break;
                }
            }
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(entityList.ToPagedList(pageNumber, pageSize));
        }

        // GET: Admin/WebContentTypes/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WebContentType webContentType = WebContentManage.QueryWebContentTypeById((int)id);// await _webContentTypeDal.QueryByIdAsync(id);
            if (webContentType == null)
            {
                return HttpNotFound();
            }
            webContentType.MenuType = WebContentManage.QueryMenuTypeById(webContentType.MenuTypeId);// await _menuTypeDal.QueryByIdAsync(webContentType.MenuTypeId);
            return View(webContentType);
        }

        // GET: Admin/WebContentTypes/Create
        public async Task<ActionResult> Create()
        {
             var webContentType=new WebContentType();
             webContentType.MenuTypeList = WebContentManage.QueryMenuTypes();// await _menuTypeDal.QueryAllAsync();
            return View(webContentType);
        }

        // POST: Admin/WebContentTypes/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        public async Task<ActionResult> Create(WebContentType webContentType)
        {
            if (!string.IsNullOrEmpty(webContentType.Name))
            {
                InitInsert(webContentType);
                await _webContentTypeDal.InsertAsync(webContentType);
                WebContentManage.RefreshWebContentTypes();
                return RedirectToAction("Index");
            }
            webContentType.MenuTypeList = WebContentManage.QueryMenuTypes();// await _menuTypeDal.QueryAllAsync();
            return View(webContentType);
        }

        // GET: Admin/WebContentTypes/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WebContentType webContentType = await _webContentTypeDal.QueryByIdAsync(id);
            if (webContentType == null)
            {
                return HttpNotFound();
            }
            webContentType.MenuType = WebContentManage.QueryMenuTypeById(webContentType.MenuTypeId);// await _menuTypeDal.QueryByIdAsync(webContentType.MenuTypeId);
            webContentType.MenuTypeList = WebContentManage.QueryMenuTypes();// await _menuTypeDal.QueryAllAsync();
            return View(webContentType);
        }

        // POST: Admin/WebContentTypes/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit( WebContentType webContentType)
        {
            if (!string.IsNullOrEmpty(webContentType.Name))
            {
                InitModify(webContentType);
                await _webContentTypeDal.ModifyAsync(webContentType);
                WebContentManage.RefreshWebContentTypes();
                return RedirectToAction("Index");
            }
            webContentType.MenuType = WebContentManage.QueryMenuTypeById(webContentType.MenuTypeId);// await _menuTypeDal.QueryByIdAsync(webContentType.MenuTypeId);
            webContentType.MenuTypeList = WebContentManage.QueryMenuTypes();// await _menuTypeDal.QueryAllAsync();
            return View(webContentType);
        }

        // GET: Admin/WebContentTypes/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WebContentType webContentType = await _webContentTypeDal.QueryByIdAsync(id);
            if (webContentType == null)
            {
                return HttpNotFound();
            }
            webContentType.MenuType = WebContentManage.QueryMenuTypeById(webContentType.MenuTypeId);
            return View(webContentType);
        }

        // POST: Admin/WebContentTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            await _webContentTypeDal.DeleteByIdAsync(id);
            WebContentManage.RefreshWebContentTypes();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
            }
            base.Dispose(disposing);
        }
    }
}
