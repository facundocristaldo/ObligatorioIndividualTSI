using Shared.Caching;
using Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public interface IDALEmployees
    {
        void AddEmployee(Employee emp);

        void DeleteEmployee(int id);

        void UpdateEmployee(Employee emp);

        List<Employee> GetAllEmployees(int id,string nombre, int type);

        Employee GetEmployee(int id);
    }
}
