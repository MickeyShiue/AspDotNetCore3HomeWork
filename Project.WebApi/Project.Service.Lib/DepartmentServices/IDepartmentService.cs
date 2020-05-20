using Project.DataAccess.Lib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Project.Service.Lib.DepartmentService
{
    public interface IDepartmentService
    {
        IEnumerable<Department> GetAll(Expression<Func<Department, bool>> filter = null,
          Func<IQueryable<Department>, IOrderedQueryable<Department>> orderBy = null);

        void Create(Department Entity);

        void Update(Department Entity);

        void Delete(Department Entity);

        IEnumerable<VwDepartmentCourseCount> GetVwDepartmentCourseCount();
    }
}
