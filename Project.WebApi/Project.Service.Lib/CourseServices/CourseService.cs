using Project.DataAccess.Lib.Interface;
using Project.DataAccess.Lib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Project.Service.Lib.CourseServices
{
    public class CourseService : ICourseService
    {
        private readonly IRepository<Course> courseRepository;
        private readonly ContosouniversityContext context;

        public CourseService(
            IRepository<Course> courseRepository,
            ContosouniversityContext context)
        {
            this.courseRepository = courseRepository;
            this.context = context;
        }

        public void Create(Course entity)
        {
            this.courseRepository.Create(entity);
        }

        public IEnumerable<Course> GetAll
            (Expression<Func<Course, bool>> filter = null,
            Func<IQueryable<Course>, IOrderedQueryable<Course>> orderBy = null)
        {
            return this.courseRepository.Find(filter, orderBy);
        }

        public void Update(Course entity)
        {
            this.courseRepository.Update(entity);
        }

        public IEnumerable<VwCourseStudentCount> GetVwCourseStudentCount()
        {
            return context.VwCourseStudentCount.AsEnumerable();
        }

        public IEnumerable<VwCourseStudents> GetVwCourseStudents()
        {
            return context.VwCourseStudents.AsEnumerable();
        }
    }
}
