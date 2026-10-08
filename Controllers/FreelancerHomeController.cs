using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC_Project_Freelance.Models;
namespace MVC_Project_Freelance.Controllers
{
    public class FreelancerHomeController : Controller
    {
        MVC_ProjectEntities dob = new MVC_ProjectEntities();
        // GET: FreelancerHome
        public ActionResult FreelancerHomePage()
        {
            var projects = dob.sp_Project_GetOpen().ToList();
            return View(projects);
        }
    }
}