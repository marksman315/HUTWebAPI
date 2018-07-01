using System;
using System.Collections.Generic;
using System.Linq;
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

        public HUTModels.Recipe GetRecipeWithIngredients(int recipeId)
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

                if (model.Ingredients?.Count > 0)
                {
                    int? recipeId = GetIdByDescriptionAndDateEntered(model.Description, model.DateEntered);

                    if (recipeId != null)
                    {
                        IngredientBLL bll = new IngredientBLL(this.repo);

                        foreach (HUTModels.Ingredient ingredient in model.Ingredients)
                        {
                            ingredient.RecipeId = (int)recipeId;
                            bll.Insert(ingredient);
                        }
                    }
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Update(HUTModels.Recipe model)
        {
            try
            {
                repo.Update(model);
                repo.Save();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Delete(int recipeId)
        {
            try
            {
                // delete all the child ingredients first by getting them and looping to delete them all (not very efficient)
                // there needs to be a better way to do this en masse
                HUTModels.Recipe recipe = GetRecipeWithIngredients(recipeId);

                IngredientBLL bll = new IngredientBLL(this.repo);

                foreach (HUTModels.Ingredient ingredient in recipe.Ingredients)
                {
                    bll.Delete(ingredient.IngredientId);
                }

                repo.Delete<Recipe>(recipeId);
                repo.Save();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private int? GetIdByDescriptionAndDateEntered(string description, DateTime dateEntered)
        {
            int? recipeId = repo.GetOne<HUTDataAccessLayerSQL.Recipe>(x => x.Description == description && x.DateEntered == dateEntered).RecipeId;
                                                    
            return recipeId;
        }
    }
}
