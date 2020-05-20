using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Project.DataAccess.Lib.Models;
using Project.DataAccess.Lib.UnitOfWork;
using Project.Service.Lib.CourseServices;

namespace Project.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly ICourseService courseService;
        private readonly IUnitOfWork unitOfWork;

        public CourseController(ICourseService courseService,
            IUnitOfWork unitOfWork)
        {
            this.courseService = courseService;
            this.unitOfWork = unitOfWork;
        }

        // GET: api/Course
        [HttpGet]
        public ActionResult<IEnumerable<Course>> GetCourse()
        {
            return courseService.GetAll(r => r.IsDeleted == false || r.IsDeleted == null).ToList();
        }

        // GET: api/Course/{id}
        [HttpGet("{id}")]
        public ActionResult<Course> GetCourse(int id)
        {
            var course = this.courseService.GetAll(
                r => (r.IsDeleted == false || r.IsDeleted == null) && r.CourseId == id).FirstOrDefault();

            if (course == null)
            {
                return NotFound("此筆資料已刪除或不存在");
            }

            return course;
        }

        [HttpPut("{id}")]
        public IActionResult PutCourse(int id, Course course)
        {
            course.DateModified = DateTime.Now;
            this.courseService.Update(course);

            try
            {
                unitOfWork.SaveChange();
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new
                {
                    errorno = 1,
                    message = "我懶得處理，略..."
                });
            }

            return NoContent();
        }

        [HttpPost]
        public ActionResult<Course> PostCourse(Course course)
        {
            this.courseService.Create(course);

            try
            {
                unitOfWork.SaveChange();
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new
                {
                    message = "我懶得處理，略..."
                });
            }

            return Ok(course);
        }

        [HttpDelete("{id}")]
        public ActionResult<Course> DeleteCourse(int id)
        {
            var course = this.courseService.GetAll(r => r.CourseId == id).FirstOrDefault();
            if (course == null)
            {
                return NotFound();
            }
            course.IsDeleted = true;

            this.courseService.Update(course);
            unitOfWork.SaveChange();
            return course;
        }

        [HttpGet("GetCourseStudentCount")]
        public ActionResult<IEnumerable<VwCourseStudentCount>> GetCourseStudentCount()
        {
            return this.courseService.GetVwCourseStudentCount().ToList();
        }

        [HttpGet("GetCourseStudents")]
        public ActionResult<IEnumerable<VwCourseStudents>> GetCourseStudents()
        {
            return this.courseService.GetVwCourseStudents().ToList();
        }
    }
}