using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_Project_Freelance.Models
{
    public class ProjectApplicationInsertClass
    {
        public int FId { get; set; }
        public int PId { get; set; }
        public DateTime ADate { get; set; }
        public string AProposal { get; set; }
        public string AExpected_Amount { get; set; }
        public string AResume { get; set; }
        public int AStatus { get; set; }
    }
}