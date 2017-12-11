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
        // GET: api/Weight
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Weight/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Weight
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
