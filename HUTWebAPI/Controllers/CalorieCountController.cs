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
        // GET: api/CalorieCount
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/CalorieCount/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/CalorieCount
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
