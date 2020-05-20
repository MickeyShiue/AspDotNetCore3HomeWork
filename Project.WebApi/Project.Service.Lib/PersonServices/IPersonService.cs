using Project.DataAccess.Lib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Project.Service.Lib.PersonServices
{
    public interface IPersonService
    {
        IEnumerable<Person> GetAll(Expression<Func<Person, bool>> filter = null,
           Func<IQueryable<Person>, IOrderedQueryable<Person>> orderBy = null);

        void Create(Person Entity);

        void Update(Person Entity);
    }
}
