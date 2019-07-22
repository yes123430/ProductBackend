using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProductBackend.Filter;

namespace ProductBackend.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            var bResult = Convert.ToBoolean(Session["auth"]);
            if (!bResult)
            {
                Session["Name"] = "Guest";
            }
            if (bResult)
            {
                return RedirectToAction("Details");
            }

            return View();
        }

        [HttpPost]
        public ActionResult Login(string id, string ps)
        {
            if (string.IsNullOrWhiteSpace(id) || string.IsNullOrWhiteSpace(ps))
            {
                return RedirectToAction("Index", "Admin");
            }

            Session["auth"] = true;
            Session["Name"] = id;
            return RedirectToAction("Details");
        }

        public ActionResult Logout()
        {
            Session["auth"] = false;
            ViewBag.Name = "Guest";
            return View("Index");
        }

        [HttpPost]
        public ActionResult Create(Models.ProductModel productModel)
        {
            if (ModelState.IsValid)
            {
                using (var dbContext = new EF.DBContext())
                {
                    var createModel = new Models.ProductModel();
                    createModel.ProdName = productModel.ProdName;
                    createModel.Price = productModel.Price;
                    createModel.Count = productModel.Count;
                    createModel.ImagePath = productModel.ImagePath;
                    createModel.ProdDescription = productModel.ProdDescription;
                    createModel.BuildDateTime = DateTime.Now;
                    dbContext.Products.Add(createModel);
                    dbContext.SaveChanges();
                    return RedirectToAction("Deatils");
                }
            }
            return View("Index");
        }

        public ActionResult Edit(int id)
        {
            using (var dbContext = new EF.DBContext())
            {
                try
                {
                    var model = dbContext.Products.Find(id);
                    return View(model);
                }
                catch
                {
                    Response.StatusCode = (int)System.Net.HttpStatusCode.BadRequest;
                    return Content("找不到產品");
                }
            }
        }

        [HttpPost]
        public ActionResult Edit(Models.ProductModel model, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                using (var dbContext = new EF.DBContext())
                {
                    string fileName = string.Empty;
                    string filePath = string.Empty;
                    if (file != null && file.ContentLength > 0)
                    {
                        fileName = System.IO.Path.GetFileName(file.FileName);
                        filePath = System.IO.Path.Combine(Server.MapPath("~/FileUploads"), fileName);
                    }

                    try
                    {
                        var resultModel = dbContext.Products.Find(model.ID);
                        resultModel.ProdName = model.ProdName;
                        resultModel.Price = model.Price;
                        var strPath = $"/FileUploads/{fileName}";
                        if (!(string.IsNullOrEmpty(fileName))
                            && resultModel.ImagePath != strPath)
                        {
                            file.SaveAs(filePath);
                            resultModel.ImagePath = strPath;
                            model.ImagePath = strPath;
                        }

                        resultModel.Count = model.Count;
                        resultModel.ProdDescription = model.ProdDescription;
                        resultModel.BuildDateTime = DateTime.Now;
                        dbContext.SaveChanges();

                        ViewBag.msg = "儲存成功。";
                    }
                    catch
                    {


                        //Response.StatusCode = (int)System.Net.HttpStatusCode.BadRequest;
                        //return Content("找不到產品");
                    }
                }
            }
            return View("Edit", model);
        }

        [AuthorizePlus]
        public ActionResult Details()
        {
            //var bResult = Convert.ToBoolean(Session["auth"]);
            //if (bResult == false)
            //{
            //    return RedirectToAction("Index");
            //}

            var models = new List<Models.ProductModel>();
            using (var dbContext = new EF.DBContext())
            {
                models = dbContext.Products.ToList();
            }

            return View(models);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult Delete(string value)
        {
            if (value == null)
            {
                //Response.StatusCode = (int)System.Net.HttpStatusCode.BadRequest;
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }

            using (var dbContext = new EF.DBContext())
            {
                try
                {
                    var wValue = Convert.ToInt32(value);
                    var model = dbContext.Products.Where(w => w.ID == wValue).Single();
                    dbContext.Products.Remove(model);
                    dbContext.SaveChanges();
                }
                catch (Exception ex)
                {
                    return Json(new
                    {
                        success = false,
                        responseText = ex.Message,
                    }, JsonRequestBehavior.AllowGet);
                }
            }

            return Json(new
            {
                success = true,
                responseText = "Your message successfuly sent!"
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Orders()
        {
            var models = new List<Models.OrderModel>();
            using (var dbContext = new EF.DBContext())
            {
                models = dbContext.Orders.ToList();
            }

            return View(models);
        }
    }
}
