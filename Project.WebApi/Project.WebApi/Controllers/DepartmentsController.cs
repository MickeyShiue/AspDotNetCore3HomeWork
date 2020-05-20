using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Project.DataAccess.Lib.Models;
using Project.DataAccess.Lib.UnitOfWork;
using Project.Service.Lib.DepartmentService;

namespace Project.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        private readonly IDepartmentService departmentService;
        private readonly IUnitOfWork unitOfWork;

        public DepartmentsController(ContosouniversityContext context,
            IDepartmentService departmentService,
            IUnitOfWork unitOfWork)
        {     
            this.departmentService = departmentService;
            this.unitOfWork = unitOfWork;
        }

        // GET: api/Departments
        [HttpGet]
        public ActionResult<IEnumerable<Department>> GetDepartment()
        {
            return this.departmentService.GetAll(r => r.IsDeleted == null || r.IsDeleted == false).ToList();
        }

        // GET: api/Departments/5
        [HttpGet("{id}")]
        public ActionResult<Department> GetDepartment(int id)
        {
            var Department = this.departmentService.GetAll(
                r => (r.IsDeleted == false || r.IsDeleted == null) && r.DepartmentId == id).FirstOrDefault();

            if (Department == null)
            {
                return NotFound("此筆資料已刪除或不存在");
            }

            return Department;
        }
       
        [HttpPut("{id}")]
        public IActionResult PutDepartment(int id, Department department)
        {
            department.DateModified = DateTime.Now;
            this.departmentService.Update(department);
            return NoContent();
        }

        [HttpPost]
        public ActionResult<Department> PostDepartment(Department department)
        {
            this.departmentService.Create(department);
            return Ok(department);
        }

        // DELETE: api/Departments/5
        [HttpDelete("{id}")]
        public ActionResult<Department> DeleteDepartment(int id)
        {
            var Department = this.departmentService.GetAll(r => r.DepartmentId == id).FirstOrDefault();
            if (Department == null)
            {
                return NotFound();
            }
            Department.IsDeleted = true;

            this.departmentService.Update(Department);
            return Department;
        }

        // GET: api/GetDepartmentCourseCount
        [HttpGet("GetDepartmentCourseCount")]
        public ActionResult<IEnumerable<VwDepartmentCourseCount>> GetDepartmentCourseCount()
        {
            return this.departmentService.GetVwDepartmentCourseCount().ToList();
        }
    }
}
