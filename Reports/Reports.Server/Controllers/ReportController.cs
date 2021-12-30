using System;
using System.Collections.Generic;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Reports.DAL.Entities;
using Reports.Server.Services;

namespace Reports.Server.Controllers
{
    [ApiController]
    [Route("/reports")]
    public class ReportController : ControllerBase
    {
        private readonly IReportService _service;

        public ReportController(IReportService service)
        {
            _service = service;
        }

        [HttpPost]
        public Report Create([FromQuery]string name, Guid employee, string description)
        {
            return _service.CreateWeeklyReport(name, employee, description);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            List<Report> reports = _service.ReturnWeeklyReports();
            if (reports != null)
            {
                return Ok(reports);
            }
            return StatusCode((int) HttpStatusCode.BadRequest);
        }

        [HttpPost]
        [Route("/addTask")]
        public IActionResult AddTask([FromQuery]Guid id, Guid task)
        {
            if (id != Guid.Empty)
            {
                _service.AddTasks(id, task);
                return Ok();
            }
            return StatusCode((int) HttpStatusCode.BadRequest);
        }

        [HttpPost]
        [Route("/createReportFromDayReport")]
        public IActionResult CreateReportFromDayReport([FromQuery]DateTime dateTime, string name, Guid employeeId, string description)
        {
            List<Report> reports = _service.CreateReportFromDayReport(dateTime, name, employeeId, description);
            if (reports != null)
            {
                return Ok(reports);
            }
            return StatusCode((int) HttpStatusCode.BadRequest);
        }

        [HttpPost]
        [Route("/createDayReport")]
        public IActionResult CreateDayReport([FromQuery]Guid employeeId, string name, string description)
        {
            Report report = _service.CreateDayReport(employeeId, name, description);
            if (report != null)
            {
                return Ok(report);
            }
            return StatusCode((int) HttpStatusCode.BadRequest);
        }

        [HttpPost]
        [Route("/changeDescription")]
        public IActionResult ChangeDescription([FromQuery]string description, Guid id)
        {
            Report report = _service.ChangeDescription(description, id);
            if (report != null)
            {
                return Ok(report);
            }
            return StatusCode((int) HttpStatusCode.BadRequest);
        }
        [HttpPost]
        [Route("/changelist")]
        public IActionResult ChangeStatus([FromQuery]Guid id)
        {
            Report report = _service.ChangeStatusReport(id);
            if (report != null)
            {
                return Ok(report);
            }
            return StatusCode((int) HttpStatusCode.BadRequest);
        }

    }
}