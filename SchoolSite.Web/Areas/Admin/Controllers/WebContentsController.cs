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
using SchoolSite.Web.DAL.MySql;

namespace SchoolSite.Web.Areas.Admin.Controllers
{
    [MyAuthorize(Roles = "Admin,Edit")]
    public class WebContentsController : BaseController
    {
        private IWebContentDal _webContentDal;
        private IWebContentTypeDal _webContentTypeDal;
        private IMenuTypeDal _menuTypeDal;

        public WebContentsController()
        {
            _webContentDal = DependencyResolver.Current.GetService<IWebContentDal>();
            _webContentTypeDal = DependencyResolver.Current.GetService<IWebContentTypeDal>();
            _menuTypeDal = DependencyResolver.Current.GetService<IMenuTypeDal>();
        }

        // GET: Admin/WebContents
        public async Task<ActionResult> Index(string menuTypeId, string currentFilter, string searchString, int? page)
        {


            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            IEnumerable<WebContent> entityList;
            if (menuTypeId == null || menuTypeId == "0")
                entityList = await _webContentDal.QueryAllAsync();
            else
            {
                int typeId = Convert.ToInt32(menuTypeId);
                var webContentTypes =
                    await _webContentTypeDal.QueryByFunAsync(t => t.MenuTypeId == typeId);
                entityList =
                    await _webContentDal.QueryByWebContentTypeIdsAsync(webContentTypes.Select(t => t.Id).ToList());

            }
            foreach (var entity in entityList)
            {
                entity.WebContentType = await _webContentTypeDal.QueryByIdAsync(entity.WebContentTypeId);
            }

            if (entityList.Any())
            {
                if (!String.IsNullOrEmpty(searchString))
                {
                    entityList = entityList.Where(s => (s.Content != null && s.Content.Contains(searchString))
                                                                          || (s.Title != null && s.Title.Contains(searchString))
                                                                          || (s.Creater != null && s.Creater.Contains(searchString))
                                                                          || (s.LastModifier != null && s.LastModifier.Contains(searchString)));

                }
                entityList = entityList.OrderByDescending(s => s.LastModifyDate);
            }

            var menuTypeList = await _menuTypeDal.QueryAllAsync();
            var selectListItemList = menuTypeList.Select(t => new SelectListItem { Text = t.Name, Value = t.Id.ToString() }).ToList();
            selectListItemList.Insert(0, new SelectListItem { Text = "所有", Value = "0" });
            ViewBag.MenuTypes = selectListItemList;
            if (ViewData["Selected"] == null)
                ViewData["Selected"] = "0";

            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(entityList.ToPagedList(pageNumber, pageSize));

        }

        // GET: Admin/WebContents/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WebContent webContent = await _webContentDal.QueryByIdAsync(id);
            if (webContent == null)
            {
                return HttpNotFound();
            }
            webContent.WebContentType = await _webContentTypeDal.QueryByIdAsync(webContent.WebContentTypeId);
            return View(webContent);
        }

        // GET: Admin/WebContents/Create
        public async Task<ActionResult> Create()
        {
            var webContent = new WebContent();
            webContent.WebContentTypeList = await _webContentTypeDal.QueryAllAsync();
            return View(webContent);
        }

        // POST: Admin/WebContents/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(WebContent webContent)
        {
            InitInsert(webContent);
            await _webContentDal.InsertAsync(webContent);
            return RedirectToAction("Index");
        }

        // GET: Admin/WebContents/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WebContent webContent = await _webContentDal.QueryByIdAsync(id);
            if (webContent == null)
            {
                return HttpNotFound();
            }
            webContent.WebContentType = await _webContentTypeDal.QueryByIdAsync(webContent.WebContentTypeId);
            webContent.WebContentTypeList = await _webContentTypeDal.QueryAllAsync();
            return View(webContent);
        }

        // POST: Admin/WebContents/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(WebContent webContent)
        {
            InitModify(webContent);
            await _webContentDal.ModifyAsync(webContent);
            return RedirectToAction("Index");
        }

        // GET: Admin/WebContents/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WebContent webContent = await _webContentDal.QueryByIdAsync(id);
            if (webContent == null)
            {
                return HttpNotFound();
            }
            webContent.WebContentType = await _webContentTypeDal.QueryByIdAsync(webContent.WebContentTypeId);
            return View(webContent);
        }

        // POST: Admin/WebContents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            await _webContentDal.DeleteByIdAsync(id);
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
