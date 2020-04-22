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
                match.Items = db.MatchItems.Where(m => m.MatchId == match.Id && m.UserName == User.Identity.Name).ToList();
                if (match.Items != null && match.Items.Count == 2)
                    result.Add(match);
            }
            return View(result);
        }


        public ActionResult Play()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }


    }
}
