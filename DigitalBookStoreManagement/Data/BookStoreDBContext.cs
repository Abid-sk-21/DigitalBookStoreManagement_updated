using DigitalBookStoreManagement.Authentication;
using Microsoft.EntityFrameworkCore;

namespace DigitalBookStoreManagement.Model
{

    public class BookStoreDBContext : DbContext
    {
        public BookStoreDBContext() { }
        public BookStoreDBContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Inventory> Inventories { get; set; }
        //public DbSet<Book> Books { get; set; }

        public DbSet<Notification> Notifications { get; set; }


        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }

        public DbSet<BookManagement> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Category> Categories { get; set; }


        public DbSet<Review> Reviews { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<BookManagement>()
                .Property(b => b.Price)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Inventory>()
              .HasOne(i => i.BookManagement)
              .WithOne()
              .HasForeignKey<Inventory>(i => i.BookID)
              .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<OrderItem>().HasOne<Order>()
                                       .WithMany(o => o.OrderItems)
                                       .HasForeignKey(oi => oi.OrderID)
                                        .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<CartItem>().HasOne<Cart>()
                                            .WithMany(o => o.CartItems)
                                            .HasForeignKey(oi => oi.CartID)
                                            .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<BookManagement>()
               .HasOne(b => b.Author)
               .WithMany()
               .HasForeignKey(b => b.AuthorID)
               .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<BookManagement>()
                .HasOne(b => b.Category)
                .WithMany()
                .HasForeignKey(b => b.CategoryID)
                .OnDelete(DeleteBehavior.Cascade);
        }

    }

}
