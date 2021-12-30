using System;
using System.IO;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using Reports.DAL.Entities;

namespace Reports.Clients
{
    internal static class Program
    {
        internal static void Main(string[] args)
        {
            CreateEmployee("aboba");
            FindEmployeeById("ac8ac3ce-f738-4cd6-b131-1aa0e16eaadc");
            FindEmployeeByName("aboba");
            FindEmployeeByName("kek");
        }

        private static void CreateEmployee(string name)
        {
            // Запрос к серверу
            var request = HttpWebRequest.Create($"https://localhost:5001/employees/?name={name}");
            request.Method = WebRequestMethods.Http.Post;
            var response = request.GetResponse();

            // Чтение ответа
            var responseStream = response.GetResponseStream();
            using var readStream = new StreamReader(responseStream, Encoding.UTF8);
            var responseString = readStream.ReadToEnd();

            // Десериализация (перевод JSON'a к C# классу)
            var employee = JsonConvert.DeserializeObject<Employee>(responseString);

            Console.WriteLine("Created employee:");
            Console.WriteLine($"Id: {employee.Id}");
            Console.WriteLine($"Name: {employee.Name}");
        }

        private static void FindEmployeeById(string id)
        {
            var request = HttpWebRequest.Create($"https://localhost:5001/employees/?id={id}");
            request.Method = WebRequestMethods.Http.Get;

            try
            {
                var response = request.GetResponse();

                var responseStream = response.GetResponseStream();
                using var readStream = new StreamReader(responseStream, Encoding.UTF8);
                var responseString = readStream.ReadToEnd();

                var employee = JsonConvert.DeserializeObject<Employee>(responseString);

                Console.WriteLine("Found employee by id:");
                Console.WriteLine($"Id: {employee.Id}");
                Console.WriteLine($"Name: {employee.Name}");
            }
            catch (WebException e)
            {
                Console.WriteLine("Employee was not found");
                Console.Error.WriteLine(e.Message);
            }
        }

        private static void FindEmployeeByName(string name)
        {
            var request = HttpWebRequest.Create($"https://localhost:5001/employees/?name={name}");
            request.Method = WebRequestMethods.Http.Get;
            try
            {
                var response = request.GetResponse();

                var responseStream = response.GetResponseStream();
                using var readStream = new StreamReader(responseStream, Encoding.UTF8);
                var responseString = readStream.ReadToEnd();

                var employee = JsonConvert.DeserializeObject<Employee>(responseString);

                Console.WriteLine("Found employee by name:");
                Console.WriteLine($"Id: {employee.Id}");
                Console.WriteLine($"Name: {employee.Name}");
            }
            catch (WebException e)
            {
                Console.WriteLine("Employee was not found");
                Console.Error.WriteLine(e.Message);
            }
        }

        private static void CreateTask(string name, string description, Guid employee)
        {
            var request = HttpWebRequest.Create($"https://localhost:5001/task/?name={name}&description={description}&employee={employee}");
            request.Method = WebRequestMethods.Http.Post;
            var response = request.GetResponse();
            
            var responseStream = response.GetResponseStream();
            using var readStream = new StreamReader(responseStream, Encoding.UTF8);
            var responseString = readStream.ReadToEnd();
            
            var task = JsonConvert.DeserializeObject<Task>(responseString);

            Console.WriteLine("Created task:");
            Console.WriteLine($"Id: {task.Id}");
            Console.WriteLine($"Name: {task.Name}");
        }

        private static void SearchTaskById(Guid id)
        {
            var request = HttpWebRequest.Create($"https://localhost:5001/task/?id={id}");
            request.Method = WebRequestMethods.Http.Get;
            try
            {
                var response = request.GetResponse();
                var responseStream = response.GetResponseStream();
                using var readStream = new StreamReader(responseStream, Encoding.UTF8);
                var responseString = readStream.ReadToEnd();
                var task = JsonConvert.DeserializeObject<Task>(responseString);

                Console.WriteLine("Found task by id:");
                Console.WriteLine($"Id: {task.Id}");
                Console.WriteLine($"Name: {task.Name}");
            }
            catch (WebException e)
            {
                Console.WriteLine("Task was not found");
                Console.Error.WriteLine(e.Message);
            }
        }
        private static void SearchTaskByDate(DateTime dateTime)
        {
            var request = HttpWebRequest.Create($"https://localhost:5001/task/?dateTime={dateTime}");
            request.Method = WebRequestMethods.Http.Get;
            try
            {
                var response = request.GetResponse();
                var responseStream = response.GetResponseStream();
                using var readStream = new StreamReader(responseStream, Encoding.UTF8);
                var responseString = readStream.ReadToEnd();
                var task = JsonConvert.DeserializeObject<Task>(responseString);
                Console.WriteLine("Found task by name:");
                Console.WriteLine($"Id: {task.Id}");
                Console.WriteLine($"Name: {task.Name}");
            }
            catch (WebException e)
            {
                Console.WriteLine("Task was not found");
                Console.Error.WriteLine(e.Message);
            }
        }

        private static void CreateWeeklyReport(string name, Guid employeeId, string description)
        {
            var request = HttpWebRequest.Create($"https://localhost:5001/report/?name={name}&employeeId={employeeId}&description={description}");
            request.Method = WebRequestMethods.Http.Get;
            request.Method = WebRequestMethods.Http.Post;
            var response = request.GetResponse();
            
            var responseStream = response.GetResponseStream();
            using var readStream = new StreamReader(responseStream, Encoding.UTF8);
            var responseString = readStream.ReadToEnd();
            
            var report = JsonConvert.DeserializeObject<Report>(responseString);

            Console.WriteLine("Created report:");
            Console.WriteLine($"Id: {report.Id}");
            Console.WriteLine($"Name: {report.Name}");
        }

        private static void ChangeDescription(string description, Guid id)
        {
            var request = HttpWebRequest.Create($"https://localhost:5001/report/?id={id}&description={description}");
            request.Method = WebRequestMethods.Http.Get;
            request.Method = WebRequestMethods.Http.Post;
            var response = request.GetResponse();
            
            var responseStream = response.GetResponseStream();
            using var readStream = new StreamReader(responseStream, Encoding.UTF8);
            var responseString = readStream.ReadToEnd();
            
            var report = JsonConvert.DeserializeObject<Report>(responseString);

            Console.WriteLine("Change description report:");
            Console.WriteLine($"Description: {report.Description}");
        }

        private static void ChangeStatus(Guid id)
        {
            var request = HttpWebRequest.Create($"https://localhost:5001/report/?id={id}");
            request.Method = WebRequestMethods.Http.Get;
            request.Method = WebRequestMethods.Http.Post;
            var response = request.GetResponse();
            
            var responseStream = response.GetResponseStream();
            using var readStream = new StreamReader(responseStream, Encoding.UTF8);
            var responseString = readStream.ReadToEnd();
            
            var report = JsonConvert.DeserializeObject<Report>(responseString);

            Console.WriteLine("Change status report:");
            Console.WriteLine($"Status: {report.StatusReport}");
        }
    }
}