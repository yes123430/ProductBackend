using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProductBackend.Services
{
    public class ShopServices 
    {
        private double _ProdAmount = 0;
        private string CookieName = "shopcart";

        public double ProdAmount { get { return this._ProdAmount; } set { this._ProdAmount = value; } }

        public List<Models.ProductModel> GetShopCart()
        {
            List<Models.ProductModel> productModels = new List<Models.ProductModel>();
            HttpCookie cookie = HttpContext.Current.Request.Cookies.Get(CookieName);
            if (cookie != null)
            {
                string str = string.Empty;
                if (string.IsNullOrEmpty(cookie.Value)) return productModels;

                var array = cookie.Value.Split(',');
                var length = array.Length;
                for (int i = 0; i < length; i = i + 2)
                {
                    var id = Convert.ToInt32(array[i].Replace("ID=", ""));
                    var count = Convert.ToInt32(array[i + 1].Replace("COUNT=", ""));

                    using (var db = new EF.DBContext())
                    {
                        var model = db.Products.Find(id);

                        if (model == null) continue;
                        model.Count = count;
                        this.ProdAmount += model.Count * model.Price;
                        productModels.Add(model);
                    }
                }

            }
            return productModels;
        }
    }
}