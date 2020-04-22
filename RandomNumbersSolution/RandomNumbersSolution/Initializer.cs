using RandomNumbersSolution.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RandomNumbersSolution
{
    public static class Initializer
    {
        public static void Seed()
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                if (!context.Matches.Any())
                {
                    Random random = new Random();
                    var match = new List<Match>
                    {
                        new Match
                        {
                            Id = 1,
                            Expired = DateTime.Now.AddMinutes(10),
                            Items = new List<MatchItem>
                            {
                                new MatchItem
                                {
                                    Number = random.Next(0, 100),
                                    UserName = "sam"
                                }
                            }
                        },
                        new Match
                        {
                            Id = 2,
                            Expired = DateTime.Now.AddMinutes(11),
                            Items = new List<MatchItem>
                            {
                                new MatchItem
                                {
                                    Number = random.Next(0, 100),
                                    UserName = "james"
                                }
                            }
                        },
                        new Match
                        {
                            Id = 3,
                            Expired = DateTime.Now.AddMinutes(12),
                            Items = new List<MatchItem>
                            {
                                new MatchItem
                                {
                                    Number = random.Next(0, 100),
                                    UserName = "peter"
                                }
                            }
                        }
                     };

                    context.Matches.AddRange(match);
                    context.SaveChanges();
                }
            }
        }
    }
}