
using System.Collections.Generic;

namespace MVC_Project_Freelance.Models
{
    public class ClientApplicantsPageView
    {
        public List<ApplicantDetailsView> Applicants { get; set; }
            = new List<ApplicantDetailsView>();

        public string Filter { get; set; } = "All";

        public int TotalCount { get; set; }
        public int PendingCount { get; set; }
        public int AcceptedCount { get; set; }
        public int RejectedCount { get; set; }

        public string Msg { get; set; }
        public string MsgType { get; set; }
    }
}
