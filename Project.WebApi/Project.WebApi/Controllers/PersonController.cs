using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Project.DataAccess.Lib.Models;
using Project.DataAccess.Lib.UnitOfWork;
using Project.Service.Lib.PersonServices;

namespace Project.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {  
        private readonly IPersonService personService;
        private readonly IUnitOfWork unitOfWork;

        public PersonController(IPersonService personService, IUnitOfWork unitOfWork)
        {
            this.personService = personService;
            this.unitOfWork = unitOfWork;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Person>> GetPerson()
        {
            return personService.GetAll(r => r.IsDeleted == false || r.IsDeleted == null).ToList();
        }
       
        [HttpGet("{id}")]
        public ActionResult<Person> GetPerson(int id)
        {
            var person = this.personService.GetAll(
               r => (r.IsDeleted == false || r.IsDeleted == null) && r.Id == id).FirstOrDefault();

            if (person == null)
            {
                return NotFound("此筆資料已刪除或不存在");
            }

            return person;
        }

        [HttpPut("{id}")]
        public IActionResult PutPerson(int id, Person person)
        {
            person.DateModified = DateTime.Now;
            this.personService.Update(person);

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
        public ActionResult<Person> PostPerson(Person person)
        {
            this.personService.Create(person);

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

            return Ok(person);
        }

        [HttpDelete("{id}")]
        public ActionResult<Person> DeletePerson(int id)
        {
            var person = this.personService.GetAll(r => r.Id == id).FirstOrDefault();
            if (person == null)
            {
                return NotFound();
            }
            person.IsDeleted = true;

            this.personService.Update(person);
            unitOfWork.SaveChange();
            return person;
        }
    }
}
