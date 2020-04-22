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
            var matches = db.Matches.Where(m => !String.IsNullOrEmpty(m.WinUserName)).ToList();
            var result = new List<Match>();
            foreach (var match in matches)
            {
                match.Items = db.MatchItems.Where(m => m.MatchId == match.Id).ToList();
    
                if (match.Items != null && CheckIsUserPlayedMatch(match))
                    result.Add(match);
            }
            return View(result);
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

        public ActionResult GameBegin()
        {
            return View();
        }

        public ActionResult Play()
        {
            Random random = new Random();
            var matchItem = new MatchItem
            {
                UserName = User.Identity.Name,
                Number = random.Next()
            };
            return View(matchItem);
        }
        private Match GetCurrentMatch()
        {
            var matches = db.Matches.ToList();
            var currentTime = DateTime.Now;
            return matches.ToList().OrderBy(x => x.Expiration).FirstOrDefault(x => x.Expiration > currentTime);
        }

        public ActionResult GameResult(MatchItem matchItem)
        {
            var currentMatch = GetCurrentMatch();
            if (currentMatch == null) throw new Exception("there is no available match");
            currentMatch.Items.Add(matchItem);
            var opponentMatchItem = db.MatchItems.FirstOrDefault(m => m.MatchId == currentMatch.Id && m.Id != matchItem.Id);
            currentMatch.Items.Add(matchItem);
            currentMatch.WinUserName = matchItem.Number > opponentMatchItem.Number ? matchItem.UserName : opponentMatchItem.UserName;
            return View(currentMatch);
        }
    }
}
