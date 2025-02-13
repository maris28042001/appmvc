
using AppMvc.Models.Blog;
using AppMvc.Models.Contacts;
using AppMvc.Models.Product;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AppMvc.models
{
    //AppMvc.models.AppDbContext
    public class AppDbContext : IdentityDbContext<AppUser> {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            foreach(var entytiTye in modelBuilder.Model.GetEntityTypes()){
                var tableName = entytiTye.GetTableName();
                if(tableName.StartsWith("AspNet")){
                    entytiTye.SetTableName(tableName.Substring(6));
                }
            }

            modelBuilder.Entity<Category>( entity => {
                entity.HasIndex(c => c.Slug)
                      .IsUnique();
            });

            modelBuilder.Entity<PostCategory>( entity => {
                entity.HasKey( c => new {c.PostID, c.CategoryID});
            });

            modelBuilder.Entity<Post>( entity => {
                entity.HasIndex( p => p.Slug)
                      .IsUnique();
            });

            modelBuilder.Entity<CategoryProduct>( entity => {
                entity.HasIndex(c => c.Slug)
                      .IsUnique();
            });

            modelBuilder.Entity<ProductCategoryProduct>( entity => {
                entity.HasKey( c => new {c.ProductID, c.CategoryID});
            });

            modelBuilder.Entity<ProductModel>( entity => {
                entity.HasIndex( p => p.Slug)
                      .IsUnique();
            });  
        }
        public DbSet<Contact> Contacts { get; set; }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<PostCategory> PostCategories { get; set; }

        public DbSet<CategoryProduct> CategoryProducts { get; set; }
        public DbSet<ProductModel> Products { get; set;}
        public DbSet<ProductCategoryProduct>  ProductCategoryProducts { get; set; }
        public DbSet<ProductPhoto> ProductPhotos { get; set; }
    }
}