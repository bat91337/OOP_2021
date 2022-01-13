using System;
using System.Collections.Generic;

namespace Reports.DAL.Entities
{
    public class Report
    {
        public Report(Guid id, string name, Employee employee, DateTime dateTime, string description)
        {
            Id = id;
            Name = name;
            Employee = employee;
            Tasks = new List<Guid>();
            DateTime = dateTime;
            StatusReport = StatusReport.Unprepared;
            Description = description;
            TasksList = new List<Task>();
        }
        public Guid Id { get; }
        public string Name { get; }
        public Employee Employee { get; }
        public List<Guid> Tasks { get; }
        public List<Task> TasksList { get; }
        public DateTime DateTime { get; }
        public StatusReport StatusReport { get; set; }
        public string Description { get; set; }
    }
}