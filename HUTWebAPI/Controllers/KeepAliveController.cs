using System.Net;
using System.Web.Http;

namespace HUTWebAPI.Controllers
{
    public class KeepAliveController : ApiController
    {
        /// <summary>
        /// Keeping the api alive
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult Get()
        {            
            return Content(HttpStatusCode.OK, "Staying Alive!");            
        }
    }
}