using ShowModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using VotingDbEntity.Repository;

namespace VotingMVCProject.Controllers
{
    public class HomeController : Controller
    {
        private ElectionRepository _electionRepository = new ElectionRepository();
        public async Task<ActionResult> Index()
        {
            var upcomingElections = await _electionRepository.GetUpComingElectionsAsync();

            var homePageModel = new HomePageModel
            {
                UpcomingElections = upcomingElections
            };
            return View(homePageModel);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

    }
}