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
    }
}
