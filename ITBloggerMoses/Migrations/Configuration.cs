namespace ITBloggerMoses.Migrations
{
    using ITBloggerMoses.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ITBloggerMoses.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(ITBloggerMoses.Models.ApplicationDbContext context)
        {
            #region Sample Code
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
            #endregion

            //Create the Admin & Moderator roles
            var roleManager = new RoleManager<IdentityRole>(
                new RoleStore<IdentityRole>(context));

           //We want to create an Admin role ONLY if one doestn't  already exist
            if (!context.Roles.Any(r => r.Name == "Admin"))
                roleManager.Create(new IdentityRole { Name = "Admin" });

            //We want to create an Moderator role ONLY if one doestn't  already exist
            if (!context.Roles.Any(r => r.Name == "Moderator"))
                roleManager.Create(new IdentityRole { Name = "Moderator" });

            //Create an Admin and Moderator user

            var userManager = new UserManager<ApplicationUser>(
                new UserStore<ApplicationUser>(context));

            //Create an Admin user
            if (!context.Users.Any(u => u.Email == "BlogAdmin@mailinator.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "BlogAdmin@mailinator.com",
                    Email = "BlogAdmin@mailinator.com",
                    FirstName = "Moses",
                    LastName = "DeSaussure",
                    DisplayName = "The Golden Boy"                  
                }, "Abc&123!");
            }

            //Create an Moerator user
            if (!context.Users.Any(u => u.Email == "BlogModerator@mailinator.com"))
            {
             userManager.Create(new ApplicationUser
                {
                    UserName = "BlogModerator@mailinator.com",
                    Email = "BlogModerator@mailinator.com",
                    FirstName = "Jason",
                    LastName = "Twichell",
                    DisplayName = "J 'MadCat' Twichell"
                }, "Abc&123!");
            }

            //Assign each user to the appropriate role
            var userId = userManager.FindByEmail("BlogAdmin@mailinator.com").Id;
            userManager.AddToRole(userId, "Admin");

            userId = userManager.FindByEmail("BlogModerator@mailinator.com").Id;
            userManager.AddToRole(userId, "Moderator");
        }
    }
}
