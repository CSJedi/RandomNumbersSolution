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

        public ActionResult GameBegin()
        {
            return View(new Match());
        }

        public ActionResult Play(Match match)
        {
            Random random = new Random();
            var matchItem = new MatchItem
            {
                MatchId = match.Id,
                UserName = User.Identity.Name,
                Number = random.Next()
            };
            match.Items.Add(matchItem);
            return View(matchItem);
        }
        private Match GetCurrentMatch()
        {
            var matches = db.Matches.ToList();
            var currentTime = DateTime.Now;
            return matches.ToList().OrderBy(x => x.Expiration).FirstOrDefault(x => x.Expiration > currentTime);
        }

        public ActionResult Submit(MatchItem matchItem)
        {
            var currentMatch = GetCurrentMatch();
            if (currentMatch == null) throw new Exception("there is no available match");
        
            currentMatch.Items.Add(matchItem);

            return View(currentMatch);
        }

    }
}
