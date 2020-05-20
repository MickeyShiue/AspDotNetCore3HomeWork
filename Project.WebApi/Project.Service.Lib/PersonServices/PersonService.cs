using Project.DataAccess.Lib.Interface;
using Project.DataAccess.Lib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Project.Service.Lib.PersonServices
{
    public class PersonService : IPersonService
    {
        private readonly IRepository<Person> PersonRepository;

        public PersonService(IRepository<Person> PersonRepository)
        {
            this.PersonRepository = PersonRepository;
    }

        public void Create(Person Entity)
        {
            this.PersonRepository.Create(Entity);
        }

        public IEnumerable<Person> GetAll(Expression<Func<Person, bool>> filter = null, Func<IQueryable<Person>, IOrderedQueryable<Person>> orderBy = null)
        {
            return this.PersonRepository.Find(filter, orderBy);
        }

        public void Update(Person Entity)
        {
            this.PersonRepository.Update(Entity);
        }
    }
}
