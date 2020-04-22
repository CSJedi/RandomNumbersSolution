using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using RandomNumbersSolution.Models;

namespace RandomNumbersSolution.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            var context = new ApplicationDbContext();
            //if(await context.Matches.CountAsync() == 0)
            //{
                Random random = new Random();
                var match = new List<Match>
                {
                    new Match
                    {
                        Id = 1,
                        Expired = DateTime.Now.AddHours(1),
                        Items = new List<MatchItem>
                        {
                            new MatchItem
                            {
                                Number = random.Next(),
                                UserName = "sam"
                            }
                        }
                    },
                    new Match
                    {
                        Id = 2,
                        Expired = DateTime.Now.AddHours(2),
                        Items = new List<MatchItem>
                        {
                            new MatchItem
                            {
                                Number = random.Next(),
                                UserName = "james"
                            }
                        }
                    },
                    new Match
                    {
                        Id = 3,
                        Expired = DateTime.Now.AddHours(3),
                        Items = new List<MatchItem>
                        {
                            new MatchItem
                            {
                                Number = random.Next(),
                                UserName = "peter"
                            }
                        }
                    }
                 };

                context.Matches.AddRange(match);
                context.SaveChanges();
            //}
            return context;
        }

        public DbSet<Match> Matches { get; set; }
        public DbSet<MatchItem> MatchItems { get; set; }
    }
}