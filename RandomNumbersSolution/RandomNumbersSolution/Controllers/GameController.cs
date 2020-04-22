using RandomNumbersSolution.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
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
            ViewBag.Message = "Your application description page.";

            return View();
        }


    }
}
