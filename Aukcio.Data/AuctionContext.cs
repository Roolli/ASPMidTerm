using System;
using Aukcio.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Aukcio.Data
{
    public class AuctionContext : IdentityDbContext<AuctionUser, IdentityRole<Guid>, Guid>
    {
        public AuctionContext()
        {
            this.Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            Guid userRole = Guid.NewGuid();
            Guid adminRole = Guid.NewGuid();
            
           
            builder.Entity<IdentityRole<Guid>>().HasData(
                new
                {
                    Id = userRole,
                    Name = "Customer",
                    NormalizedName = "CUSTOMER"
                },
                new
                {
                    Id = adminRole,
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                }
            );
            var regularUser = new AuctionUser()
            {
                Id = Guid.NewGuid(),
                Email = "teszt@elek.hu",
                NormalizedEmail = "teszt@elek.hu",
                EmailConfirmed =  true,
                SecurityStamp = String.Empty,
                UserName = "teszt@elek.hu",
                NormalizedUserName = "teszt@elek.hu",
                
            };
            var adminUser = new AuctionUser()
            {
                Id = Guid.NewGuid(),
                Email = "vampir@agi.hu",
                NormalizedEmail = "vampir@agi.hu",
                EmailConfirmed = true,
                SecurityStamp = String.Empty,
                UserName = "vampir@agi.hu",
                NormalizedUserName = "vampir@agi.hu",
            };
            adminUser.PasswordHash = new PasswordHasher<AuctionUser>().HashPassword(null, "asd123");
            regularUser.PasswordHash = new PasswordHasher<AuctionUser>().HashPassword(null, "teszt1234");
            builder.Entity<AuctionUser>().HasData(adminUser,regularUser);

            builder.Entity<IdentityUserRole<Guid>>().HasData(
                new IdentityUserRole<Guid>
                {
                    RoleId = adminRole,
                    UserId = adminUser.Id,
                },
                new IdentityUserRole<Guid>()
                {
                    RoleId = userRole,
                    UserId = regularUser.Id
                }
            );
            builder.Entity<Auction>(auc =>
            {
                auc.HasOne(a => a.Owner).WithMany(u => u.Auctions).HasForeignKey(fk => fk.OwnerId).IsRequired();
                auc.HasMany(a => a.Images).WithOne(i => i.Auction).HasForeignKey(image => image.AuctionId);
                auc.HasData(new Auction
                {
                    Description = "Some Description",
                    OwnerId = regularUser.Id,
                    AuctionName = "Very expensive stuff",
                    BuyoutPrice = 500,
                    CurrentBid = 0,
                    UId = "d477590e-3fa7-4371-831f-d7e3f6b62e98",
                },
                    new Auction
                    {
                        Description = "This is a very good description",
                        OwnerId = adminUser.Id,
                        AuctionName = "Not so expensive",
                        BuyoutPrice = 100,
                        CurrentBid = 20,
                        UId = "d477590e-4fa8-4371-831f-d7e3f6b62e98",
                    }
                    );
            });
            
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseLazyLoadingProxies().UseSqlServer(
                    @"Data Source=(LocalDB)\MSSQLLocalDB;Initial Catalog=AuctionDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            }

            base.OnConfiguring(optionsBuilder);
        }

        public DbSet<Auction> Auctions { get; set; }
    }
}