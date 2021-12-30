using System;
using System.Collections.Generic;

namespace Reports.DAL.Entities
{
    public class Employee
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
        public List<Guid> OpenTasks { get; set; }
        public List<Guid> ActiveTasks { get; set; }
        public List<Guid> ResolvedTasks { get; set; }
        public List<Guid> Subordinates { get; set; }

        public Employee(Guid id, string name)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(id), "Id is invalid");
            }

            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException(nameof(name), "Name is invalid");
            }

            Id = id;
            Name = name;
            Subordinates = new List<Guid>();
            OpenTasks = new List<Guid>();
            ActiveTasks = new List<Guid>();
            ResolvedTasks = new List<Guid>();
        }
    }
}