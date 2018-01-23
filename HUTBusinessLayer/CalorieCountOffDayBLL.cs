using System;
using System.Collections.Generic;
using System.Linq;
using HUTDataAccessLayerSQL;

namespace HUTBusinessLayer.API
{
    public class CalorieCountOffDayBLL
    {
        private IInsertUpdateDeleteRepository repo;

        #region Constructors

        public CalorieCountOffDayBLL(IInsertUpdateDeleteRepository repo)
        {
            this.repo = repo;
        }

        public CalorieCountOffDayBLL()
        {
            this.repo = new InsertUpdateDeleteRepository<HomeUseTrackingEntities>(new HomeUseTrackingEntities());
        }

        #endregion

        public List<HUTModels.CalorieCountOffDay> GetDaysOffInDateRange(int personId, DateTime startDate, DateTime endDate)
        {
            List<HUTModels.CalorieCountOffDay> counts = repo.Get<CalorieCountOffDay>(c => c.PersonId == personId
                                                                                        && c.DateEntered >= startDate.Date
                                                                                        && c.DateEntered <= endDate.Date,
                                                                                        orderBy: o => o.OrderBy(c => c.DateEntered)
                                                                                    )
                                                                                    .Select(x => new HUTModels.CalorieCountOffDay()
                                                                                    {
                                                                                        CalorieCountOffDayId = x.CalorieCountOffDayId,
                                                                                        PersonId = x.PersonId,
                                                                                        DateEntered = x.DateEntered.Date                                       
                                                                                    })
                                                                                    .ToList();

            return counts;
        }

        public bool Insert(HUTModels.CalorieCountOffDay model)
        {
            try
            {
                CalorieCountOffDay countOffDay = new CalorieCountOffDay() { PersonId = model.PersonId, DateEntered = model.DateEntered };

                repo.Create(countOffDay);
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
