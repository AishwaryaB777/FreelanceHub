using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC_Project_Freelance.Models
{
    public class ClientInsertClass
    {
        [Required(ErrorMessage = "Enter your Name please")]
        public string CName { get; set; }
        [Required(ErrorMessage = "Enter you Phone Number please")]
        [RegularExpression(@"^[6-9][0-9]{9}$", ErrorMessage = "Enter a valid 10-digit phone number")]
        public string CPhone { get; set; }
        [EmailAddress(ErrorMessage = "Enter a valid Email please")]
        public string CEmail { get; set; }
        public string CAddress { get; set; }
        public string CCompany_Name { get; set; }
        public string CC_Description { get; set; }
        public string CPhoto { get; set; }
        public string CStatus { get; set; }
        public string CMsg { get; set; }
        public string CUsername { get; set; }
        public string CPassword { get; set; }
        [Compare("CPassword", ErrorMessage = "Password Mismatch")]
        public string CCPassword { get; set; }
    }
}