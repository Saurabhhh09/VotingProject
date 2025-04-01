using Microsoft.Ajax.Utilities;
using ShowModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using VotingDbEntity.Entities;
using VotingDbEntity.Repository;

namespace VotingMVCProject.Controllers
{
    public class UserController : Controller
    {
        private readonly VotingRepository _votingRepo = new VotingRepository();
        public async Task<ActionResult> OngoingElections()
        {
            var ongoingElections = await _votingRepo.getOngoingElectionAsync();
            return View(ongoingElections);
        }

        public async Task<ActionResult> ViewCandidates(int electionId)
        {
            var Data = await _votingRepo.GetCandidatesForOngoingElectionAsync(electionId);
            
            return View(Data);
        }

        [HttpPost]
        public async Task<ActionResult> ViewCandidates(VotingPageModel model, int electionId)
        {
            var Data = await _votingRepo.GetCandidatesForOngoingElectionAsync(electionId);
            int UserId = int.Parse(Request.Cookies["UserId"].Value);
            if(UserId == 0)
            {
                return RedirectToAction("Login", "Form");
            }
            if (ModelState.IsValid)
            {
                var VoteMessage = await _votingRepo.RegisterVoteAsync(model,UserId);
                if (VoteMessage != null) 
                {
                    ViewBag.Message = VoteMessage;
                    return View(Data);
                }
                ViewBag.Message = "Voted successfully !!";
                return RedirectToAction("Index","Home");
            }
            return View(model);
        }

        private ElectionRepository _electionRepository = new ElectionRepository();
        public async Task<ActionResult> upComingElectionsDetail()
        {
            var details = await _electionRepository.GetUpComingElectionsAsync();

            var homePageModel = new HomePageModel
            {
                UpcomingElections = details
            };
            return View(homePageModel);
        }

        public ActionResult CompletedElection()
        {
            return View();
        }

        private readonly UserRepository userRepo = new UserRepository();
        public async Task<ActionResult> ProfileUser()
        {
            if (Request.Cookies["UserId"] != null)
            {
                int UserId;
                if (int.TryParse(Request.Cookies["UserId"].Value, out UserId))
                {
                    var model = await userRepo.GetUserbyIdAsync(UserId);
                    if (model == null)
                    {
                        return HttpNotFound();
                    }
                    return View(model);
                }
            }
            return HttpNotFound();
        }

        public async Task<ActionResult> Update(int id)
        {
            var user = await userRepo.GetUserbyIdAsync(id);
            return View(user);
        }

        [HttpPost]
        public async Task<ActionResult> Update(UserModel model)
        {
            var result = await userRepo.UpdateUserAsync(model);
            if (result)
            {
                TempData["UpdateMessage"] = "Updated Successfully!!";
                return RedirectToAction("ProfileUser", "User");
            }
            else
            {
                TempData["UpdateMessage"] = "User Update Failed!!";
                return View(model);
            }
        }

        public ActionResult ViewResults()
        {
            return View();
        }

        public async Task<ActionResult> OngoingElectionsForViewCandidates()
        {
            var ongoingElections = await _votingRepo.getOngoingElectionAsync();
            return View(ongoingElections);
        }

        private readonly VotingRepository _votingCandidateRepo = new VotingRepository();
        public async Task<ActionResult> ShowCandidates(int electionId)
        {
            var Data = await _votingCandidateRepo.GetCandidatesForOngoingElectionAsync(electionId);
            return View(Data);
        }

        private ViewResultToUsersRepository _resultRepository = new ViewResultToUsersRepository();
        public async Task<ActionResult> Result()
        {
            var result = await _resultRepository.GetResultToUsersAsync();
            if(result == null)
            {
                ViewBag.Message = "There is not any data in database !!";
                return View();
            }
           
            return View(result);
        }

    }
}