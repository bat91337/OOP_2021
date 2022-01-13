using System;
using System.Collections.Generic;
using System.Linq;
using Reports.DAL.Entities;
using Reports.Server.Controllers;

namespace Reports.Server.Services 
{
    public class EmployeeService : IEmployeeService
    {
        private DataBase _dataBase { get; }

        public EmployeeService()
        {
            _dataBase = new DataBase();
        }
        public Employee Create(string name)
        {
            List<Employee> employees =_dataBase.EmployeeJson();
            _dataBase.Employees.AddRange(employees);
            var employee = new Employee(Guid.NewGuid(), name);
            _dataBase.Employees.Add(employee);
            _dataBase.CreateEmployeesJson();
            return employee;
        }

        public List<Employee> GetAllEmployee()
        { 
          return  _dataBase.EmployeeJson();
        }

        public Employee FindByName(string name)
        {
            List<Employee> employees = _dataBase.EmployeeJson();
            return employees.FirstOrDefault(employee => employee.Name.Equals(name));
        }

        public Employee FindById(Guid id)
        {
            List<Employee> employees = _dataBase.EmployeeJson();
            return employees.FirstOrDefault(employee => employee.Id.Equals(id));
        }

        public void Delete(Guid id)
        {
            List<Employee> employees = _dataBase.EmployeeJson();
            Employee employee = employees.FirstOrDefault(employee => employee.Id.Equals(id));
            employees.Remove(employee);
            _dataBase.CreateEmployeesListJson(employees);
        }

        public Employee Update(Guid id, Guid task)
        {
            List<Employee> employees = _dataBase.EmployeeJson();
            Employee employee = employees.FirstOrDefault(employee => employee.Id.Equals(id));
            employee.OpenTasks.Add(task);
            _dataBase.CreateEmployeesListJson(employees);
            return employee;
        }

        public Employee UpdateTeamlid(Guid id, Guid idSubordinate)
        {
            List<Employee> employees = _dataBase.EmployeeJson();
            Employee employee = employees.FirstOrDefault(employee => employee.Id.Equals(id));
            employee.Subordinates.Add(idSubordinate);
            _dataBase.CreateEmployeesListJson(employees);
            return employee;
        }
    }
}