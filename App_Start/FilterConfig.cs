using System.Web;
using System.Web.Mvc;

namespace MVC_Project_Freelance
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
