using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Reports.DAL.Entities;

namespace Reports.Server
{
    public class DataBase
    {
        private const string PathReport = "fileReport.json";
        private const string PathEmployee = "fileEmployee.json";
        private const string PathDayReport = "fileDayReport.json";
        private const string PathTask = "fileTask.json";
        public DataBase()
        {
            Tasks = new List<Task>();
            Reports = new List<Report>();
            Employees = new List<Employee>();
            DayReport = new List<Report>();
        }
        
        public List<Task> Tasks { get; }
        public List<Report> Reports { get; }
        public List<Employee> Employees { get; }
        public List<Report> DayReport { get; }
        public void CreateEmployeesListJson(List<Employee> employee)
        {
            string json = JsonConvert.SerializeObject(employee, new JsonSerializerSettings
            {
               TypeNameHandling = TypeNameHandling.All,
               Formatting = Formatting.Indented,
            });

            File.WriteAllText(PathEmployee, json);
        }
        public void CreateTaskJson()
        {
            string json = JsonConvert.SerializeObject(Tasks, new JsonSerializerSettings
            {
               TypeNameHandling = TypeNameHandling.All,
               Formatting = Formatting.Indented,
             });

            File.WriteAllText(PathTask, json);
        }
        public void CreateReportsJson()
        {
            string json = JsonConvert.SerializeObject(Reports, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All,
                Formatting = Formatting.Indented,
            });

            File.WriteAllText(PathReport, json);
        }
        public void CreateEmployeesJson()
        {
            string json = JsonConvert.SerializeObject(Employees, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All,
                Formatting = Formatting.Indented,
            });

            File.WriteAllText(PathEmployee, json);
        }
        public void CreateDayReportJson()
        {
            string json = JsonConvert.SerializeObject(DayReport, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All,
                Formatting = Formatting.Indented,
            });

            File.WriteAllText(PathDayReport, json);
        }
        public List<Report> ReportJson()
        {
            string jsonM = File.ReadAllText(PathReport);
            List<Report> report = JsonConvert.DeserializeObject<List<Report>>(jsonM, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All,
                Formatting = Formatting.Indented,
            });

            return report;
        }
        public List<Task> TaskJson()
        {
            string jsonM = File.ReadAllText(PathTask);
            List<Task> task = JsonConvert.DeserializeObject<List<Task>>(jsonM, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All,
                Formatting = Formatting.Indented,
            });

            return task;
        }
        public List<Employee> EmployeeJson()
        {
            string jsonM = File.ReadAllText(PathEmployee);
            List<Employee> employee = JsonConvert.DeserializeObject<List<Employee>>(jsonM, new JsonSerializerSettings
            {
                    TypeNameHandling = TypeNameHandling.All,
                    Formatting = Formatting.Indented,
            });

            return employee;
        }
        public List<Report> DayReportJson()
        {
            string jsonM = File.ReadAllText(PathDayReport);
            List<Report> report = JsonConvert.DeserializeObject<List<Report>>(jsonM, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All,
                Formatting = Formatting.Indented,
            });

            return report;
        }
    }
}