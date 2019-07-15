﻿namespace ProductBackend.EF
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class DBModelContext : DbContext
    {
        // 您的內容已設定為使用應用程式組態檔 (App.config 或 Web.config)
        // 中的 'DBModel' 連接字串。根據預設，這個連接字串的目標是
        // 您的 LocalDb 執行個體上的 'ProductBackend.EF.DBModel' 資料庫。
        // 
        // 如果您的目標是其他資料庫和 (或) 提供者，請修改
        // 應用程式組態檔中的 'DBModel' 連接字串。
        public DBModelContext()
            : base("name=DBModelContext")
        {
        }

        // 針對您要包含在模型中的每種實體類型新增 DbSet。如需有關設定和使用
        // Code First 模型的詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=390109。

        // public virtual DbSet<MyEntity> MyEntities { get; set; }

        public virtual DbSet<Models.ProductModel> Products { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            var productTable = modelBuilder.Entity<Models.ProductModel>().ToTable("Product");
            productTable.Property(k => k.ID)
                .IsRequired()
                .HasColumnName("ID")
                .HasColumnType("Int");
            productTable.Property(k => k.ProdName)
                .IsRequired()
                .HasColumnName("ProdName")
                .HasColumnType("nvarchar(max)");
            productTable.Property(k => k.Price)
                .IsRequired()
                .HasColumnName("Price")
                .HasColumnType("float");
            productTable.Property(k => k.Count)
                .IsRequired()
                .HasColumnName("Count");

            base.OnModelCreating(modelBuilder);
        }

        /*
         教學:http://kevintsengtw.blogspot.com/2013/10/aspnet-mvc-entity-framework-code-first.html
         Enable-Migrations
         add-migration
         update-database
         update-database –Verbose
         */
    }


    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
}