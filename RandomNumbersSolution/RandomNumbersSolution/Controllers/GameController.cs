using RandomNumbersSolution.Models;
using System;
using System.Linq;
using System.Web.Mvc;


namespace RandomNumbersSolution.Controllers
{
    [Authorize]
    public class GameController :  Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            var matches = db.Matches.Where(m=> m.Expired > DateTime.Now).OrderBy(i => i.Expired).ToList();
            foreach (var match in matches)
            {
                match.Items = db.MatchItems.Where(m => m.MatchId == match.Id).ToList();
             }
            return View(matches);
        }

        public ActionResult Play(int matchId)
        {
            Random random = new Random();
            var matchItem = new MatchItem
            {
                MatchId = matchId,
                UserName = User.Identity.Name,
                Number = random.Next(0, 100)
            };
            return View(matchItem);
        }

        public ActionResult GameResult(MatchItem matchItem)
        {
            var currentMatch = db.Matches.FirstOrDefault(m => m.Id == matchItem.MatchId);
            if(!currentMatch.Items.Any(i =>i.Id == matchItem.Id))
            {
                db.MatchItems.Add(matchItem);
                currentMatch.Items = db.MatchItems.Where(m => m.MatchId == currentMatch.Id).ToList();
                db.SaveChanges();
            }

            if(currentMatch.Expired > DateTime.Now)
            {
                currentMatch.WinUserName = currentMatch.Items.FirstOrDefault(m => m.Number == currentMatch.Items.Max(i => i.Number)).UserName;
                db.SaveChanges();
            }
           
            return View(currentMatch);
        }
    }
}
