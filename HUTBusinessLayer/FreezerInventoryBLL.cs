using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HUTDataAccessLayerSQL;

namespace HUTBusinessLayer.API
{
    public class FreezerInventoryBLL
    {
        private IInsertUpdateDeleteRepository repo;

        #region Constructors

        public FreezerInventoryBLL(IInsertUpdateDeleteRepository repo)
        {
            this.repo = repo;
        }

        public FreezerInventoryBLL()
        {
            this.repo = new InsertUpdateDeleteRepository<HomeUseTrackingEntities>(new HomeUseTrackingEntities());
        }

        #endregion

        #region Public Methods

        public List<HUTModels.FreezerInventory> GetListOfInventory()
        {
            List<HUTModels.FreezerInventory> inventory = repo.GetAll<HUTDataAccessLayerSQL.FreezerInventory>()
                                                            .Select(f => new HUTModels.FreezerInventory()
                                                            {
                                                                FreezerInventoryId = f.FreezerInventoryId,
                                                                ItemName = f.ItemName,
                                                                Quantity = f.Quantity
                                                            })
                                                            .OrderBy(y => y.ItemName)
                                                            .ToList();

            return inventory;
        }

        public bool Insert(HUTModels.FreezerInventory model)
        {
            try
            {
                FreezerInventory inventory = new FreezerInventory() { ItemName = model.ItemName, Quantity = model.Quantity };

                repo.Create(inventory);
                repo.Save();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Update(HUTModels.FreezerInventory model)
        {
            try
            {
                FreezerInventory inventory = new FreezerInventory() { FreezerInventoryId = model.FreezerInventoryId, ItemName = model.ItemName, Quantity = model.Quantity };               

                repo.Update(inventory);
                repo.Save();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Delete(int freezerInventoryId)
        {
            try
            {
                repo.Delete<FreezerInventory>(freezerInventoryId);
                repo.Save();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        #endregion
    }
}
