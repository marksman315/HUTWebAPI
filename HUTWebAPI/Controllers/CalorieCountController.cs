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

        /// <summary>
        /// Insert calorie counts and return the total for the current day
        /// </summary>
        /// <param name="model">Calorie Count Model</param>
        /// <returns></returns>
        [HttpPost]        
        public IHttpActionResult Post(HUTModels.CalorieCount model)
        {
            CalorieCountBLL bll = new CalorieCountBLL();
            if (bll.Insert(model))
            {
                DateTime startDateTime = model.DatetimeEntered.Date;
                DateTime endDateTime = model.DatetimeEntered.Date.AddDays(1);
                var calories = bll.GetByDateRange(model.PersonId, startDateTime, endDateTime);

                return Ok(calories);
            }
            else
            {
                return Content(HttpStatusCode.InternalServerError, "Problem inserting or returning calorie counts for the current day.");
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
