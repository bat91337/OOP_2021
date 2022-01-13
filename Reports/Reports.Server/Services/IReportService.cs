using System;
using System.Collections.Generic;
using Reports.DAL.Entities;

namespace Reports.Server.Services
{
    public interface IReportService
    {
        Report CreateWeeklyReport(string name, Guid employee, string description);
        List<Report> ReturnWeeklyReports();
        void AddTasks(Guid id, Guid task);
        Report CreateDayReport(Guid employee, string name, string description);
        List<Report> CreateReportFromDayReport(DateTime dateTime, string name, Guid employee, string description);
        IEnumerable<Guid> ReturnLazyEmployees(DateTime dateTime);
        Report ChangeDescription(string description, Guid id);
        Report ChangeStatusReport(Guid id);
        List<Guid> GiveListGuidEmployee();
    }
}