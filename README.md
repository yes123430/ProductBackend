# ProductBackend - Asp .Net MVC 5 實作練習

<p align="center">
  <img src="https://github.com/yes123430/ProductBackend/blob/master/Description/PB01.JPG">
</p>

開發工具：Visual Studio 2019<br>
　資料庫：MSSQL<br>
 
## - 實作那些功能
* CRUD (Entity Framework)
* 單張照片上傳
* jQuery Ajax Post 

#### - CRUD 說明
`EF Context` -> 物件去取得資料。<br>
`[HttpPost]` -> Attribute，設置該Function，是限制哪種method。<br>

 ```csharp
        [HttpPost]
        public ActionResult Create(Models.ProductModel productModel)
        {
            if (ModelState.IsValid)
            {
                using (var dbContext = new EF.DBModelContext())
                {
                    var createModel = new Models.ProductModel();
                    createModel.ProdName = productModel.ProdName;
                    createModel.Price = productModel.Price;
                    createModel.Count = productModel.Count;
                    createModel.ImagePath = productModel.ImagePath;
                    dbContext.Products.Add(createModel);
                    dbContext.SaveChanges();
                    return RedirectToAction("Deatils");
                }
            }
            return View("Index");
        }
 ```      
`ModelState` -> 當 Model Binding 驗證過後會得到一個Model字典物件，在Functions裡來驗證邏輯。<br>
首先在 Model 類的屬性可以設[資料驗證]，可在Countrol，透過ModelState去做控制<br>
 
 ```csharp
    public class ProductModel
    {    
        [Key]
        [Required]
        public int ID { get; set; }

        [Display(Name ="產品名稱")]
        [Required]
        public string ProdName { get; set; }

        [Display(Name = "圖片")]
        public string ImagePath { get; set; }

        [Display(Name = "價格")]
        [Required]
        public double Price { get; set; }

        [Display(Name = "庫存量")]
        public double Count { get; set; }
    }
 ```
