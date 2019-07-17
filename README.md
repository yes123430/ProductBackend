# ProductBackend - Asp .Net MVC 5 實作練習

<p align="center">
  <img src="https://github.com/yes123430/ProductBackend/blob/master/Description/PB01.JPG">
</p>

## - 實作那些功能 -
1. CRUD
2. 單張照片上傳
3. jQuery Ajax Post 

        [HttpPost, ActionName("Delete")]
        public ActionResult Delete(string value)
        {
            if (value == null)
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.BadRequest;
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            using (var dbContext = new EF.DBModelContext())
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
    
