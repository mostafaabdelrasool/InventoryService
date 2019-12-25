using Microsoft.EntityFrameworkCore;
using Product.Domain.Aggregate;
using System;

namespace Product.Persistance
{
    public class ProductContext : DbContext
    {
        public ProductContext()
        {
        }

        public ProductContext(DbContextOptions<ProductContext> options)
            : base(options)
        {
        }
        public virtual DbSet<Products> Products { get; set; }
        public virtual DbSet<Categories> Categories { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Products>()
          .Property(p => p.Timestamp)
          .IsRowVersion();
            modelBuilder.Entity<Categories>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.HasIndex(e => e.CategoryName)
                    .HasName("CategoryName");

                entity.Property(e => e.CategoryName)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.Description).HasColumnType("ntext");

                entity.Property(e => e.Picture).HasColumnType("image");
            });
            modelBuilder.Entity<Products>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.HasIndex(e => e.CategoryId)
                    .HasName("CategoryID");

                entity.HasIndex(e => e.ProductName)
                    .HasName("ProductName");

                entity.Property(e => e.CategoryId).HasColumnName("CategoryID");

                entity.Property(e => e.ProductName)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.QuantityPerUnit).HasMaxLength(20);

                entity.Property(e => e.ReorderLevel).HasDefaultValueSql("((0))");
                entity.Property(e => e.UnitPrice)
                    .HasColumnType("money")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.UnitsInStock).HasDefaultValueSql("((0))");

                entity.Property(e => e.UnitsOnOrder).HasDefaultValueSql("((0))");

                entity.HasOne(d => d.Category)
                    .WithMany()
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK_Products_Categories");
            });

        }
    }
}
