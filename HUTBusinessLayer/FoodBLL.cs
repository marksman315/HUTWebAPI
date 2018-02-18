using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HUTDataAccessLayerSQL;

namespace HUTBusinessLayer.API
{
    public class FoodBLL
    {
        private IInsertUpdateDeleteRepository repo;

        #region Constructors

        public FoodBLL(IInsertUpdateDeleteRepository repo)
        {
            this.repo = repo;
        }

        public FoodBLL()
        {
            this.repo = new InsertUpdateDeleteRepository<HomeUseTrackingEntities>(new HomeUseTrackingEntities());
        }

        #endregion

        public List<HUTModels.Food> GetListOfFoods()
        {
            List<HUTModels.Food> foods = repo.GetAll<HUTDataAccessLayerSQL.Food>()
                                                .Select(f => new HUTModels.Food()
                                                {
                                                    CaloriesPer100Grams = f.CaloriesPer100Grams,
                                                    Description = f.Description,
                                                    FoodId = f.FoodId
                                                })
                                                .OrderBy(y => y.Description)
                                                .ToList();

            return foods;
        }

        public bool Insert(HUTModels.Food model)
        {
            try
            {
                Food food = new Food() { CaloriesPer100Grams = model.CaloriesPer100Grams, Description = model.Description };

                repo.Create(food);
                repo.Save();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Update(HUTModels.Food model)
        {
            try
            {
                Food food = new Food() { CaloriesPer100Grams = model.CaloriesPer100Grams, Description = model.Description, FoodId = model.FoodId };

                repo.Update(food);
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
