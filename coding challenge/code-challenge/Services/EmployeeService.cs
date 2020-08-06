using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using challenge.Models;
using Microsoft.Extensions.Logging;
using challenge.Repositories;

namespace challenge.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ILogger<EmployeeService> _logger;

        public EmployeeService(ILogger<EmployeeService> logger, IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
            _logger = logger;
        }

        public Employee Create(Employee employee)
        {
            if (employee != null)
            {
                _employeeRepository.Add(employee);
                _employeeRepository.SaveAsync().Wait();
            }

            return employee;
        }

        public Employee GetById(string id)
        {
            if (!String.IsNullOrEmpty(id))
            {
                return _employeeRepository.GetById(id);
            }

            return null;
        }

        public Employee Replace(Employee originalEmployee, Employee newEmployee)
        {
            if (originalEmployee != null)
            {
                _employeeRepository.Remove(originalEmployee);
                if (newEmployee != null)
                {
                    // ensure the original has been removed, otherwise EF will complain another entity w/ same id already exists
                    _employeeRepository.SaveAsync().Wait();

                    _employeeRepository.Add(newEmployee);
                    // overwrite the new id with previous employee id
                    newEmployee.EmployeeId = originalEmployee.EmployeeId;
                }
                _employeeRepository.SaveAsync().Wait();
            }

            return newEmployee;
        }

        public ReportingStructure GetReportingStructure(string id)
        {
            ReportingStructure structure = new ReportingStructure();
            Employee rootEmployee = GetById(id);
            int reports = 0;
            if (rootEmployee != null)
            {
                structure.employee = rootEmployee;               
                var queue = new Queue<string>();
                queue.Enqueue(rootEmployee.EmployeeId);

                while(queue.Count > 0)
                {
                    var employeeID = queue.Dequeue();
                    var employee = GetById(employeeID);
                    if(employee.DirectReports != null)
                    {
                        foreach (var dr in employee.DirectReports)
                        {
                            queue.Enqueue(dr.EmployeeId);
                        }
                        reports += employee.DirectReports.Count;
                    }
                    employee = null;
                }
            }
            structure.numberOfReports = reports;
            return structure;
        }

        //public int traverseTree(Employee employee)
        //{
        //    int counter = 0;
            
        //    if(employee.DirectReports.Count > 0)
        //    {
        //        foreach(Employee e in employee.DirectReports)
        //        {
        //            counter += traverseTree(e);
        //        }
        //    }

        //    return counter;
        //}
    }
}
