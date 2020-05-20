using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Project.DataAccess.Lib.Interface;
using Project.DataAccess.Lib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Project.Service.Lib.DepartmentService
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IRepository<Department> departmentRepository;
        private readonly ContosouniversityContext context;

        public DepartmentService(IRepository<Department> departmentRepository, ContosouniversityContext context)
        {
            this.departmentRepository = departmentRepository;
            this.context = context;
        }

        public void Create(Department entity)
        {
            SqlParameter name = new SqlParameter("@Name", entity.Name);
            SqlParameter budget = new SqlParameter("@Budget", entity.Budget);
            SqlParameter startDate = new SqlParameter("@StartDate", entity.StartDate);
            SqlParameter instructorID = new SqlParameter("@InstructorID", entity.InstructorId);
            entity.DepartmentId = context.Department
                                  .FromSqlRaw("EXEC dbo.Department_Insert @Name, @Budget, @StartDate, @InstructorID", name, budget, startDate, instructorID)
                                  .Select(d => d.DepartmentId)
                                  .ToList()
                                  .First();
        }

        public void Update(Department entity)
        {
            byte[] rowVersion = context.Department
                                .Where(d => d.DepartmentId == entity.DepartmentId)
                                .Select(c => c.RowVersion)
                                .FirstOrDefault();

            entity.RowVersion = rowVersion;

            SqlParameter departmentID = new SqlParameter("@DepartmentID", entity.DepartmentId);
            SqlParameter name = new SqlParameter("@Name", entity.Name);
            SqlParameter budget = new SqlParameter("@Budget", entity.Budget);
            SqlParameter startDate = new SqlParameter("@StartDate", entity.StartDate);
            SqlParameter instructorID = new SqlParameter("@InstructorID", entity.InstructorId);
            SqlParameter rowVersion_Original = new SqlParameter("@RowVersion_Original", rowVersion);
            context.Database.ExecuteSqlRaw("EXEC Department_Update @DepartmentID, @Name, @Budget, @StartDate, @InstructorID, @RowVersion_Original", departmentID, name, budget, startDate, instructorID, rowVersion_Original);


        }

        public IEnumerable<Department> GetAll(
            Expression<Func<Department, bool>> filter = null,
            Func<IQueryable<Department>, IOrderedQueryable<Department>> orderBy = null)
        {
            return this.departmentRepository.Find(filter, orderBy);
        }

        public IEnumerable<VwDepartmentCourseCount> GetVwDepartmentCourseCount()
        {
            return context.VwDepartmentCourseCount.FromSqlRaw("SELECT * FROM dbo.VwDepartmentCourseCount").ToList();
        }

        public void Delete(Department Entity)
        {
            SqlParameter departmentID = new SqlParameter("@DepartmentID", Entity.DepartmentId);
            SqlParameter rowVersion_Original = new SqlParameter("@RowVersion_Original", Entity.RowVersion);
            context.Database.ExecuteSqlRaw("EXEC dbo.Department_Delete @DepartmentID, @RowVersion_Original", departmentID, rowVersion_Original);
        }
    }
}
