using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using HUTBusinessLayer.API;

namespace HUTWebAPI.Controllers
{
    public class FoodController : ApiController
    {
        [HttpGet]
        [Route("api/Food/GetListOfFoods", Name = "GetListOfFoods")]
        public IHttpActionResult GetListOfFoods()
        {
            FoodBLL bll = new FoodBLL();
            var foods = bll.GetListOfFoods();

            if (foods == null)
            {
                return Content(HttpStatusCode.NotFound, "List of foods do not exist.");
            }
            else
            {
                return Ok(foods);
            }
        }
        
        [HttpPost]
        public IHttpActionResult Post(HUTModels.Food model)
        {
            FoodBLL bll = new FoodBLL();
            if (bll.Insert(model))
            {                               
                return Ok();
            }
            else
            {
                return Content(HttpStatusCode.InternalServerError, "Problem inserting food record.");
            }
        }

        [HttpPut]
        public IHttpActionResult Put(HUTModels.Food model)
        {
            FoodBLL bll = new FoodBLL();
            if (bll.Update(model))
            {
                return Ok();
            }
            else
            {
                return Content(HttpStatusCode.InternalServerError, "Problem updating food record.");
            }
        }        
    }
}