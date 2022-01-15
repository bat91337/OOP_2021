using System;
using System.Collections.Generic;

namespace Reports.DAL.Entities
{
    public class Task
    {
        public Task(string name, Guid id, DateTime dateTime, string description, Guid employee)
        {
             Name = name;
             Id = id;
             StatusTask = StatusTask.Open;
             DateTime = dateTime;
             Description = description;
             Comments = new List<string>();
             Employee = employee;
        }
       public string Name { get; }
       public Guid Id { get; }
       public StatusTask StatusTask { get; set; }
       public DateTime DateTime { get; }
       public string Description { get; set; }
       public List<string> Comments { get; }
       public Guid Employee { get; set; }
    }
}