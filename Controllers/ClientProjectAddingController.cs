using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC_Project_Freelance.Models;
namespace MVC_Project_Freelance.Controllers
{
    public class ClientProjectAddingController : Controller
    {
        MVC_ProjectEntities dob = new MVC_ProjectEntities();
        // GET: ClientHome
        public ActionResult ClientPage()
        {
            return View();
        }
        public ActionResult ButtonClick(ProjectInsertClass cob)
        {
            if (ModelState.IsValid)
            {
                cob.CId = Convert.ToInt32(Session["uid"]);
                cob.PStatus = "Open";

                ObjectParameter op = new ObjectParameter("OStatus", typeof(int));
                dob.sp_Project_Insert(cob.CId, cob.PTitle, cob.PDescription, cob.PSkill,
                    cob.PBudget, cob.PExperiance, cob.PQualification, cob.PLocation,
                    cob.PAdd, cob.PEnd, cob.PType, cob.PStatus, op);
                ModelState.Clear();
                cob.PMsg = "Successfully Registered";
                return View("ClientPage", cob);
            }
            return View("ClientPage", cob);
        }
    }
}