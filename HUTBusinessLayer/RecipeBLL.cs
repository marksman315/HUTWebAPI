using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HUTDataAccessLayerSQL;

namespace HUTBusinessLayer.API
{
    public class RecipeBLL
    {
        private IInsertUpdateDeleteRepository repo;

        #region Constructors

        public RecipeBLL(IInsertUpdateDeleteRepository repo)
        {
            this.repo = repo;
        }

        public RecipeBLL()
        {
            this.repo = new InsertUpdateDeleteRepository<HomeUseTrackingEntities>(new HomeUseTrackingEntities());
        }

        #endregion

        public List<HUTModels.Recipe> GetListOfRecipes()
        {
            List<HUTModels.Recipe> recipes = repo.GetAll<HUTDataAccessLayerSQL.Recipe>()
                                                    .Select(r => new HUTModels.Recipe()
                                                    {
                                                        Archived = r.Archived,
                                                        DateEntered = r.DateEntered,
                                                        Description = r.Description,
                                                        RecipeId = r.RecipeId
                                                    })
                                                    .OrderByDescending(y => y.Description)
                                                    .OrderByDescending(x => x.DateEntered)
                                                    .ToList();

            return recipes;
        }

        public HUTModels.Recipe Get(int recipeId)
        {
            HUTDataAccessLayerSQL.Recipe recipe = repo.GetById<HUTDataAccessLayerSQL.Recipe>(recipeId);
            HUTModels.Recipe model = new HUTModels.Recipe() {
                                                                Archived = recipe.Archived,
                                                                DateEntered = recipe.DateEntered,
                                                                Description = recipe.Description,
                                                                RecipeId = recipe.RecipeId,
                                                                Ingredients = recipe.Ingredients.Select(x => new HUTModels.Ingredient()
                                                                                                                {
                                                                                                                    FoodId = x.FoodId,
                                                                                                                    IngredientId = x.IngredientId,
                                                                                                                    RecipeId = x.RecipeId,
                                                                                                                    Weight = x.Weight
                                                                                                                })
                                                                                                                .OrderByDescending(y => y.Weight)
                                                                                                                .ToList()
            };

            return model;
        }

        public bool Insert(HUTModels.Recipe model)
        {
            try
            {
                Recipe recipe = new Recipe() { Archived = model.Archived, DateEntered = model.DateEntered, Description = model.Description };

                repo.Create(recipe);
                repo.Save();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
