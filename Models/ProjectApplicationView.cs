using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_Project_Freelance.Models
{
    public class ProjectApplicationView
    {
        public List<ProjectApplicationView> selectjobdetails { get; set; } = new List<ProjectApplicationView>();
        public int FId { get; set; }
        public int PId { get; set; }
        public DateTime ADate { get; set; }
        public string AProposal { get; set; }
        public string AExpected_Amount { get; set; }
        public string AResume { get; set; }
        public int AStatus { get; set; }
        public string PTitle { get; set; }
        public string PDescription { get; set; }
        public string PSkill { get; set; }
        public string PBudget { get; set; }
        public int PExperiance { get; set; }
        public string PQualification { get; set; }
        public string PLocation { get; set; }
        public DateTime PAdd { get; set; }
        public DateTime PEnd { get; set; }
        public string PType { get; set; }
        public string PStatus { get; set; }
        public string PMsg { get; set; }
    }
}