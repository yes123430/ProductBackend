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
        private Services.ShopServices ShopServices = new Services.ShopServices();
        private int pageSize = 4;

        [HttpGet]
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

        [HttpGet]
        public ActionResult Categories(int page = 1)
        {
            int currentPage = page < 1 ? 1 : page;
            var products = db.Products.OrderBy(x => x.ID);
            var result = products.ToPagedList(currentPage, pageSize);
            return View(result);
        }

        [HttpGet]
        public ActionResult Amount()
        {
            var models = this.ShopServices.GetShopCart();
            ViewBag.Amount =  this.ShopServices.ProdAmount;
            return View(models);
        }

        public JsonResult CalAmount(object value)
        {
            var models = this.ShopServices.GetShopCart();
            ViewBag.Amount = this.ShopServices.ProdAmount;
            return Json(new
            {
                success = true,
                responseText = this.ShopServices.ProdAmount,
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SaveOrder(Models.OrderModel orderModel)
        {
            try
            {
                List<OrderDetailsModel> orderDetailsModels = new List<OrderDetailsModel>();
                var models = this.ShopServices.GetShopCart();
                if (models.Count() == 0) throw new Exception();

                foreach (var item in models)
                {
                    var newModel = new OrderDetailsModel()
                    {
                        Item = item.ProdName,
                        Count = (int)item.Count,
                        Price = item.Price,
                    };
                    orderDetailsModels.Add(newModel);
                }


                using (var db = new EF.DBContext())
                {
                    orderModel.OrderDate = DateTime.Now;
                    orderModel.Amount = this.ShopServices.ProdAmount;
                    orderModel.OrderDetailModels = orderDetailsModels;
                    db.Orders.Add(orderModel);
                    db.SaveChanges();
                }

                var c = new HttpCookie("shopcart")
                {
                    Expires = DateTime.Now.AddDays(-1)
                };
                Response.Cookies.Add(c);

                ViewData["Message"] = "訂購成功。";
            }
            catch(Exception ex)
            {
                ViewData["Message"] = $"訂購失敗。({ex.Message})";
            }

            return View();
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
