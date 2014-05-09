using System.Text;
using System.Web.Mvc;
using Contacts.Infrastructure;

namespace Contacts.Controllers
{
    public class BaseController : Controller
    {
        public JsonResult JsonNet<T>(T data) where T : class
        {
            var jsonNetResult = new JsonNetResult<T>(data)
            {
                ContentEncoding = Encoding.UTF8, 
                ContentType = "application/json", 
                JsonRequestBehavior = JsonRequestBehavior.AllowGet, 
            };
            return jsonNetResult;
        }
    }
}