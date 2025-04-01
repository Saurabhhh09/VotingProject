using iTextSharp.text;
using iTextSharp.text.pdf;
using Rotativa;
using ShowModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using VotingDbEntity.Entities;
using VotingDbEntity.Enums;
using VotingDbEntity.Repository;

namespace VotingMVCProject.Controllers
{
    [Authorize]

    public class AdminController : Controller
    {
        private readonly UserRepository userRepo = new UserRepository();
        public async Task<ActionResult> Dashboard()
        {
            try
            {
                var DashBoardRepo = new DashboardCountRepository();
                var usersCount = await DashBoardRepo.UsersCountAsync();
                var candidateCount = await DashBoardRepo.CandidateCountAsync();
                var electionCount = await DashBoardRepo.ElectionCountAsync();
                var AdminDashboardModel = new AdminDashboardModel
                {
                    TotalUsers = usersCount,
                    TotalCandidate = candidateCount,
                    TotalElection = electionCount
                };
                return View(AdminDashboardModel);
            }
            catch(Exception)
            {
                return RedirectToAction("Index", "Error");
            }
        }

        public ActionResult AddElection()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> AddElection(AddElectionModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var electionRepo = new AddElectionRepository();
                    var result = await electionRepo.AddElectionAsync(model);
                    if (result)
                    {
                        ModelState.Clear();
                        ViewBag.Message = "Election Added Successfully!!";
                        return View();
                    }
                    else
                    {
                        ViewBag.Message = "Election Added Failled!!";
                        return View(model);
                    }
                }
                return View(model);
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Error");
            }
        }

        public ActionResult AddPosition()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> AddPosition(AddPositionModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var PositionRepo = new AddPositionRepository();
                    var result = await PositionRepo.AddPositionAsync(model);
                    if (result)
                    {
                        ModelState.Clear();
                        ViewBag.Message = "Position Added Successfully!!";
                        return View();
                    }
                    else
                    {
                        ViewBag.Message = "Position Added Failed!!";
                        return View();
                    }
                }
                return View(model);
            }
            catch (Exception) 
            {
                return RedirectToAction("Index", "Error");
            }
        }


        public async Task<ActionResult> ShowUsers()
        {
            try
            {
                var users = await userRepo.GetAllUsersAsync();
                if (users.Count == 0)
                {
                    ViewBag.Message = "No Users Found!!";
                }
                var totalUsersModel = new TotalUsersModel
                {
                    Users = users
                };
                return View(totalUsersModel);
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Error");
            }
        }

        public async Task<ActionResult> ShowUsersById(int id)
        {
            try
            {
                var user = await userRepo.GetUserbyIdAsync(id);
                return View(user);
            }
            catch (Exception) 
            {
                return RedirectToAction("Index", "Error");
            }

        }

        public async Task<ActionResult> EditUser(int id)
        {
            try
            {
                var user = await userRepo.GetUserbyIdAsync(id);
                return View(user);
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Error");
            }
        }

        [HttpPost]
        public async Task<ActionResult> EditUser(UserModel model)
        {
            try
            {
                var result = await userRepo.UpdateUserByAdminAsync(model);
                if (result)
                {
                    TempData["UpdateMessage"] = "User Updated Successfully!!";
                    return RedirectToAction("ShowUsers", "Admin");
                }
                else
                {
                    TempData["UpdateMessage"] = "User Update Failed!!";
                    return View(model);
                }
            }
            catch (Exception) 
            {
                return RedirectToAction("Index", "Error");
            }
        }

        public async Task<ActionResult> DeleteUser(int id)
        {
            try
            {
                var user = await userRepo.GetUserbyIdAsync(id);
                return View(user);
            }
            catch(Exception)
            {
                return RedirectToAction("Index", "Error");
            }
        }

        [HttpPost]
        public async Task<ActionResult> DeleteUser(UserModel model)
        {
            try
            {
                var result = await userRepo.DeleteUserAsync(model.Id);
                if (result)
                {
                    //ModelState.Clear();
                    TempData["DeleteMessage"] = "User Deleted Successfully!!";
                    return RedirectToAction("ShowUsers", "Admin");
                }
                else
                {
                    TempData["DeleteMessage"] = "User Delete Failed!!";
                    return View(model);
                }
            }
            catch(Exception)
            {
                return RedirectToAction("Index", "Error");
            }
        }

        public ActionResult AddUser()
        {
            try
            {
                var model = new AddNewUserModel();
                Role();
                return View(model);
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Error");
            }
        }

        [HttpPost]
        public async Task<ActionResult> AddUser(AddNewUserModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var errorMessage = await userRepo.AddUserAsync(model, model.Role);
                    if (string.IsNullOrEmpty(errorMessage))
                    {
                        ModelState.Clear();
                        ViewBag.Message = "User Added Successfully!!";
                        Role();
                        return View();
                    }
                    else
                    {
                        ViewBag.Message = errorMessage;
                        return View(model);
                    }
                }
                Role();
                return View(model);
            }
            catch(Exception)
            {
                return RedirectToAction("Index", "Error");
            }
        }
        public void Role()
        {
            ViewBag.Roles = Enum.GetValues(typeof(EnumRole))
                               .Cast<EnumRole>()
                               .Select(r => new SelectListItem
                               {
                                   Value = ((int)r).ToString(),
                                   Text = r.ToString()
                               }).ToList();
        }
        public async Task<ActionResult> ShowElection()
        {
            try
            {
                var _getElectionRepo = new GetElectionRepository();
                var elections = await _getElectionRepo.getAllElectionAsync();
                var electionModel = new TotalElection
                {
                    Elec = elections
                };
                if (elections.Count == 0)
                {
                    ViewBag.Message = "No Elections Found!!";
                }
                return View(electionModel);
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Error");
            }
        }

        public async Task<ActionResult> ProfilePage()
        {
            try
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
            catch (Exception)
            {
                return RedirectToAction("Index", "Error");
            }
        }

        private readonly GetElectionRepository _votingRepo = new GetElectionRepository();
        public async Task<ActionResult> Candidates(enumElectionStatus electionStatusId = enumElectionStatus.Completed)
        {
            try
            {
                var _showCandidateRepo = new ShowCandidateRepository();
                var Candidates = await _showCandidateRepo.showCandidatesToAdminAsync(electionStatusId);
                var model = new ShowCandidatesModel
                {
                    StatusId = electionStatusId,
                    Elections = Candidates
                };
                return View(model);
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Error");
            }
        }

        private readonly GetElectionRepository _elections = new GetElectionRepository();
        public async Task<ActionResult> Results()
        {
            try
            {
                var _getElectionRepo = new GetElectionRepository();
                var elections = await _getElectionRepo.getCompletedElectionAsync();
                var electionModel = new TotalElection
                {
                    Elec = elections
                };
                if (elections.Count == 0)
                {
                    ViewBag.Message = "No Elections Found!!";
                }
                return View(electionModel);
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Error");
            }
        }

        public ActionResult ViewResults(int electionId)
        {
            try
            {
                ViewBag.Id = electionId;
                return View();
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Error");
            }
        }

        private ResultRepository _resultRepository = new ResultRepository();
        public async Task<JsonResult> GetResult(int electionId)
        {
            var result = await _resultRepository.GetResultsAsync(electionId);
            foreach (var position in result.ResultPositions)
            {
                position.DataModels.Insert(0, new object[] { "Candidate", "Votes" });
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ViewVotesCurrentElection()
        {
            return View();
        }

        private readonly ViewVotesRepository _viewVotes = new ViewVotesRepository();

        public async Task<JsonResult> GetVotes()
        {
            var currentResult = await _viewVotes.getVotesCurrentElection();
            if(currentResult != null)
            {
                foreach (var position in currentResult.ViewVotesPositions)
                {
                    position.ViewVoteModel.Insert(0, new object[] { "Votes", "Total Votes" });
                }
                
            }
            else
            {
                currentResult = new ViewVotesModel();
            }
            return Json(currentResult, JsonRequestBehavior.AllowGet);
        }


        public async Task<ActionResult> Reports()
        {
            try
            {
                var _getElectionRepo = new GetElectionRepository();
                var elections = await _getElectionRepo.getCompletedElectionAsync();
                var electionModel = new TotalElection
                {
                    Elec = elections
                };
                if (elections.Count == 0)
                {
                    ViewBag.Message = "No Elections Found!!";
                }
                return View(electionModel);
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Error");
            }
        }

        public async Task<ActionResult> ReportsByElection(int electionId)
        {
            try
            {
                ViewBag.Id = electionId;
                var _reportsRepo = new ReportsRepository();
                var report = await _reportsRepo.Reports(electionId);
                return View(report);
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Error");
            }
        }
        public async Task<ActionResult> GeneratePdf(int electionId)
        {
            try
            {
                var _reportsRepo = new ReportsRepository();
                var report = await _reportsRepo.Reports(electionId);

                MemoryStream ms = new MemoryStream();
                Document document = new Document(PageSize.A4, 10, 10, 10, 10);
                PdfWriter writer = PdfWriter.GetInstance(document, ms);
                document.Open();

                // Add election name as heading
                var color1 = BaseColor.GREEN;
                var electionNameFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 20, color1);
                var electionNameParagraph = new Paragraph(report.ElectionName, electionNameFont)
                {
                    Alignment = Element.ALIGN_CENTER,
                    SpacingAfter = 20
                };
                document.Add(electionNameParagraph);

                // Add position headings, candidate names, and tables
                foreach (var position in report.ReportPositions)
                {
                    // Add position heading
                    var color2 = BaseColor.ORANGE;
                    var positionFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 16, color2);
                    var positionParagraph = new Paragraph(position.PositionName, positionFont)
                    {
                        SpacingBefore = 10,
                        SpacingAfter = 10
                    };
                    document.Add(positionParagraph);

                    foreach (var candidate in position.Candidates)
                    {
                        string votes = candidate.TotalVotes.ToString();
                        // Add candidate name
                        var candidateFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 14);
                        var candidateParagraph = new Paragraph($"{candidate.CandidateName} - Total Votes : {votes}", candidateFont)
                        {
                            SpacingBefore = 5,
                            SpacingAfter = 5
                        };
                        document.Add(candidateParagraph);

                        // Add table for voter names and vote dates
                        PdfPTable table = new PdfPTable(2)
                        {
                            WidthPercentage = 100,
                            SpacingBefore = 5,
                            SpacingAfter = 10
                        };
                        table.AddCell("Voter Name");
                        table.AddCell("Vote Date");

                        foreach (var voter in candidate.Voters)
                        {
                            table.AddCell(voter.VoterName);
                            table.AddCell(voter.Date);
                        }

                        document.Add(table);
                    }
                }

                document.Close();
                return File(ms.ToArray(), "application/pdf", "ReportsByElection.pdf");
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Error");
            }
        }

    }
}