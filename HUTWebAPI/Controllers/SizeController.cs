using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using HUTModels;
using HUTBusinessLayer.API;

namespace HUTWebAPI.Controllers
{
    public class SizeController : ApiController
    {        
        [HttpGet]
        public IHttpActionResult GetByDateRange(int personId, DateTime startDate, DateTime endDate)
        {
            SizeBLL bll = new SizeBLL();
            var sizes = bll.GetByDateRange(personId, startDate, endDate);

            if (sizes == null)
            {
                return Content(HttpStatusCode.NotFound, "Sizes do not exist.");
            }
            else
            {
                return Ok(sizes);
            }
        }

        // POST: api/Size
        [HttpPost]
        public HttpResponseMessage Post(HUTModels.Size model)
        {
            SizeBLL bll = new SizeBLL();
            if (bll.Insert(model))
            {
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            else
            {
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
        }

        // PUT: api/Size/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Size/5
        public void Delete(int id)
        {
        }
    }
}
