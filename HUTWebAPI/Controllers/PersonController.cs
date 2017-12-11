using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using HUTBusinessLayer.API;
using HUTModels;

namespace HUTWebAPI.Controllers
{
    public class PersonController : ApiController
    {
        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            PersonBLL bll = new PersonBLL();
            var person = bll.Get(id);

            if (person == null)
            {
                return Content(HttpStatusCode.NotFound, "Person does not exist.");
            }
            else
            {
                return Ok(person);
            }
        }

        [HttpGet]
        [Route("api/Person/GetSummaryForDay/{id}/date/{date}", Name = "GetSummaryForDay")]
        public IHttpActionResult GetSummaryForDay(int id, DateTime date)
        {
            PersonBLL bll = new PersonBLL();
            var person = bll.GetSummaryForDay(id, date);

            if (person == null)
            {
                return Content(HttpStatusCode.NotFound, "Person does not exist.");
            }
            else
            {
                return Ok(person);
            }
        }

        [HttpGet]
        [Route("api/Person/GetAll", Name = "GetAll")]
        public IHttpActionResult GetAll()
        {
            PersonBLL bll = new PersonBLL();
            var persons = bll.GetAll();

            if (persons == null)
            {
                return Content(HttpStatusCode.NotFound, "No persons exist.");
            }
            else
            {
                return Ok(persons);
            }
        }
    }
}
