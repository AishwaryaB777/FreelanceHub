using System;
using System.Collections.Generic;

namespace MVC_Project_Freelance.Models
{
    public class ApplicantDetailsView
    {
        public int A_Id { get; set; }
        public int PId { get; set; }

        public string PTitle { get; set; }
        public string PBudget { get; set; }

        public DateTime ADate { get; set; }
        public string AProposal { get; set; }
        public string AExpected_Amount { get; set; }
        public string AResume { get; set; }
        public int AStatus { get; set; }

        public int FId { get; set; }
        public string FName { get; set; }
        public int FAge { get; set; }
        public string FPhone { get; set; }
        public string FEmail { get; set; }
        public string FAddress { get; set; }
        public string FQualification { get; set; }
        public int FExp { get; set; }
        public string FSkills { get; set; }
        public string FPhoto { get; set; }
        public string FResume { get; set; }

        public string DRemarks { get; set; }
        public DateTime? DDate { get; set; }
    }
}
