using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HUTDataAccessLayerSQL;

namespace HUTBusinessLayer.API
{
    public class SizeBLL
    {
        private IInsertUpdateDeleteRepository repo;

        #region Constructors

        public SizeBLL(IInsertUpdateDeleteRepository repo)
        {
            this.repo = repo;
        }

        public SizeBLL()
        {
            this.repo = new InsertUpdateDeleteRepository<HomeUseTrackingEntities>(new HomeUseTrackingEntities());
        }

        #endregion

        public List<HUTModels.Size> GetByDateRange(int personId, DateTime startDate, DateTime endDate)
        {
            List<HUTModels.Size> sizes = repo.Get<Size>(s => s.PersonId == personId
                                                            && s.DateEntered >= startDate.Date
                                                            && s.DateEntered <= endDate.Date,
                                                            orderBy: o => o.OrderBy(s => s.DateEntered)
                                                        )
                                                        .Select(x => new HUTModels.Size()
                                                                                        {
                                                                                            SizeId = x.SizeId,
                                                                                            PersonId = x.PersonId,
                                                                                            DateEntered = x.DateEntered,
                                                                                            Bicep = x.Bicep,
                                                                                            Stomach = x.Stomach
                                                                                        })
                                                        .ToList();

            return sizes;
        }

        public bool Insert(HUTModels.Size model)
        {
            try
            {
                Size size = new Size()
                                    {
                                        PersonId = model.PersonId,
                                        DateEntered = model.DateEntered,
                                        Bicep = model.Bicep,
                                        Stomach = model.Stomach
                                    };

                repo.Create(size);
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
