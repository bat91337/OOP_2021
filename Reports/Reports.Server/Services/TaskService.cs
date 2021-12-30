using System;
using System.Collections.Generic;
using System.Linq;
using Reports.DAL;
using Reports.DAL.Entities;

namespace Reports.Server.Services
{
    public class TaskService : ITaskService
    {
        private Guid _guid;
        private DataBase _dataBase { get; }

        public TaskService()
        {
            _guid = Guid.NewGuid();
            _dataBase = new DataBase();
        }

        public Task CreateTask(string name, string description, Guid employee)
        {
            List<Task> tasks = _dataBase.TaskJson();
            _dataBase.Tasks.AddRange(tasks);
           var task = new Task(name, _guid, DateTime.Now, description, employee);
           _dataBase.Tasks.Add(task);
           _dataBase.CreateTaskJson();
           return task;
       }

       public Task RefactorTask(Guid id, string description)
       {
           List<Task> tasks = _dataBase.TaskJson();
           foreach (Task task in tasks)
           {
               if (task.Id.Equals(id))
               {
                   task.Description = description;
                   return task;
               }
           }
           return null;
       }

       public Task SearchTaskById(Guid id)
       {
           List<Task> tasks = _dataBase.TaskJson();
           Task task = tasks.FirstOrDefault(task => task.Id.Equals(id));
           return task;
       }

       public Task SearchTaskByDate(DateTime dateTime)
       {
           List<Task> tasks = _dataBase.TaskJson();
           Task task = tasks.FirstOrDefault(task => task.DateTime.Equals(dateTime));
           return task;
       }

       public List<Task> GetAllTasks()
       {
           List<Task> tasks = _dataBase.TaskJson();
           return tasks;
       }

       public List<Guid> SearchTaskByChangesEmployee(Guid id)
       {
           List<Employee> employees = _dataBase.EmployeeJson();
           Employee employee = employees.FirstOrDefault(employee => employee.Id.Equals(id));
           return employee.ActiveTasks;
       }

       public Task UpdateTaskOnActive(Guid id)
       {
           List<Task> tasks1 = _dataBase.TaskJson();
           _dataBase.Tasks.AddRange(tasks1);
           Task task = tasks1.FirstOrDefault(task => task.Id.Equals(id));
           _dataBase.Tasks.Remove(task);
           if (task != null)
           {
               task.StatusTask = StatusTask.Active;
               _dataBase.Tasks.Add(task);
               _dataBase.CreateTaskJson();
           }
           return task;
       }

       public Task UpdateTaskOnResolved(Guid id)
       {
           List<Task> tasks = _dataBase.TaskJson();
           Task task = tasks.FirstOrDefault(task => task.Id.Equals(id));
           if (task != null)
           {
               task.StatusTask = StatusTask.Resolved;
               var idEmployee = task.Employee;
               Employee employee = _dataBase.Employees.FirstOrDefault(employee => employee.Id.Equals(idEmployee));
               employee.OpenTasks.Remove(task.Id);
               employee.ActiveTasks.Add(task.Id);
           }
           _dataBase.CreateTaskJson();
           return task;
       }

       public Task AddComment(string comment, Guid id)
       {
           List<Task> tasks = _dataBase.TaskJson();
           _dataBase.Tasks.AddRange(tasks);
           Task task = tasks.FirstOrDefault(task => task.Id.Equals(id));
           task.Comments.Add(comment);
           _dataBase.CreateTaskJson();
           return task;
       }

       public Task ChangeEmployee(Guid id, Guid newEmployee)
       {
           List<Task> tasks = _dataBase.TaskJson();
           _dataBase.Tasks.AddRange(tasks);
           Task task = tasks.FirstOrDefault(task => task.Id.Equals(id));
           task.Employee = newEmployee;
           _dataBase.CreateTaskJson();
           return task;
       }

       public List<Task> GetListTaskEmployee(Guid employee)
       {
           List<Task> tasksJson = _dataBase.TaskJson();
           _dataBase.Tasks.AddRange(tasksJson);
           var tasks = new List<Task>();
           foreach (Task task in tasksJson)
           {
               if (task.Employee.Equals(employee))
               {
                   tasks.Add(task);
               }
           }

           return tasks;
       }
    }
}