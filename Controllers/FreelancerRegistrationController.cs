using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC_Project_Freelance.Models;
namespace MVC_Project_Freelance.Controllers
{
    public class FreelancerRegistrationController : Controller
    {
        MVC_ProjectEntities dob = new MVC_ProjectEntities();
        // GET: FreelancerRegistration
        public ActionResult FreelancerRegPage()
        {
            return View();
        }
        public ActionResult ButtonClick(FreelancerInsertClass cob, HttpPostedFileBase file, HttpPostedFileBase resumeFile)
        {
            if (ModelState.IsValid)
            {
                if (file!=null && file.ContentLength > 0)
                {
                    string fname = Path.GetFileName(file.FileName);
                    var s = Server.MapPath("~/Photo");
                    string pa = Path.Combine(s, fname);
                    file.SaveAs(pa);
                    var fullpath = Path.Combine("~//Photo", fname);
                    cob.FPhoto = fullpath;
                }
                if (resumeFile != null && resumeFile.ContentLength > 0)
                {
                    string rname = Path.GetFileName(resumeFile.FileName);
                    var rs = Server.MapPath("~/Resume");
                    string rpa = Path.Combine(rs, rname);
                    resumeFile.SaveAs(rpa);
                    cob.FResume = "~/Resume/" + rname;
                }

                var getmaxid = dob.sp_Login_GetMax_Id().FirstOrDefault();
                int mid = Convert.ToInt32(getmaxid);
                int regid = 0;
                if (mid == 0)
                {
                    regid = 1;
                }
                else
                {
                    regid = mid + 1;
                }
                cob.FStatus = "Active";
                ObjectParameter op = new ObjectParameter("OStatus", typeof(int));
                dob.sp_Freelancer_Insert(regid, cob.FName, cob.FAge, cob.FPhone, cob.FEmail, cob.FAddress, cob.FQualification, cob.FExp, cob.FSkills, cob.FPhoto, cob.FResume, cob.FStatus, op);
                dob.sp_Login_Insert(regid, cob.FUsername, cob.FPassword, "Freelancer", "Active");
                cob.FMsg = "Successfully Registered";
                return View("FreelancerRegPage", cob);
            }
            return View("FreelancerRegPage", cob);
        }
    }
}