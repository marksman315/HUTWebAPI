using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using HUTBusinessLayer.API;

namespace HUTWebAPI.Controllers
{
    public class CalorieCountOffDayController : ApiController
    {
        /// <summary>
        /// Insert calorie counts and return the total for the current day
        /// </summary>
        /// <param name="model">Calorie Count Model</param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult Post(HUTModels.CalorieCountOffDay model)
        {
            CalorieCountOffDayBLL bll = new CalorieCountOffDayBLL();
            if (bll.Insert(model))
            {
                return Ok(model);
            }
            else
            {
                return Content(HttpStatusCode.InternalServerError, "Problem inserting calorie count day off entry.");
            }
        }
    }
}
