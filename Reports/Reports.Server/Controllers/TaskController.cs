using System;
using System.Collections.Generic;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Reports.DAL.Entities;
using Reports.Server.Services;

namespace Reports.Server.Controllers
{
    [ApiController]
    [Route("/tasks")]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _service;

        public TaskController(ITaskService service)
        {
            _service = service;
        }

        [HttpPost]
        public Task CreateTask([FromQuery]string name, string description, Guid employee)
        {
            return _service.CreateTask(name, description, employee);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            List<Task> task = _service.GetAllTasks();
            if (task != null)
            {
                return Ok(task);
            }
            return StatusCode((int) HttpStatusCode.BadRequest);
        }

        [HttpGet]
        [Route("/searchById")]
        public IActionResult SearchTask([FromQuery]Guid id)
        {
            Task task = _service.SearchTaskById(id);
            if (task != null)
            {
                return Ok(task);
            }
            return StatusCode((int) HttpStatusCode.BadRequest);
        }

        [HttpGet]
        [Route("/searchByDate")]
        public IActionResult SerachByDate([FromQuery]DateTime dateTime)
        {
            Task task = _service.SearchTaskByDate(dateTime);
            if (task != null)
            {
                return Ok(task);
            }
            return StatusCode((int) HttpStatusCode.BadRequest);
        }

        [HttpPost]
        [Route("/changeStatus")]
        public IActionResult ChangeStatus([FromQuery]Guid id)
        {
            Task task = _service.UpdateTaskOnActive(id);
            if (task != null)
            {
                return Ok(task);
            }
            return StatusCode((int) HttpStatusCode.BadRequest);
        }

        [HttpPost]
        [Route("/addComment")]
        public IActionResult AddComment([FromQuery]string comment, Guid id)
        {
            if (id != Guid.Empty)
            {
                _service.AddComment(comment, id);
                return Ok();
            }
            return StatusCode((int) HttpStatusCode.BadRequest);
        }

        [HttpGet]
        [Route("/changeEmployee")]
        public IActionResult ChangeEmployee([FromQuery]Guid id, Guid newEmployee)
        {
            if (id != Guid.Empty)
            {
                _service.ChangeEmployee(id, newEmployee); 
                return Ok();
            }
            return StatusCode((int) HttpStatusCode.BadRequest);
        }

        [HttpGet]
        [Route("/getListTaskEmployee")]
        public IActionResult GetListTaskEmployee([FromQuery] Guid employee)
        {
            List<Task> task = _service.GetListTaskEmployee(employee);
            if (task != null)
            {
                return Ok(task);
            }
            return StatusCode((int) HttpStatusCode.BadRequest);
        }
    }
}