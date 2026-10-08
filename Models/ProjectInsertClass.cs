using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC_Project_Freelance.Models
{
    public class ProjectInsertClass
    {
        public int CId { get; set; }
        [Required(ErrorMessage = "Enter your Name please")]
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