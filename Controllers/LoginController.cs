using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC_Project_Freelance.Models;
namespace MVC_Project_Freelance.Controllers
{
    public class LoginController : Controller
    {
        MVC_ProjectEntities dob = new MVC_ProjectEntities();
        // GET: Login
        public ActionResult LoginPage()
        {
            return View();
        }
        public ActionResult ButtonClick(LoginClass cob)
        {
            if (ModelState.IsValid)
            {
                var val = dob.sp_LoginCount(cob.LUsername, cob.LPassword).First();
                if (val == 1)
                {
                    var uid = dob.sp_Get_Login_Id(cob.LUsername, cob.LPassword).First();
                    Session["uid"] = uid;
                    var it = dob.sp_LoginRole(cob.LUsername, cob.LPassword).First();
                    if (it == "Client")
                    {
                        return RedirectToAction("ClientHomePage", "ClientHome");
                    }
                    else if (it == "Freelancer")
                    {
                        return RedirectToAction("FreelancerHomePage", "FreelancerHome");
                    }
                }
                else
                {
                    ModelState.Clear();
                    cob.LMsg = "Invalid Username and Password";
                    return View("LoginPage", cob);
                }
            }
            else
            {
                ModelState.Clear();
                cob.LMsg = "Invalid Username and Password";
                return View("LoginPage", cob);
            }
            return View();
        }
    }
}