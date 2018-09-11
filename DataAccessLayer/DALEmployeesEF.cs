using Shared.Caching;
using Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class DALEmployeesEF : IDALEmployees
    {
        public void AddEmployee(Employee emp)
        {

            var conn = new Model.EmployeeModelDataContext();


            if (emp is PartTimeEmployee emp1)
            {
                Model.PartTimeEmployee emp2 = new Model.PartTimeEmployee()
                {
                    //emp2.EmpId = emp1.Id;
                    Name = emp1.Name,
                    StartDate = emp1.StartDate,
                    HourlyRate = emp1.HourlyRate,
                };
                conn.Employees.InsertOnSubmit(emp2);

            }
            else if (emp is FullTimeEmployee emp2)
            {
                Model.FullTimeEmployee emp3 = new Model.FullTimeEmployee()
                {
                    //emp2.EmpId = emp1.Id;
                    Name = emp2.Name,
                    StartDate = emp2.StartDate,
                    Salary = emp2.Salary
                };
                conn.Employees.InsertOnSubmit(emp3);

            }
            conn.SubmitChanges();
        }

        public void DeleteEmployee(int id)
        {
            var conn = new Model.EmployeeModelDataContext();

            var q = conn.Employees.Where(emp => emp.EmpId == id).FirstOrDefault();
            conn.Employees.DeleteOnSubmit(q);
            conn.SubmitChanges();

        }

        public void UpdateEmployee(Employee emp)
        {
            var conn = new Model.EmployeeModelDataContext();

            var q = conn.Employees.Where(em => em.EmpId == emp.Id).FirstOrDefault();
            if (q != null)
            {
                if (emp is FullTimeEmployee nuevosdatos)
                {
                    Model.FullTimeEmployee aux = (Model.FullTimeEmployee)q;
                    aux.Name = nuevosdatos.Name;
                    aux.Salary = nuevosdatos.Salary;
                    aux.StartDate = nuevosdatos.StartDate;
                }
                else if (emp is PartTimeEmployee nuevossdatos)
                {
                    Model.PartTimeEmployee aux = (Model.PartTimeEmployee)q;
                    aux.Name = nuevossdatos.Name;
                    aux.HourlyRate = nuevossdatos.HourlyRate;
                    aux.StartDate = nuevossdatos.StartDate;
                }
            }
            conn.SubmitChanges();
        }

        public List<Employee> GetAllEmployees(int id,string nombre, int type)
        {
            var conn = new Model.EmployeeModelDataContext();

            List<Employee> ret = new List<Employee>();
            var list = from q in conn.Employees select q;
            if (type.Equals(1) || type.Equals(2))
            {
                list = from q in list where q.TYPE_EMP.Equals(type) select q;
            }
            if (!nombre.Equals("nosearch"))
            {
                list = from q in list where q.Name.Contains(nombre) select q;
            }
            if (!id.Equals(0))
            {
                list = from q in list where q.EmpId.Equals(id) select q;
            }
            

            //list = (List<Model.Employee>)from q in conn.Employees where q.EmpId == 2 select q;
            foreach (Model.Employee aux in list)
            {
                if (aux is Model.FullTimeEmployee)
                {
                    Model.FullTimeEmployee aux2 = (Model.FullTimeEmployee)aux;
                    FullTimeEmployee a = new FullTimeEmployee()
                    {
                        Id = aux2.EmpId,
                        Name = aux2.Name,
                        Salary = Convert.ToInt32(aux2.Salary.ToString()),
                        StartDate = aux2.StartDate
                    };
                    ret.Add(a);
                }
                else if (aux is Model.PartTimeEmployee aux2)
                {
                    PartTimeEmployee a = new PartTimeEmployee()
                    {
                        Id = aux2.EmpId,
                        Name = aux2.Name,
                        StartDate = aux2.StartDate,
                        HourlyRate = Convert.ToDouble(aux2.HourlyRate)
                    };
                    ret.Add(a);
                }
            }


            return ret;
        }

        public Employee GetEmployee(int id)
        {
            var conn = new Model.EmployeeModelDataContext();
            var aux = conn.Employees.Where(e => e.EmpId == id).FirstOrDefault();
            if (aux != null)
            {
                if (aux is Model.FullTimeEmployee aux1)
                {
                    FullTimeEmployee a = new FullTimeEmployee();
                    a.Id = aux1.EmpId;
                    a.Name = aux1.Name;
                    a.Salary = Convert.ToInt32(aux1.Salary.ToString());
                    a.StartDate = aux1.StartDate;
                    return a;
                }
                else if (aux is Model.PartTimeEmployee aux2)
                {
                    PartTimeEmployee a = new PartTimeEmployee();
                    a.Id = aux2.EmpId;
                    a.Name = aux2.Name;
                    a.StartDate = aux2.StartDate;
                    a.HourlyRate = Convert.ToDouble(aux2.HourlyRate);
                    return a;
                }
            }
            return null;
        }
        
    }
}
