using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HUTDataAccessLayerSQL;

namespace HUTBusinessLayer.API
{
    public class CalorieCountBLL
    {
        private IInsertUpdateDeleteRepository repo;

        #region Constructors

        public CalorieCountBLL(IInsertUpdateDeleteRepository repo)
        {
            this.repo = repo;
        }

        public CalorieCountBLL()
        {
            this.repo = new InsertUpdateDeleteRepository<HomeUseTrackingEntities>(new HomeUseTrackingEntities());
        }

        #endregion

        public List<HUTModels.CalorieCount> GetByDateRange(int personId, DateTime startDate, DateTime endDate)
        {
            List<HUTModels.CalorieCount> counts = repo.Get<CalorieCount>(c => c.PersonId == personId
                                                                            && c.DatetimeEntered >= startDate.Date
                                                                            && c.DatetimeEntered <= endDate.Date,
                                                                            orderBy: o => o.OrderBy(c => c.DatetimeEntered)
                                                                        )
                                                                        .Select(x => new HUTModels.CalorieCount()
                                                                                                                {
                                                                                                                    CalorieCountId = x.CalorieCountId,
                                                                                                                    PersonId = x.PersonId,
                                                                                                                    DatetimeEntered = x.DatetimeEntered,
                                                                                                                    Calories = x.Calories
                                                                                                                })
                                                                        .ToList();

            return counts;
        }

        public List<HUTModels.CalorieCount> GetTotalsPerDayInDateRange(int personId, DateTime startDate, DateTime endDate)
        {
            List<HUTModels.CalorieCount> counts = GetByDateRange(personId, startDate, endDate);

            // group by the date, without the time, and sum up the calories
            List<HUTModels.CalorieCount> countsPerDay = counts.GroupBy(g => new {
                                                                            g.DatetimeEntered.Date,
                                                                            g.PersonId
                                                                        })
                                                                        .Select(group => new HUTModels.CalorieCount()
                                                                        {
                                                                            CalorieCountId = 0,
                                                                            PersonId = group.Key.PersonId,
                                                                            DatetimeEntered = group.Key.Date,
                                                                            Calories = group.Sum(c => c.Calories)
                                                                        }).ToList();

            return countsPerDay;
        }

        public bool Insert(HUTModels.CalorieCount model)
        {
            try
            {
                CalorieCount count = new CalorieCount() { PersonId = model.PersonId, DatetimeEntered = model.DatetimeEntered, Calories = model.Calories };

                repo.Create(count);
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
