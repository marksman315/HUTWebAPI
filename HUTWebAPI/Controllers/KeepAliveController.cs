using System.Net;
using System.Web.Http;
using HUTBusinessLayer.API;

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
            // hit the database just so the objects do not get garbage collected
            PersonBLL bll = new PersonBLL();
            bll.Get(1);

            return Content(HttpStatusCode.OK, "Staying Alive!");            
        }
    }
}