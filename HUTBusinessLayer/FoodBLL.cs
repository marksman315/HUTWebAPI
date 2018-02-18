﻿using System;
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
    }
}