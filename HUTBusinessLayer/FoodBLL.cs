using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using HUTBusinessLayer.API.Helpers;
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

        #region Public Methods

        public List<HUTModels.Food> GetListOfFoods()
        {
            List<HUTModels.Food> foods = repo.GetAll<HUTDataAccessLayerSQL.Food>()
                                                .Select(f => new HUTModels.Food()
                                                {
                                                    CaloriesPer100Grams = f.CaloriesPer100Grams,
                                                    Description = f.Description,
                                                    FoodId = f.FoodId,
                                                    PhotoURL = f.PhotoURL
                                                })
                                                .OrderBy(y => y.Description)
                                                .ToList();

            return foods;
        }

        public List<HUTModels.Food> GetUnlistedFood(string foodName)
        {
            // need webservice call to get new food
            HUTModels.Food food = GetFoodFromService(foodName).Result;

            // need to insert record into database
            if (Insert(food))
            {
                // need to return new food with id
                List<HUTModels.Food> foods = GetFood(foodName);

                return foods;
            }
            
            return null;            
        }

        public HUTModels.Food RetrieveAndUpdatePhotoURL(HUTModels.Food model)
        {
            HUTModels.Food food = GetFoodFromService(model.Description).Result;

            // if a photo url is found, update our database with the value
            if (!string.IsNullOrEmpty(food.PhotoURL))
            {
                model.PhotoURL = food.PhotoURL;
                Update(model);
            }

            return model;
        }

        public bool Insert(HUTModels.Food model)
        {
            try
            {
                Food food = new Food() { CaloriesPer100Grams = model.CaloriesPer100Grams, Description = model.Description, PhotoURL = model.PhotoURL };

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

                // checking separately so we don't accidentally delete the URL while doing some testing
                if (model.PhotoURL != null)
                {
                    food.PhotoURL = model.PhotoURL;
                }

                repo.Update(food);
                repo.Save();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        #endregion

        #region Private Methods

        private async Task<HUTModels.Food> GetFoodFromService(string foodName)
        {
            Uri uri = new Uri("https://trackapi.nutritionix.com/v2/natural/nutrients");

            string json = await HTTPHelper.PostValues(uri, SetQueryModel(foodName), SetCustomHeaders()).ConfigureAwait(false);

            JObject jsonObject = JObject.Parse(json);            

            JArray foods =  (JArray)jsonObject["foods"];
            int calories = Convert.ToInt32(foods[0]["nf_calories"]);
            string highres = (string)foods[0]["photo"]["highres"];

            HUTModels.Food food = new HUTModels.Food();
            food.CaloriesPer100Grams = Convert.ToInt32(calories);
            food.Description = foodName;
            food.PhotoURL = highres;

            return food;
        }

        private List<HUTModels.Food> GetFood(string foodName)
        {
            List<HUTModels.Food> foods = repo.GetAll<HUTDataAccessLayerSQL.Food>()
                                                .Select(f => new HUTModels.Food()
                                                {
                                                    CaloriesPer100Grams = f.CaloriesPer100Grams,
                                                    Description = f.Description,
                                                    FoodId = f.FoodId, 
                                                    PhotoURL = f.PhotoURL
                                                })
                                                .Where(y => y.Description == foodName)
                                                .OrderBy(y => y.Description)
                                                .ToList();

            return foods;
        }

        private Dictionary<string, string> SetCustomHeaders()
        {
            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add("x-app-id", "f00f8b0d");
            headers.Add("x-app-key", "92d014ef8c9f1847c456adf89b4b9f7c");
            headers.Add("x-remote-user-id", "0");

            return headers;
        }

        private string SetQueryModel(string foodName)
        {
            string queryString = "{" + string.Format("\"query\" : \"{0} 100 grams\"", foodName) + "}";
            return queryString;
        }

        #endregion
    }
}
