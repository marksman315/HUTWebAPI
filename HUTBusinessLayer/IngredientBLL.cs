using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HUTDataAccessLayerSQL;

namespace HUTBusinessLayer.API
{
    public class IngredientBLL
    {
        private IInsertUpdateDeleteRepository repo;

        #region Constructors

        public IngredientBLL(IInsertUpdateDeleteRepository repo)
        {
            this.repo = repo;
        }

        public IngredientBLL()
        {
            this.repo = new InsertUpdateDeleteRepository<HomeUseTrackingEntities>(new HomeUseTrackingEntities());
        }

        #endregion

        public List<HUTModels.Ingredient> GetPerRecipe(int recipeId)
        {
            List<HUTModels.Ingredient> ingredients = repo.Get<HUTDataAccessLayerSQL.Ingredient>()
                                                    .Select(x => new HUTModels.Ingredient()
                                                    {
                                                        FoodId = x.FoodId,
                                                        IngredientId = x.IngredientId,
                                                        RecipeId = x.RecipeId,
                                                        Weight = x.Weight
                                                    })
                                                    .Where(y => y.RecipeId == recipeId)
                                                    .OrderByDescending(z => z.Weight)
                                                    .ToList();
                                                    
            return ingredients;
        }

        public bool Insert(HUTModels.Ingredient model)
        {
            try
            {
                Ingredient ingredient = new Ingredient() { FoodId = model.FoodId, RecipeId = model.RecipeId, Weight = model.Weight };

                repo.Create(ingredient);
                repo.Save();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Update(HUTModels.Ingredient model)
        {
            try
            {
                Ingredient ingredient = new Ingredient() { FoodId = model.FoodId, RecipeId = model.RecipeId, Weight = model.Weight, IngredientId = model.IngredientId };

                repo.Update(ingredient);
                repo.Save();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Delete(int ingredientId)
        {
            try
            {
                repo.Delete<Ingredient>(ingredientId);
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
