using System;
using System.Collections.Generic;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Reports.DAL.Entities;
using Reports.Server.Services;

namespace Reports.Server.Controllers
{
    [ApiController]
    [Route("/employees")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _service;

        public EmployeeController(IEmployeeService service)
        {
            _service = service;
        }

        [HttpPost]
        public Employee Create([FromQuery] string name)
        {
            return _service.Create(name);
        }

        [HttpGet]
        public IActionResult Find([FromQuery] Guid id)
        {
            if (id != Guid.Empty)
            {
                Employee result = _service.FindById(id);
                if (result != null)
                {
                    return Ok(result);
                }

                return NotFound();
            }

            return StatusCode((int) HttpStatusCode.BadRequest);
        }

        [HttpGet]
        [Route("/findByName")]
        public IActionResult FindByName([FromQuery] string name)
        {
            if (name != null)
            {
                Employee result = _service.FindByName(name);
                if (result != null)
                {
                    return Ok(result);
                }
        
                return NotFound();
            }
        
            return StatusCode((int) HttpStatusCode.BadRequest);
        }
        
        [HttpGet]
        [Route("/getAll")]
        public IActionResult GetAll()
        {
            List<Employee> employees = _service.GetAllEmployee();
            if (employees != null)
            {
                return Ok(employees);
            }
        
            return StatusCode((int) HttpStatusCode.BadRequest);
        }

        [HttpGet]
        [Route("/delete")]
        public IActionResult Delete(Guid id)
        {
            if (id != Guid.Empty)
            {
                _service.Delete(id);
                return Ok();
            }
            return StatusCode((int) HttpStatusCode.BadRequest);
        }

        [HttpPost]
        [Route("/updateTeamLid")]
        public IActionResult UpdateTeamLid(Guid id, Guid idSubordinate)
        {
            if (id != Guid.Empty)
            {
                _service.UpdateTeamlid(id, idSubordinate);
                return Ok();
            }
            return StatusCode((int) HttpStatusCode.BadRequest);
        }

        [HttpPost]
        [Route("/update")]
        public IActionResult Update(Guid idEmployee, Guid idTask)
        {
            if (idEmployee != Guid.Empty)
            {
                _service.Update(idEmployee, idTask);
                return Ok();
            }
            return StatusCode((int) HttpStatusCode.BadRequest);
        }
        
    }
}