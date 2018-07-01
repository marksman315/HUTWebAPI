using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using HUTBusinessLayer.API;

namespace HUTWebAPI.Controllers
{
    public class RecipeController : ApiController
    {
        [HttpGet]
        public IHttpActionResult GetActiveRecipes()
        {
            RecipeBLL bll = new RecipeBLL();
            var recipes = bll.GetListOfRecipes();

            if (recipes == null)
            {
                return Content(HttpStatusCode.NotFound, "List of recipes does not exist.");
            }
            else
            {
                return Ok(recipes);
            }
        }

        [HttpGet]
        [Route("api/Recipe/GetRecipeWithIngredients/{recipeId}", Name = "GetRecipeWithIngredients")]
        public IHttpActionResult GetRecipeWithIngredients(int recipeId)
        {
            RecipeBLL bll = new RecipeBLL();
            var recipes = bll.GetRecipeWithIngredients(recipeId);

            if (recipes == null)
            {
                return Content(HttpStatusCode.NotFound, "Recipe does not exist.");
            }
            else
            {
                return Ok(recipes);
            }
        }
    }
}