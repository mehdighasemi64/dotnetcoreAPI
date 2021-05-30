using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace dotnetcoreAPI.Models
{
    public partial class soamanaDB : DbContext
    {
        public soamanaDB()
        {
        }

        public soamanaDB(DbContextOptions<soamanaDB> options)
            : base(options)
        {
        }

        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<DeliveryOrder> DeliveryOrders { get; set; }
        public virtual DbSet<Image> Images { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderDetail> OrderDetails { get; set; }
        public virtual DbSet<OrderStatus> OrderStatuses { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductCategory> ProductCategories { get; set; }
        public virtual DbSet<ResetCredential> ResetCredentials { get; set; }
        public virtual DbSet<Shop> Shops { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<VwProductImage> VwProductImages { get; set; }
        public virtual DbSet<VwShoppingHistory> VwShoppingHistories { get; set; }
        public virtual DbSet<VwShoppingHistoryOld> VwShoppingHistoryOlds { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                 //optionsBuilder.UseSqlServer("Server=.;Database=somana;Persist Security Info=True;Integrated Security=True");
                optionsBuilder.UseSqlServer("Server= mssql.somana.ir;Database=somana;User Id =somana;Password = LWXmOuh5#0s;");

            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1256_CI_AS");

            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("Category");

                entity.Property(e => e.CategoryName).HasMaxLength(50);
            });

            modelBuilder.Entity<Country>(entity =>
            {
                entity.ToTable("Country");

                entity.Property(e => e.CountryName).HasMaxLength(50);
            });

            modelBuilder.Entity<DeliveryOrder>(entity =>
            {
                entity.ToTable("DeliveryOrder");

                entity.Property(e => e.Address).HasMaxLength(200);

                entity.Property(e => e.CellPhone).HasMaxLength(15);

                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.DesireDateTime).HasColumnType("smalldatetime");

                entity.Property(e => e.LastName).HasMaxLength(50);

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.ZipCode).HasMaxLength(10);
            });

            modelBuilder.Entity<Image>(entity =>
            {
                entity.ToTable("Image");

                entity.Property(e => e.ImageFormat).HasMaxLength(50);

                entity.Property(e => e.ImageName).HasMaxLength(50);
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("Order");

                entity.Property(e => e.DeliveryAddress).HasMaxLength(1000);

                entity.Property(e => e.DeliveryEmail).HasMaxLength(1000);

                entity.Property(e => e.DeliveryPhone).HasMaxLength(1000);

                entity.Property(e => e.OrderDate).HasColumnType("smalldatetime");
            });

            modelBuilder.Entity<OrderDetail>(entity =>
            {
                entity.ToTable("OrderDetail");
            });

            modelBuilder.Entity<OrderStatus>(entity =>
            {
                entity.ToTable("OrderStatus");

                entity.Property(e => e.OrderStatusName).HasMaxLength(50);
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Product");

                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.DescriptionEN)
                    .HasMaxLength(500)
                    .HasColumnName("DescriptionEN");

                entity.Property(e => e.ProductName).HasMaxLength(100);

                entity.Property(e => e.ProductNameEN)
                    .HasMaxLength(100)
                    .HasColumnName("ProductNameEN");

                entity.Property(e => e.TagEN).HasColumnName("TagEN");
            });

            modelBuilder.Entity<ProductCategory>(entity =>
            {
                entity.ToTable("ProductCategory");

                entity.Property(e => e.ProductCategoryName).HasMaxLength(100);

                entity.Property(e => e.ProductCategoryNameEN)
                    .HasMaxLength(100)
                    .HasColumnName("ProductCategoryNameEN");
            });

            modelBuilder.Entity<ResetCredential>(entity =>
            {
                entity.ToTable("ResetCredential");

                entity.Property(e => e.ExpirationDate).HasColumnType("smalldatetime");

                entity.Property(e => e.UserName).HasMaxLength(50);
            });

            modelBuilder.Entity<Shop>(entity =>
            {
                entity.ToTable("Shop");

                entity.Property(e => e.DateShop).HasColumnType("smalldatetime");

                entity.Property(e => e.Email).HasMaxLength(100);

                entity.Property(e => e.Mobile).HasMaxLength(11);

                entity.Property(e => e.RefId)
                    .HasMaxLength(100)
                    .HasColumnName("RefID");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Address).HasMaxLength(100);

                entity.Property(e => e.Email).HasMaxLength(100);

                entity.Property(e => e.FirstName).HasMaxLength(50);

                entity.Property(e => e.LastName).HasMaxLength(50);

                entity.Property(e => e.Mobile).HasMaxLength(11);

                entity.Property(e => e.UserName).HasMaxLength(50);
            });

            modelBuilder.Entity<VwProductImage>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vw_ProductImage");

                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.DescriptionEN)
                    .HasMaxLength(500)
                    .HasColumnName("DescriptionEN");

                entity.Property(e => e.ImageName).HasMaxLength(50);

                entity.Property(e => e.ProductName).HasMaxLength(100);

                entity.Property(e => e.ProductNameEN)
                    .HasMaxLength(100)
                    .HasColumnName("ProductNameEN");

                entity.Property(e => e.TagEn).HasColumnName("TagEN");
            });

            modelBuilder.Entity<VwShoppingHistory>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vwShoppingHistory");

                entity.Property(e => e.Num).HasColumnName("num");

                entity.Property(e => e.OrderDate).HasColumnType("smalldatetime");

                entity.Property(e => e.OrderStatusName).HasMaxLength(50);
            });

            modelBuilder.Entity<VwShoppingHistoryOld>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vwShoppingHistoryOLD");

                entity.Property(e => e.OrderDate).HasColumnType("smalldatetime");

                entity.Property(e => e.OrderStatusName).HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
