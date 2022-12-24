using SMShop.Models;
using System.Web;
using System.Web.Mvc;

namespace SMShop
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new ProductFilter()); // Add this line
            filters.Add(new HandleErrorAttribute());
           
        
        }
    }
}
