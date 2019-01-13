using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using HUTBusinessLayer.API;

namespace HUTWebAPI.Controllers
{
    public class FreezerInventoryController : ApiController
    {
        [HttpGet]
        [Route("api/FreezerInventory/GetListOfInventory", Name = "GetListOfIventory")]
        public IHttpActionResult GetListOfInventory()
        {
            FreezerInventoryBLL bll = new FreezerInventoryBLL();
            var foods = bll.GetListOfInventory();

            if (foods == null)
            {
                return Content(HttpStatusCode.NotFound, "List of freezer inventory do not exist.");
            }
            else
            {
                return Ok(foods);
            }
        }
               
        [HttpPost]
        public IHttpActionResult Post(HUTModels.FreezerInventory model)
        {
            FreezerInventoryBLL bll = new FreezerInventoryBLL();
            if (bll.Insert(model))
            {                               
                return Ok();
            }
            else
            {
                return Content(HttpStatusCode.InternalServerError, "Problem inserting freezer inventory record.");
            }
        }        

        [HttpPut]
        public IHttpActionResult Put(HUTModels.FreezerInventory model)
        {
            FreezerInventoryBLL bll = new FreezerInventoryBLL();
            if (bll.Update(model))
            {
                return Ok();
            }
            else
            {
                return Content(HttpStatusCode.InternalServerError, "Problem updating freezer inventory record.");
            }
        }

        [HttpDelete]
        public IHttpActionResult Delete(int freezerInventoryId)
        {
            FreezerInventoryBLL bll = new FreezerInventoryBLL();
            if (bll.Delete(freezerInventoryId))
            {
                return Ok();
            }
            else
            {
                return Content(HttpStatusCode.InternalServerError, "Problem deleting freezer inventory record.");
            }
        }
    }
}