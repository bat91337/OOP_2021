using System;
using System.Collections.Generic;
using Reports.DAL.Entities;

namespace Reports.Server.Services
{
    public interface IEmployeeService
    {
        Employee Create(string name);

        List<Employee> GetAllEmployee();

        Employee FindById(Guid id);

        void Delete(Guid id);

        Employee Update(Guid id, Guid task);

        Employee UpdateTeamlid(Guid id, Guid idSubordinate);
        public Employee FindByName(string name);
    }
}