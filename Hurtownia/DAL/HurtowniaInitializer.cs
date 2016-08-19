using Hurtownia.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hurtownia.DAL
{
    public class HurtowniaInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<HurtowniaContext>
    {
        protected override void Seed(HurtowniaContext context)
        {
            var roleManager = new RoleManager<IdentityRole>(
                new RoleStore<IdentityRole>(new ApplicationDbContext()));

            var userManager = new UserManager<ApplicationUser>(
                new UserStore<ApplicationUser>(new ApplicationDbContext()));

            roleManager.Create(new IdentityRole("Admin"));
            var user =new ApplicationUser{UserName="neo"};
            string passwor = "Complex.Passwor.123";
          

               var  zmienna= userManager.Create(user, passwor);

          
           
            userManager.AddToRole(user.Id, "Admin");

            var users = new List<User>
            {
            new User{UserName="Carson",Name="Carson",Surname="Alexander",DateOfCreation=DateTime.Parse("2005-09-01")},
            new User{UserName="d",Name="Meredith",Surname="Alonso",DateOfCreation=DateTime.Parse("2002-09-01")},
            new User{UserName="b",Name="Arturo",Surname="Anand",DateOfCreation=DateTime.Parse("2003-09-01")},
            new User{UserName="c",Name="Gytis",Surname="Barzdukas",DateOfCreation=DateTime.Parse("2002-09-01")},
            new User{UserName="e",Name="Yan",Surname="Li",DateOfCreation=DateTime.Parse("2002-09-01")},
            new User{UserName="f",Name="Peggy",Surname="Justice",DateOfCreation=DateTime.Parse("2001-09-01")},
            new User{UserName="g",Name="Laura",Surname="Norman",DateOfCreation=DateTime.Parse("2003-09-01")},
            new User{UserName="a",Name="Nino",Surname="Olivetto",DateOfCreation=DateTime.Parse("2005-09-01")},
            new User{UserName="Admin",Name="Wiktor",Surname="Debski",DateOfCreation=DateTime.Parse("2005-09-01")}
            };

            users.ForEach(s => context.Users.Add(s));
            context.SaveChanges();

            var categories = new List<Category>
            {
            new Category{Name="Grocery",},
            new Category{Name="Alkohol"},
            new Category{Name="Beverage"},
            new Category{Name="Milk Products"},
            new Category{Name="Home Product"},
            new Category{Name="Baby Products"},
               new Category{Name="Chemistry"},
            };

            categories.ForEach(s => context.Categories.Add(s));
            context.SaveChanges();
            var products = new List<Product>
            {
                new Product{Name="Chocapic",Description="Nestle",Price=3,Quantity=100,Category=categories[0]},
                new Product{Name="Pur",Description="Pur",Price=3,Quantity=100,Category=categories[6]},
                new Product{Name="Milk",Description="Milk",Price=3,Quantity=100,Category=categories[3]},
                new Product{Name="Bread",Description="Chleb",Price=3,Quantity=100,Category=categories[0]},
                new Product{Name="Milky-Way",Description="Twix",Price=3,Quantity=100,Category=categories[0]},
                new Product{Name="Chupa-Chups",Description="Chupa",Price=3,Quantity=100,Category=categories[0]},
                new Product{Name="Cheese",Description="Milk",Price=3,Quantity=100,Category=categories[0]},
            };
            products.ForEach(s => context.Products.Add(s));
            context.SaveChanges();
            var typy = new List<Statistic>
            {
            new Statistic{ProductType=ProductType.Słodycze, Product=products[0],User=users[0]},
            new Statistic{ProductType=ProductType.Chemiczne, Product=products[0],User=users[0]},
            new Statistic{ProductType=ProductType.ProduktMleczny, Product=products[0],User=users[0]},
            new Statistic{ProductType=ProductType.Wypieki, Product=products[0],User=users[0]},
            new Statistic{ProductType=ProductType.Słodycze, Product=products[0],User=users[0]},
            new Statistic{ProductType=ProductType.Słodycze, Product=products[0],User=users[0]},
            new Statistic{ProductType=ProductType.ProduktMleczny, Product=products[0],User=users[0]},
           
            
            };
            typy.ForEach(s => context.Statistics.Add(s));
            context.SaveChanges();


          
        }
    }
}