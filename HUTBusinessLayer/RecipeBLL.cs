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
