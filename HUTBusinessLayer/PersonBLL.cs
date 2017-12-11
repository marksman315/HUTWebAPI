using System;
using System.Collections.Generic;
using System.Linq;
using HUTDataAccessLayerSQL;

namespace HUTBusinessLayer.API
{
    public class PersonBLL
    {
        private IReadOnlyRepository repo;

        #region Constructors

        public PersonBLL(IReadOnlyRepository repo)
        {
            this.repo = repo;
        }

        public PersonBLL()
        {
            this.repo = new ReadOnlyRepository<HomeUseTrackingEntities>(new HomeUseTrackingEntities());
        }

        #endregion

        public HUTModels.Person Get(int id)
        {
            HUTDataAccessLayerSQL.Person person = repo.GetById<HUTDataAccessLayerSQL.Person>(id);
            HUTModels.Person model = new HUTModels.Person() { PersonId = person.PersonId, Firstname = person.Firstname, Lastname = person.Lastname };
                                         
            return model;
        }

        public HUTModels.Person GetSummaryForDay(int id, DateTime date)
        {
            HUTModels.Person person = Get(id);

            // get child data
            SizeBLL sizeBLL = new SizeBLL();
            person.Sizes = sizeBLL.GetByDateRange(id, date, date.AddDays(1));

            WeightBLL weightBLL = new WeightBLL();
            person.Weights = weightBLL.GetByDateRange(id, date, date.AddDays(1));

            CalorieCountBLL countBLL = new CalorieCountBLL();
            person.CalorieCounts = countBLL.GetByDateRange(id, date, date.AddDays(1));

            return person;
        }

        public List<HUTModels.Person> GetAll()
        {           
            List<HUTModels.Person> persons = repo.GetAll<HUTDataAccessLayerSQL.Person>()
                                                .Select(x => new HUTModels.Person() { PersonId = x.PersonId, Firstname = x.Firstname, Lastname = x.Lastname })
                                                .ToList();

            return persons;
        }
    }
}
