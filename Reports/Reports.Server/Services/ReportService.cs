using System;
using System.Collections.Generic;
using System.Linq;
using Reports.DAL;
using Reports.DAL.Entities;

namespace Reports.Server.Services
{
    public class ReportService : IReportService
    {
        private DataBase _dataBase;
        private Guid _guid;

        public ReportService()
        {
            _dataBase = new DataBase();
            _guid = Guid.NewGuid();
        }
        public Report CreateWeeklyReport(string name, Guid employeeId, string description)
        {
            List<Report> reports = _dataBase.ReportJson();
            _dataBase.Reports.AddRange(reports);
            List<Employee> employees = _dataBase.EmployeeJson();
            Employee employee = employees.FirstOrDefault(employee => employee.Id.Equals(employeeId));
            var report = new Report(_guid, name, employee, DateTime.Now, description);
            DateTime dateTime = DateTime.Now.Subtract(new TimeSpan(7, 0, 0, 0));
            foreach (Task task in _dataBase.Tasks)
            {
                if (dateTime < task.DateTime)
                {
                   report.Tasks.Add(task.Id);
                }
            }
            _dataBase.Reports.Add(report);
            _dataBase.CreateReportsJson();
            return report;
        }

        public List<Report> ReturnWeeklyReports()
        {
            var reports = new List<Report>();
            DateTime dateTime = DateTime.Now.Subtract(new TimeSpan(7, 0, 0, 0));
            List<Report> reportsFromJson = _dataBase.ReportJson();
            _dataBase.Reports.AddRange(reportsFromJson);
            foreach (Report report in reportsFromJson)
            {
                if (dateTime < report.DateTime)
                {
                    reports.Add(report);
                }
            }

            return reports;
        }

        public void AddTasks(Guid id, Guid task)
        {
            List<Report> reportsFromJson = _dataBase.ReportJson();
            _dataBase.Reports.AddRange(reportsFromJson);
            foreach (Report report1 in reportsFromJson)
            {
                if (report1.Id.Equals(id))
                {
                    report1.Tasks.Add(task);
                    _dataBase.CreateReportsJson();
                }
            }
        }

        public Report CreateDayReport(Guid employeeId, string name, string description)
        {
            List<Employee> employees = _dataBase.EmployeeJson();
            Employee employee = employees.FirstOrDefault(employee => employee.Id.Equals(employeeId));
            var report = new Report(_guid, name, employee, DateTime.Now, description);
            DateTime dateTime = DateTime.Now.Subtract(new TimeSpan(1, 0, 0, 0));
            foreach (var activeTaskEmployee in employee.ActiveTasks)
            {
                Task activeTask = _dataBase.Tasks.FirstOrDefault(task => task.Id.Equals(activeTaskEmployee));
                if (dateTime < activeTask.DateTime)
                {
                    report.Tasks.Add(activeTask.Id);
                }
            }
            _dataBase.DayReport.Add(report);
            _dataBase.CreateDayReportJson();

            return report;
        }

        public List<Report> CreateReportFromDayReport(DateTime dateTime, string name, Guid employeeId, string description)
        {
            var reportsList = new List<Report>();
            List<Employee> employees = _dataBase.EmployeeJson();
            _dataBase.Employees.AddRange(employees);
            Employee employee = _dataBase.Employees.FirstOrDefault(employee => employee.Id.Equals(employeeId));
            var report = new Report(_guid, name, employee, DateTime.Now, description);
            List<Report> reportsFromJson = _dataBase.DayReportJson();
            _dataBase.Reports.AddRange(reportsFromJson);
            foreach (Report reports in _dataBase.Reports)
            {
                if (reports.DateTime.Equals(dateTime))
                {
                    report.Tasks.AddRange(report.Tasks);
                }
            }
            _dataBase.CreateReportsJson();

            return reportsList;
        }

        public IEnumerable<Guid> ReturnLazyEmployees(DateTime dateTime)
        {
            var employeeList = new List<Guid>();
            List<Report> reportsFromJson = _dataBase.DayReportJson();
            _dataBase.Reports.AddRange(reportsFromJson);
            foreach (Report reports in _dataBase.Reports)
            {
                if (reports.DateTime.Equals(dateTime))
                {
                    foreach (var task in reports.TasksList)
                    {
                        employeeList.Add(task.Employee);
                    }
                }
            }

            List<Guid> ListIdEmployee = GiveListGuidEmployee();
            IEnumerable<Guid> lazyEmployee = ListIdEmployee.Except(employeeList);
            return lazyEmployee;
        }

        public Report ChangeDescription(string description, Guid id)
        {
            List<Report> reports = _dataBase.ReportJson();
            _dataBase.Reports.AddRange(reports);
            foreach (Report report in reports)
            {
                if (report.Id.Equals(id))
                {
                    report.Description = description;
                    _dataBase.CreateReportsJson();
                    return report;
                }
            }

            return null;
        }

        public Report ChangeStatusReport(Guid id)
        {
            List<Report> reports = _dataBase.ReportJson();
            _dataBase.Reports.AddRange(reports);
            Report report = reports.FirstOrDefault(reports => reports.Id.Equals(id));
            report.StatusReport = StatusReport.Prepared;
            _dataBase.CreateReportsJson(); 
            return report;
        }

        public List<Guid> GiveListGuidEmployee()
        {
            var idEmployee = new List<Guid>();
            foreach (var employee in _dataBase.Employees)
            {
                idEmployee.Add(employee.Id);
            }

            return idEmployee;
        }
    }
}