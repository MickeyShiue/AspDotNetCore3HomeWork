using Project.DataAccess.Lib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Project.Service.Lib.CourseServices
{
    public interface ICourseService
    {
        IEnumerable<Course> GetAll(Expression<Func<Course, bool>> filter = null,
            Func<IQueryable<Course>, IOrderedQueryable<Course>> orderBy = null);

        void Create(Course course);

        void Update(Course course);

        IEnumerable<VwCourseStudentCount> GetVwCourseStudentCount();

        IEnumerable<VwCourseStudents> GetVwCourseStudents();
    }
}
