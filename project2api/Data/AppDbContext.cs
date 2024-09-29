using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using project2api.Model;

namespace project2api.Data
{
    public class AppDbContext:IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext>options):base(options)
        {
            
        }
        public DbSet<CartItem> cartItems { get; set; }
        public DbSet<product> Products { get; set; }
        public DbSet<Category> categories { get; set; }
        public DbSet<Cart> Carts { get; set; }
        //public DbSet<CartOrder> employeeTasks { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> orderItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            // relation 1 to m between products and Category---------------------
            modelBuilder.Entity<Category>().HasMany(x => x.products)
                .WithOne(x => x.category).HasForeignKey(x => x.cat_id).OnDelete(DeleteBehavior.Cascade);




            // relationship between order and orderitems---------------------

            modelBuilder.Entity<Order>().HasMany(x => x.OrderItems).
                WithOne(x => x.order).HasForeignKey(x => x.orderId);

            //--- relationshiop between order and user ------------------------
            modelBuilder.Entity<AppUser>()
                .HasMany(x => x.Orders).WithOne(x => x.User).HasForeignKey(x => x.userid);


            // ----- relationship between cart and user 1 ===> 1
            modelBuilder.Entity<AppUser>().HasOne(x => x.cart).WithOne(x => x.Appuser).HasForeignKey<Cart>(x => x.userid);

            // ---- relationship between cart cart item  1===>m
            modelBuilder.Entity<Cart>().HasMany(x => x.cartItems).WithOne(x => x.cart).HasForeignKey(x => x.cartId);


            // relationship between product and cartitem

            modelBuilder.Entity<product>().HasMany(x => x.cartItems).WithOne(x => x.product).
                HasForeignKey(x => x.productid);

            // relationship between product and orderitem

            modelBuilder.Entity<product>().HasMany(x => x.orderItems).WithOne(x => x.product).
                HasForeignKey(x => x.productId);




            base.OnModelCreating(modelBuilder);

        }
    }
}
