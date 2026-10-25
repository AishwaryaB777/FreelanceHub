using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;
using MVC_Project_Freelance.Models;

namespace MVC_Project_Freelance.Controllers
{
    public class ClientHomeApplicationController : Controller
    {
        MVC_ProjectEntities dob = new MVC_ProjectEntities();

        // Helper: fetch applications for a client, optionally by status
        private System.Collections.Generic.List<ApplicantDetailsView> GetApplications(int cid, int? status)
        {
            var statusParam = new SqlParameter("@Status", SqlDbType.Int)
            {
                Value = status.HasValue ? (object)status.Value : DBNull.Value
            };

            return dob.Database.SqlQuery<ApplicantDetailsView>(
                "EXEC dbo.sp_Client_GetApplications @CId, @Status",
                new SqlParameter("@CId", cid),
                statusParam
            ).ToList();
        }

        // GET: Client home - ONLY pending applicants
        public ActionResult ViewApplicants()
        {
            if (Session["uid"] == null)
                return RedirectToAction("LoginPage", "Login");

            int cid = Convert.ToInt32(Session["uid"]);

            var pending = GetApplications(cid, 0);

            var model = new ClientApplicantsPageView
            {
                Filter = "Pending",
                TotalCount = pending.Count,
                PendingCount = pending.Count,
                Applicants = pending,
                Msg = TempData["Msg"] as string,
                MsgType = TempData["MsgType"] as string
            };

            return View("ViewApplicants", model);
        }

        // GET: History - decided applicants (Accepted / Rejected)
        public ActionResult History(string filter = "All")
        {
            if (Session["uid"] == null)
                return RedirectToAction("LoginPage", "Login");

            int cid = Convert.ToInt32(Session["uid"]);

            var decided = GetApplications(cid, null)
                .Where(x => x.AStatus == 1 || x.AStatus == 2)
                .ToList();

            if (filter != "Accepted" && filter != "Rejected")
                filter = "All";

            var model = new ClientApplicantsPageView
            {
                Filter = filter,
                TotalCount = decided.Count,
                AcceptedCount = decided.Count(x => x.AStatus == 1),
                RejectedCount = decided.Count(x => x.AStatus == 2),
                Applicants = filter == "Accepted" ? decided.Where(x => x.AStatus == 1).ToList()
                           : filter == "Rejected" ? decided.Where(x => x.AStatus == 2).ToList()
                           : decided
            };

            return View("History", model);
        }

        // POST: Accept or reject an application
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Decide(int A_Id, string decision, string remarks)
        {
            if (Session["uid"] == null)
                return RedirectToAction("LoginPage", "Login");

            int cid = Convert.ToInt32(Session["uid"]);

            if (decision != "Accepted" && decision != "Rejected")
            {
                TempData["Msg"] = "Invalid decision.";
                TempData["MsgType"] = "error";
                return RedirectToAction("ViewApplicants");
            }

            var outputStatus = new SqlParameter("@OStatus", SqlDbType.Int)
            {
                Direction = ParameterDirection.Output
            };

            dob.Database.ExecuteSqlCommand(
                @"EXEC dbo.sp_Application_Decide
                    @A_Id, @CId, @Decision, @Remarks,
                    @OStatus OUTPUT",
                new SqlParameter("@A_Id", A_Id),
                new SqlParameter("@CId", cid),
                new SqlParameter("@Decision", decision),
                new SqlParameter("@Remarks",
                    string.IsNullOrWhiteSpace(remarks)
                        ? (object)DBNull.Value
                        : remarks.Trim()),
                outputStatus
            );

            int result = outputStatus.Value == DBNull.Value
                ? 0
                : Convert.ToInt32(outputStatus.Value);

            switch (result)
            {
                case 1:
                    TempData["Msg"] = "Application " + decision.ToLower() + " successfully.";
                    TempData["MsgType"] = "success";
                    break;
                case -1:
                    TempData["Msg"] = "Invalid decision.";
                    TempData["MsgType"] = "error";
                    break;
                case -2:
                    TempData["Msg"] = "This application does not belong to your projects.";
                    TempData["MsgType"] = "error";
                    break;
                case -3:
                    TempData["Msg"] = "This application has already been decided.";
                    TempData["MsgType"] = "error";
                    break;
                default:
                    TempData["Msg"] = "Something went wrong. Please try again.";
                    TempData["MsgType"] = "error";
                    break;
            }

            return RedirectToAction("ViewApplicants");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                dob.Dispose();

            base.Dispose(disposing);
        }
    }
}