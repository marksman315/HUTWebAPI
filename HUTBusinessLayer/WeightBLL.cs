using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HUTDataAccessLayerSQL;

namespace HUTBusinessLayer.API
{
    public class WeightBLL
    {
        private IInsertUpdateDeleteRepository repo;

        #region Constructors

        public WeightBLL(IInsertUpdateDeleteRepository repo)
        {
            this.repo = repo;
        }

        public WeightBLL()
        {
            this.repo = new InsertUpdateDeleteRepository<HomeUseTrackingEntities>(new HomeUseTrackingEntities());
        }

        #endregion

        public List<HUTModels.Weight> GetByDateRange(int personId, DateTime startDate, DateTime endDate)
        {
            List<HUTModels.Weight> weight = repo.Get<Weight>(w => w.PersonId == personId
                                                                && w.DateEntered >= startDate.Date
                                                                && w.DateEntered <= endDate.Date,
                                                                orderBy: o => o.OrderBy(w => w.DateEntered)
                                                            )
                                                            .Select(x => new HUTModels.Weight()
                                                                                            {
                                                                                                WeightId = x.WeightId,
                                                                                                PersonId = x.PersonId,
                                                                                                DateEntered = x.DateEntered,
                                                                                                WeightAmount = x.WeightAmount
                                                                                            })
                                                            .ToList();

            return weight;
        }

        public bool Insert(HUTModels.Weight model)
        {
            try
            {
                Weight weight = new Weight() { PersonId = model.PersonId, DateEntered = model.DateEntered, WeightAmount = model.WeightAmount };

                repo.Create(weight);
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
