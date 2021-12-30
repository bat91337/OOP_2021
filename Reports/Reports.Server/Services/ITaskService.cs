using System;
using System.Collections.Generic;
using Reports.DAL.Entities;

namespace Reports.Server.Services
{
    public interface ITaskService
    { 
        Task CreateTask(string name, string description, Guid employee); 
        Task RefactorTask(Guid id, string description);
        Task SearchTaskById(Guid id);
        Task SearchTaskByDate(DateTime dateTime);
        List<Task> GetAllTasks();
        List<Guid> SearchTaskByChangesEmployee(Guid id);
        Task UpdateTaskOnActive(Guid id);
        Task UpdateTaskOnResolved(Guid id);
        Task AddComment(string comment, Guid id);
        Task ChangeEmployee(Guid id, Guid newEmployee);
        List<Task> GetListTaskEmployee(Guid employee);
    }
}