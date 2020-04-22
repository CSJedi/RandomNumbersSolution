using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RandomNumbersSolution.Controllers
{
    public class HomeController : Controller
    {
        private Models.ApplicationDbContext db = new Models.ApplicationDbContext();

        // GET: Matches
        public ActionResult Index()
        {
            var matches = db.Matches.OrderBy(i => i.Expired).ToList();
            foreach (var match in matches)
            {
                match.Items = db.MatchItems.Where(m => m.MatchId == match.Id).ToList();
            }
            return View(matches);
        }


        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            ViewBag.Title = "About page.";
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Contact page.";
            ViewBag.Title = "Contact page.";
            return View();
        }
    }
}