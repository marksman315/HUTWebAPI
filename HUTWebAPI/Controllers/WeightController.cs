using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using HUTBusinessLayer.API;

namespace HUTWebAPI.Controllers
{
    public class WeightController : ApiController
    {
        [HttpGet]
        public IHttpActionResult GetByDateRange(int personId, DateTime startDate, DateTime endDate)
        {
            WeightBLL bll = new WeightBLL();
            var weights = bll.GetByDateRange(personId, startDate, endDate);

            if (weights == null)
            {
                return Content(HttpStatusCode.NotFound, "Weights do not exist.");
            }
            else
            {
                return Ok(weights);
            }
        }

        [HttpPost]
        public HttpResponseMessage Post(HUTModels.Weight model)
        {
            WeightBLL bll = new WeightBLL();
            if (bll.Insert(model))
            {
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            else
            {
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
        }

        // PUT: api/Weight/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Weight/5
        public void Delete(int id)
        {
        }
    }
}
