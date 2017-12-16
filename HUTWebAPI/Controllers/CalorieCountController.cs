using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using HUTBusinessLayer.API;

namespace HUTWebAPI.Controllers
{
    public class CalorieCountController : ApiController
    {
        [HttpGet]
        public IHttpActionResult GetByDateRange(int personId, DateTime startDate, DateTime endDate)
        {
            CalorieCountBLL bll = new CalorieCountBLL();
            var calories = bll.GetByDateRange(personId, startDate, endDate);

            if (calories == null)
            {
                return Content(HttpStatusCode.NotFound, "Calorie counts do not exist.");
            }
            else
            {
                return Ok(calories);
            }
        }

        [HttpPost]        
        public HttpResponseMessage Post(HUTModels.CalorieCount model)
        {
            CalorieCountBLL bll = new CalorieCountBLL();
            if (bll.Insert(model))
            {
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            else
            {
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
        }

        // PUT: api/CalorieCount/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/CalorieCount/5
        public void Delete(int id)
        {
        }
    }
}
