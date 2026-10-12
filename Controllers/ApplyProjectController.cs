using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity.Core.Objects;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC_Project_Freelance.Models;

namespace MVC_Project_Freelance.Controllers
{
    public class ApplyProjectController : Controller
    {
        MVC_ProjectEntities dob = new MVC_ProjectEntities();
        // GET: ApplyProject
        public ActionResult ApplicationPage(int? pid)
        {
            Session["pid"] = pid;
            ObjectParameter op = new ObjectParameter("status", typeof(int));
            int clientid = Convert.ToInt32(Session["uid"]);
            dob.SP_CountAppliedFreelanceWithID(clientid, pid, op);
            int countapply = Convert.ToInt32(op.Value);
            if (countapply == 1)
            {
                ViewBag.isapplied = countapply;
            }
            return View(getdata());
        }
        public ActionResult Application_click(HttpPostedFileBase file, ProjectApplicationView clsobj)
        { 
            if (ModelState.IsValid)
            {
                if (file.ContentLength > 0)
                {
                    string fname = Path.GetFileName(file.FileName);
                    var s = Server.MapPath("~/ApplicationResume");
                    string pa = Path.Combine(s, fname);
                    file.SaveAs(pa);
                    var fullpath = Path.Combine("~/ApplicationResume", fname);
                    clsobj.AResume = fullpath;
                }
                DateTime currentDate = DateTime.Today;
                clsobj.ADate = currentDate;
                clsobj.FId = Convert.ToInt32(Session["uid"]);
                int projid = Convert.ToInt32(Session["pid"]);
                clsobj.AStatus = 1;
                dob.sp_Project_Application_Insert(clsobj.FId, projid, clsobj.ADate, clsobj.AProposal,clsobj.AExpected_Amount, clsobj.AResume, clsobj.AStatus);

                return RedirectToAction("FreelancerHomePage", "FreelancerHome");
            }
            else
            {
                return View("ApplicationPage", clsobj);
            }
        }
        private ProjectApplicationView getdata()
        {
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings
                ["importdataconnection"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SP_GetProjectDatawithID", con);
                cmd.CommandType = CommandType.StoredProcedure;
                int pids = Convert.ToInt32(Session["pid"]);
                cmd.Parameters.AddWithValue("@pid", pids);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                var joblist = new ProjectApplicationView();
                while (dr.Read())
                {
                    var jobcls = new ProjectApplicationView();
                    jobcls.PId = Convert.ToInt32(dr["PId"].ToString());
                    jobcls.PTitle = dr["PTitle"].ToString();
                    jobcls.PDescription = dr["PDescription"].ToString();
                    jobcls.PType = dr["PType"].ToString();
                    jobcls.PExperiance = Convert.ToInt32(dr["PExperiance"]);
                    jobcls.PSkill = dr["PSkill"].ToString();
                    jobcls.PBudget = dr["PBudget"].ToString();
                    jobcls.PEnd = Convert.ToDateTime(dr["PEnd"].ToString());
                    jobcls.PLocation = dr["PLocation"].ToString();

                    joblist.selectjobdetails.Add(jobcls);
                }
                con.Close();
                return joblist;
            }
        }
    }
}