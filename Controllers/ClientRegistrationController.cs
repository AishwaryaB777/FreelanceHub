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
    public class ClientRegistrationController : Controller
    {
        MVC_ProjectEntities dob = new MVC_ProjectEntities();
        // GET: ClientRegistration
        public ActionResult ClientRegPage()
        {
            return View();
        }
        public ActionResult ButtonClick(ClientInsertClass cob, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                if (file.ContentLength > 0)
                {
                    string fname = Path.GetFileName(file.FileName);
                    var s = Server.MapPath("~/Photo");
                    string pa = Path.Combine(s, fname);
                    file.SaveAs(pa);
                    var fullpath = Path.Combine("~//Photo", fname);
                    cob.CPhoto = fullpath;
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
                cob.CStatus = "Active";
                ObjectParameter op = new ObjectParameter("OStatus", typeof(int));
                dob.sp_Client_Insert(regid, cob.CName, cob.CPhone, cob.CEmail, cob.CAddress, cob.CCompany_Name, cob.CC_Description,cob.CPhoto, cob.CStatus,op);
                dob.sp_Login_Insert(regid, cob.CUsername, cob.CPassword, "Client","Active");
                cob.CMsg = "Successfully Registered";
                return View("ClientRegPage", cob);
            }
            return View("ClientRegPage", cob);
        }
    }
}