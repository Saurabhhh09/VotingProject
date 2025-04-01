using ShowModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI.WebControls;
using VotingDbEntity;
using VotingDbEntity.Entities;
using VotingDbEntity.Enums;
using VotingDbEntity.Repository;
using VotingMVCProject.CommonMethod;

namespace VotingMVCProject.Controllers
{
    public class FormController : Controller
    {
        private readonly UserRepository userRepo = new UserRepository();
        public ActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Registration(RegistrationModel model, string button)
        {
            if (button == "Submit")
            {
                if (ModelState.IsValid)
                {
                    var usermodel = new AddNewUserModel
                    {
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        Email = model.Email,
                        Password = model.Password
                    };
                    var errorMessage = await userRepo.AddUserAsync(usermodel,EnumRole.User);
                    if (string.IsNullOrEmpty(errorMessage))
                    {
                        ModelState.Clear();
                        TempData["Message"] = "Registered Successfully!! Login here !!";
                        return RedirectToAction("Login","Form");
                    }
                    else
                    {
                        ViewBag.Message = errorMessage;
                        return View();
                    }
                }
                return View(model);
            }
            else if (button == "Cancel")
            {
                ModelState.Clear();
                return View();

            }
            return View();

        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Login(LoginModel model, string button)
        {
            if (button == "Login")
            {
                if (ModelState.IsValid)
                {
                    var result = await userRepo.LoginCheckAsync(model);
                    if (result.IsSuccessful)
                    {
                        FormsAuthentication.SetAuthCookie(result.UserId.ToString(),false);
                        HttpCookie cookie = new HttpCookie("UserId");
                        cookie.Value = result.UserId.ToString();
                        Response.Cookies.Add(cookie);
                        
                        if (result.Role.ToString() == "Admin")
                        {
                            return RedirectToAction("Dashboard", "Admin");
                        }                   
                        else
                        {
                            ViewBag.Message = "Login Successful!!";
                            ModelState.Clear();
                            return RedirectToAction("Index","Home",null);
                        }
                    }
                    else
                    {
                        ViewBag.Message = "Incorrect Email or Password !! ";
                        return View();
                    }
                }
                return View(model);
            }
            else if (button == "Reset")
            {

                ModelState.Clear();
                return View();
            }
            return View();
        }
        public async Task<ActionResult> CandidateForm()
        {
            var model = new CandidateRegistrationModel();
            await CandidateRegistration.Dropdowns(model);
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> CandidateForm(CandidateRegistrationModel model)
        {
            if (ModelState.IsValid)
            {
                HttpCookie cookie = Request.Cookies["UserId"];
                var userId = Convert.ToInt32(cookie.Value);
                var _candidateRepo = new CandidateRegisterRepository();
                var errorMessage = await _candidateRepo.RegisterCandidateAsync(model,userId);
                if ( string.IsNullOrEmpty(errorMessage))
                {
                    ModelState.Clear();
                    ViewBag.Message = "Candidate Registered Successfully!!";
                    await CandidateRegistration.Dropdowns(model);
                    return View(model);
                }
                else
                {
                    ViewBag.Message = errorMessage;
                    //await Dropdowns(model);
                    //return View(model);
                }
            }
            await CandidateRegistration.Dropdowns(model);
            return View(model);
        }
        //public async Task Dropdowns(CandidateRegistrationModel model)
        //{
        //    var elections = await _electionRepository.getUpcomingElectionAsync();
        //    model.Elections = elections
        //        .Select(e => new SelectListItem
        //        {
        //            Value = e.ElectionId.ToString(),
        //            Text = e.Title
        //        }).ToList();

        //    var positions = await _positionRepository.getAllPositionsAsync();
        //    model.Positions = positions
        //        .Select(e => new SelectListItem
        //        {
        //            Value = e.PositionId.ToString(),
        //            Text = e.PositionName
        //        }).ToList();
        //}

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            HttpCookie cookie = Request.Cookies["UserId"];
            if (cookie != null)
            {
                cookie.Expires = DateTime.Now.AddDays(-1);
                Response.Cookies.Add(cookie);
            }
            return RedirectToAction("Login", "Form");
        }
    }
}