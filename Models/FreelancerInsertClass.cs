using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC_Project_Freelance.Models
{
    public class FreelancerInsertClass
    {
        [Required(ErrorMessage ="Enter your Name please")]
        public string FName { get; set; }
        [Range(18,120, ErrorMessage = "Enter a valid Age please")]
        public int FAge { get; set; }
        [Required(ErrorMessage = "Enter you Phone Number please")]
        [RegularExpression(@"^[6-9][0-9]{9}$", ErrorMessage = "Enter a valid 10-digit phone number")]
        public string FPhone { get; set; }
        [EmailAddress(ErrorMessage ="Enter a valid Email please")]
        public string FEmail { get; set; }
        public string FAddress { get; set; }
        public string FQualification { get; set; }
        public int FExp { get; set; }
        public string FSkills { get; set; }
        public string FPhoto { get; set; }
        public string FResume { get; set; }
        public string FStatus { get; set; }
        public string FMsg { get; set; }
        public string FUsername { get; set; }
        public string FPassword { get; set; }
        [Compare("FPassword", ErrorMessage = "Password Mismatch")]
        public string FCPassword { get; set; }
    }
}