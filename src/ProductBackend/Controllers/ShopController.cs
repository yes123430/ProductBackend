using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProductBackend.EF;
using ProductBackend.Models;
using PagedList;
namespace ProductBackend.Controllers
{
    public class ShopController : Controller
    {
        private DBContext db = new DBContext();
        private int pageSize = 4;

        // GET: Shop
        public async Task<ActionResult> Index()
        {
            var result = new List<Models.ProductModel>();
            
            var list = db.ShowItmeShops.ToList();

            foreach (var item in list)
            {
                var model = await db.Products.Where(w => w.ProdName == item.ProductID).SingleOrDefaultAsync();
                if (model != null) result.Add(model);
            }

            return View(result);
        }
       
        public async Task<ActionResult> Categories(int page = 1)
        {
            int currentPage = page < 1 ? 1 : page;
            var products = db.Products.OrderBy(x => x.ID);
            var result = products.ToPagedList(currentPage, pageSize);
            return View(result);
        }

        public async Task<ActionResult> JoinShopCart(string id)
        {
            HttpCookie cookie = Request.Cookies["shopcart"];
            if (cookie != null)
            {

            }
            else
            {
                // Set Cookie 
                HttpCookie Cookie = new HttpCookie("shopcart","svalue");
                Cookie.Expires = DateTime.Now.AddDays(10); //設置Cookie到期時間
                HttpContext.Response.Cookies.Add(Cookie);
            }
            return View();
        }

        public ActionResult Amount()
        {
            List<Models.ProductModel> productModels = new List<ProductModel>();
            double prodAmount = 0;
            HttpCookie cookie = Request.Cookies["shopcart"];
            if (cookie != null)
            {
                string str = string.Empty;
                if (string.IsNullOrEmpty(cookie.Value)) return View(productModels);
                var array = cookie.Value.Split(',');
                var length = array.Length;
                for(int i = 0; i < length; i=i+2)
                {
                    var id = Convert.ToInt32(array[i].Replace("ID=",""));
                    var count = Convert.ToInt32(array[i + 1].Replace("COUNT=", ""));
                    var model = db.Products.Find(id);

                    if (model == null) continue;
                    model.Count = count;
                    prodAmount += model.Count * model.Price;
                    productModels.Add(model);
                }
            }
            ViewBag.Amount = prodAmount;
            return View(productModels);
        }

        public JsonResult CalAmount(object value)
        {
            double prodAmount = 0;
            HttpCookie cookie = Request.Cookies["shopcart"];
            if (cookie != null)
            {
                string str = string.Empty;
                if (string.IsNullOrEmpty(cookie.Value))
                    return Json(new
                    {
                        success = true,
                        responseText = prodAmount,
                    }, JsonRequestBehavior.AllowGet);


                var array = cookie.Value.Split(',');
                var length = array.Length;
                for (int i = 0; i < length; i = i + 2)
                {
                    var id = Convert.ToInt32(array[i].Replace("ID=", ""));
                    var count = Convert.ToInt32(array[i + 1].Replace("COUNT=", ""));
                    var model = db.Products.Find(id);

                    if (model == null) continue;
                    model.Count = count;
                    prodAmount += model.Count * model.Price;
                }
            }
            ViewBag.Amount = prodAmount;
            return Json(new
            {
                success = true,
                responseText = prodAmount,
            }, JsonRequestBehavior.AllowGet);
        }


 


        // GET: Shop/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductModel productModel = await db.Products.FindAsync(id);
            if (productModel == null)
            {
                return HttpNotFound();
            }
            return View(productModel);
        }

        // GET: Shop/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Shop/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,ProdName,ImagePath,Price,Count,BuildDateTime")] ProductModel productModel)
        {
            if (ModelState.IsValid)
            {
                db.Products.Add(productModel);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(productModel);
        }

        // GET: Shop/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductModel productModel = await db.Products.FindAsync(id);
            if (productModel == null)
            {
                return HttpNotFound();
            }
            return View(productModel);
        }

        // POST: Shop/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,ProdName,ImagePath,Price,Count,BuildDateTime")] ProductModel productModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(productModel).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(productModel);
        }

        // GET: Shop/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductModel productModel = await db.Products.FindAsync(id);
            if (productModel == null)
            {
                return HttpNotFound();
            }
            return View(productModel);
        }

        // POST: Shop/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            ProductModel productModel = await db.Products.FindAsync(id);
            db.Products.Remove(productModel);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
