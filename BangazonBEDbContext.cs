using Microsoft.EntityFrameworkCore;
using BangazonBE.Models;

public class BangazonBEDbContext : DbContext
{
    DbSet<User> Users { get; set; }
    DbSet<Customer> Customers { get; set; }
    DbSet<Seller> Sellers { get; set; }
    DbSet<Order> Orders { get; set; }
    DbSet<Product> Products { get; set; }
    DbSet<ProductType> ProductTypes { get; set; }
    DbSet<ProductOrders> ProductOrders { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().HasData(new User[]
        {
            new User {Id = 1, Uid = "MyUid", UserName = "dinnerdoggy" },
            new User {Id = 2, Uid = "TheirUid", UserName = "Stranger" }
        });

        modelBuilder.Entity<Customer>().HasData(new Customer[]
        {
            new Customer {Id = 1, UserId = "MyUid"}
        });

        modelBuilder.Entity<Seller>().HasData(new Seller[]
        {
            new Seller {Id = 1, UserId = "TheirUid", Purse = 0m }
        });

        modelBuilder.Entity<ProductType>().HasData(new ProductType[] 
        {
            new ProductType {Id = 1, TypeName = "Dagger" },
            new ProductType {Id = 2, TypeName = "Straight Sword" },
            new ProductType {Id = 3, TypeName = "Greatsword" },
            new ProductType {Id = 4, TypeName = "Colossal Sword" },
            new ProductType {Id = 5, TypeName = "Thrusting Sword" },
            new ProductType {Id = 6, TypeName = "Heavy Thrusting Sword" },
            new ProductType {Id = 7, TypeName = "Curved Sword" },
            new ProductType {Id = 8, TypeName = "Curved Greatsword" },
            new ProductType {Id = 9, TypeName = "Katana" },
            new ProductType {Id = 10, TypeName = "Twinblade" },
            new ProductType {Id = 11, TypeName = "Axe" },
            new ProductType {Id = 12, TypeName = "Greataxe" }
        });

        modelBuilder.Entity<Product>().HasData(new Product[]
        {
            new Product 
            {
                Id = 1, 
                ProductName = "Dark Moon Greatsword",
                ProductImage = "https://eldenring.wiki.fextralife.com/file/Elden-Ring/dark_moon_greatsword_weapon_elden_ring_wiki_guide_200px.png",
                Description = "A Moon Greatsword, bestowed by a Carian queen upon her\r\nspouse to honor long-standing tradition.\r\nOne of the legendary armaments.\r\n\r\nRanni's sigil is a full moon, cold and leaden, and this sword is but a beam of its light.",
                ProductTypeId = 3,
                Price = 1000000m,
                Quantity = 1
            },

            new Product
            {
                Id = 2,
                ProductName = "Hand of Malenia",
                ProductImage = "https://eldenring.wiki.fextralife.com/file/Elden-Ring/hand_of_malenia_katana_weapon_elden_ring_wiki_guide_200px.png",
                Description = "Blade built into Malenia's prosthetic arm.\r\nThrough consecration it is resistant to rot.\r\n\r\nMalenia's war prosthesis symbolized her victories.\r\nSome claim to have seen wings when the weapon was raised aloft;\r\nwings of fierce determination that have never known defeat.",
                ProductTypeId = 9,
                Price = 1000000m,
                Quantity = 1
            },

            new Product
            {
                Id = 3,
                ProductName = "Magma Wyrm's Scalesword",
                ProductImage = "https://eldenring.wiki.fextralife.com/file/Elden-Ring/magma_wyrms_scalesword_curved_greatsword_weapon_elden_ring_wiki_guide_200px.png",
                Description = "Curved greatsword wielded by Magma Wyrms. The shape resembles a dragon's jaw and is covered in hard scales.\r\n\r\nIt's said these land-bound dragons were once human heroes who partook in dragon communion, a grave transgression for which they were cursed to crawl the earth upon their bellies, shadows of their former selves.",
                ProductTypeId = 8,
                Price = 1000000m,
                Quantity = 1
            }
        });

        modelBuilder.Entity<Order>().HasData(new Order[]
        {
            new Order { Id = 1, CustomerId = 1, SellerId = 1, Pending = true, Total = 1000000m }
        });

        modelBuilder.Entity<ProductOrders>().HasData(new ProductOrders[]
        {
            new ProductOrders { Id = 1, OrderId = 1, ProductId = 1 }
        });
    }
    public BangazonBEDbContext(DbContextOptions<BangazonBEDbContext> context) : base(context) { }
}