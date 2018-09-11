
using DataAccessLayer;
using Shared.Caching;
using Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{
    public class BLEmployees : IBLEmployees
    {
        private IDALEmployees _dal;

        public BLEmployees(IDALEmployees dal)
        {
            _dal = dal;
        }

        public void AddEmployee(Employee emp)
        {

            _dal.AddEmployee(emp);
        }

        public void DeleteEmployee(int id)
        {
            _dal.DeleteEmployee(id);
        }

        public void UpdateEmployee(Employee emp)
        {
            _dal.UpdateEmployee(emp);
        }

        public List<Employee> GetAllEmployees(int id, string nombre, int type)
        {
            return _dal.GetAllEmployees(id,nombre,type);
        }

        public Employee GetEmployee(int id)
        {
            return _dal.GetEmployee(id);
        }

        public double CalcPartTimeEmployeeSalary(int idEmployee, int hours)
        {
            Employee emp = _dal.GetEmployee(idEmployee);
            if (emp is null)
            {
                throw new EmployeeIdNotFoundException(idEmployee);
            }
            else
            if (emp is FullTimeEmployee)
            {
                throw new EmployeeNotPartTimeException(idEmployee);
            }
            else
            {
                PartTimeEmployee part = (PartTimeEmployee)emp;
                double rate = part.HourlyRate;
                return rate * hours;
            }
        }

       
    }
}
