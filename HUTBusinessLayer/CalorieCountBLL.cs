using System;
using System.Collections.Generic;
using System.Linq;
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

            countsPerDay = AdjustForOffDays(personId, startDate, endDate, countsPerDay);

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

        private List<HUTModels.CalorieCount> AdjustForOffDays(int personId, DateTime startDate, DateTime endDate, List<HUTModels.CalorieCount> countsPerDay)
        {
            List<HUTModels.CalorieCountOffDay> offDays = GetCalorieCountOffDays(personId, startDate, endDate);

            if (offDays?.Count > 0)
            {
                foreach (var offDay in offDays)
                {
                    foreach (var count in countsPerDay.Where(c => c.DatetimeEntered.Date == offDay.DateEntered))
                    {
                        // 2000 is an arbitrary number that will show on the chart and help identify which bars need to display with a different color
                        count.Calories = 2000;
                    }
                }

                List<HUTModels.CalorieCount> newCountsPerDay = new List<HUTModels.CalorieCount>();

                for (int i = 0; i < countsPerDay.Count; i++)
                {
                    newCountsPerDay.Add(countsPerDay[i]);

                    foreach (var offDay in offDays)
                    {
                        if (i == 0)
                        {
                            if (offDay.DateEntered < countsPerDay[i].DatetimeEntered.Date)
                            {
                                newCountsPerDay.Insert(0, new HUTModels.CalorieCount()
                                                                        {
                                                                            CalorieCountId = 0,
                                                                            Calories = 2000,
                                                                            DatetimeEntered = offDay.DateEntered,
                                                                            PersonId = personId
                                                                        });
                            }
                        }
                        else if (i == countsPerDay.Count - 1)
                        {
                            if (offDay.DateEntered > countsPerDay[i].DatetimeEntered.Date)
                            {
                                newCountsPerDay.Add(new HUTModels.CalorieCount()
                                                                        {
                                                                            CalorieCountId = 0,
                                                                            Calories = 2000,
                                                                            DatetimeEntered = offDay.DateEntered,
                                                                            PersonId = personId
                                                                        });
                            }
                        }
                        else
                        {
                            if (offDay.DateEntered > countsPerDay[i].DatetimeEntered.Date && offDay.DateEntered < countsPerDay[i + 1].DatetimeEntered.Date)
                            {
                                newCountsPerDay.Add(new HUTModels.CalorieCount()
                                                                        {
                                                                            CalorieCountId = 0,
                                                                            Calories = 2000,
                                                                            DatetimeEntered = offDay.DateEntered,
                                                                            PersonId = personId
                                                                        });
                            }
                        }
                    }                    
                }

                return newCountsPerDay;
            }

            return countsPerDay;
        }

        private List<HUTModels.CalorieCountOffDay> GetCalorieCountOffDays(int personId, DateTime startDate, DateTime endDate)
        {
            CalorieCountOffDayBLL bll = new CalorieCountOffDayBLL(this.repo);

            List<HUTModels.CalorieCountOffDay> offDays = bll.GetDaysOffInDateRange(personId, startDate, endDate);

            return offDays;
        }
    }
}
