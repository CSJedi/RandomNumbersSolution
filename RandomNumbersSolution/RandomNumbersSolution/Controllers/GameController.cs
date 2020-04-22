using RandomNumbersSolution.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;


namespace RandomNumbersSolution.Controllers
{
    [Authorize]
    public class GameController :  Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            var matches = db.Matches.ToList();
            foreach (var match in matches)
            {
                match.Items = db.MatchItems.Where(m => m.MatchId == match.Id).ToList();
    
                //if (match.Items != null && CheckIsUserPlayedMatch(match))
                //    result.Add(matches);
            }
            return View(matches);
        }

        private bool CheckIsUserPlayedMatch(Match match)
        {
            foreach (var item in match.Items)
            {
                if (item.UserName == User.Identity.Name)
                    return true;
            }
            return false;
        }

        public ActionResult Play(Match match)
        {
            if (match == null) throw new Exception("there is no available match");
            Random random = new Random();
            var matchItem = new MatchItem
            {
                MatchId = match.Id,
                UserName = User.Identity.Name,
                Number = random.Next()
            };
            match.Items.AddRange(db.MatchItems.Where(i =>i.MatchId == match.Id).ToList());
            return View(matchItem);
        }

        public ActionResult GameResult(MatchItem matchItem)
        {
            //var opponentsMatchItems = db.MatchItems.Where(m => m.MatchId == currentMatch.Id && m.Id != matchItem.Id);
            //currentMatch.WinUserName = currentMatch.Items.FirstOrDefault(m => m.Number == currentMatch.Items.Max(i => i.Number)).UserName;
            //db.SaveChanges();
            return View(matchItem);
        }
    }
}
