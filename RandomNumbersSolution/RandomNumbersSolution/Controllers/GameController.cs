using RandomNumbersSolution.Domain.Entities;
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
        private Models.ApplicationDbContext db = new Models.ApplicationDbContext();
        public ActionResult Index()
        {
            var matches = db.Matches.ToList();
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


        public ActionResult Play()
        {
            return View();
        }
        private Match GetCurrentMatch()
        {
            var matches = db.Matches.ToList();
            var currentTime = DateTime.Now;
            return matches.ToList().OrderBy(x => x.Expiration).FirstOrDefault(x => x.Expiration > currentTime);
        }
        public ActionResult Play(int number)
        {
            var currentMatch = GetCurrentMatch();
            if (currentMatch == null) throw new Exception("there is no available match");

            var matchItem = new MatchItem
            {
                MatchId = currentMatch.Id,
                UserName = User.Identity.Name,
                Number = number
            };

            return View();
        }

    }
}
