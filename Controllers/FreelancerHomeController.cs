using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
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
            var result = dob.sp_Projectsearches("").ToList();

            List<ProjectSearch> projects = new List<ProjectSearch>();

            foreach (var item in result)
            {
                ProjectSearch project = new ProjectSearch();

                project.PId = item.PId;
                project.PTitle = item.PTitle;
                project.PDescription = item.PDescription;
                project.PType = item.PType;
                project.PExperiance = item.PExperiance;
                project.PSkill = item.PSkill;
                project.PBudget = item.PBudget;
                project.PEnd = item.PEnd;
                project.PLocation = item.PLocation;

                projects.Add(project);
            }

            return View(projects);
        }

        // SEARCH PROJECT
        public ActionResult searchproject_click(ProjectSearch clsobj)
        {
            string qry = "";

            // Project Type
            if (!string.IsNullOrWhiteSpace(clsobj.PType))
            {
                qry += " AND p.PType LIKE '%" + clsobj.PType + "%'";
            }

            // Skill
            if (!string.IsNullOrWhiteSpace(clsobj.PSkill))
            {
                qry += " AND p.PSkill LIKE '%" + clsobj.PSkill + "%'";
            }

            // Budget
            if (!string.IsNullOrWhiteSpace(clsobj.PBudget))
            {
                qry += " AND p.PBudget <= " + clsobj.PBudget;
            }

            // Experience
            if (clsobj.PExperiance > 0)
            {
                qry += " AND p.PExperiance <= " + clsobj.PExperiance;
            }

            return View(
                "FreelancerHomePage",
                getdata(clsobj, qry)
            );
        }

        // GET SEARCHED DATA
        private List<ProjectSearch> getdata(ProjectSearch clsobj, string qry)
        {
            List<ProjectSearch> projectlist = new List<ProjectSearch>();

            using (var con = new SqlConnection(
                ConfigurationManager.ConnectionStrings["importdataconnection"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(
                    "sp_Projectsearches",
                    con
                );

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@qry", qry);

                con.Open();

                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    ProjectSearch item = new ProjectSearch();

                    item.PId = Convert.ToInt32(dr["PId"]);

                    item.PTitle = dr["PTitle"].ToString();

                    item.PDescription = dr["PDescription"].ToString();

                    item.PSkill = dr["PSkill"].ToString();

                    item.PBudget = dr["PBudget"].ToString();

                    item.PExperiance = Convert.ToInt32(
                        dr["PExperiance"]
                    );

                    item.PLocation = dr["PLocation"].ToString();

                    item.PEnd = Convert.ToDateTime(
                        dr["PEnd"]
                    );

                    item.PType = dr["PType"].ToString();

                    projectlist.Add(item);
                }

                con.Close();
            }

            return projectlist;
        }
    }
}