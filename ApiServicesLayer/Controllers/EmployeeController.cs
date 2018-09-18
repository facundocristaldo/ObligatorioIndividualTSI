
using System;
using System.Collections.Generic;
using System.Web.Http;
using Shared.Entities;
using ApiServices;
using System.Web.Http.Cors;
using BusinessLogicLayer;
using DataAccessLayer;
using Shared.Caching;

namespace ApiServices.Controllers
{
    [Authorize]
    [EnableCors(origins: "http://localhost:52391", headers: "*", methods: "*")]
    
    public class EmployeeController : ApiController
    {
        private static IDALEmployees db = new DALEmployeesEF();
        private static IBLEmployees ctr = new BLEmployees(db);
        // GET api/<controller>
        public IEnumerable<DATAEmployee> Get([FromUri] Filtros filtros)
        {

            string nombre = filtros.nombreEmp;
            int id = Convert.ToInt32(filtros.idEmp);
           
            

            int type = 1;
            if (filtros.tipoEmp.Equals("1"))
            {
                type = 1;
            }
            else if (filtros.tipoEmp.Equals("2"))
            {
                type = 2;
            }
            else
            {
                type = 0;
            }
                List<DATAEmployee> ret = new List<DATAEmployee>();
                List<Employee> emps = ctr.GetAllEmployees(id, nombre, type);
                foreach (Employee emp in emps)
                {
                    if (emp is PartTimeEmployee)
                    {
                        PartTimeEmployee e = (PartTimeEmployee)emp;
                        ret.Add(new DATAEmployee(emp.Id, emp.Name, emp.StartDate, 1, 0, e.HourlyRate));
                    }
                    else
                    {
                        FullTimeEmployee e = (FullTimeEmployee)emp;
                        ret.Add(new DATAEmployee(emp.Id, emp.Name, emp.StartDate, 2, e.Salary, 0));
                    }
                }
                return ret;

           
        }

        // GET api/<controller>/5
        public DATAEmployee Get(int id)
        {

            Employee emp = ctr.GetEmployee(id);
            if (emp is FullTimeEmployee)
            {
                FullTimeEmployee e = (FullTimeEmployee)emp;
                return new DATAEmployee(e.Id, e.Name, e.StartDate, 2, e.Salary, 0);
            }
            else
            {
                PartTimeEmployee e = (PartTimeEmployee)emp;
                return new DATAEmployee(e.Id, e.Name, e.StartDate, 1, 0, e.HourlyRate);
            }
        }

        // POST api/<controller>
        public bool Post(int type, [FromBody]DATAEmployee emp)
        {
            try
            {
                if (type == 1) // type 1 corresponde a PARTTIME
                {
                    PartTimeEmployee e = new PartTimeEmployee();
                    e.Id = emp.Id;
                    e.Name = emp.Name;
                    e.StartDate = emp.StartDate;
                    e.HourlyRate = emp.HourlyRate;
                    ctr.AddEmployee(e);
                }
                else if (type == 2)// type 2 corresponde a FULLTIME
                {

                    FullTimeEmployee e = new FullTimeEmployee();
                    e.Id = emp.Id;
                    e.Name = emp.Name;
                    e.StartDate = emp.StartDate;
                    e.Salary = emp.Salary;
                    ctr.AddEmployee(e);

                }
                return true;
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
                return false;
            }
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]DATAEmployee emp)
        {
            Employee viejo = ctr.GetEmployee(id);
            if (emp.Type == 2)// type 2 corresponde a FULLTIME
            {
                FullTimeEmployee e = new FullTimeEmployee();

                e.Id = viejo.Id;

                e.Name = emp.Name;
                e.StartDate = emp.StartDate;
                e.Salary = emp.Salary;

                ctr.UpdateEmployee(e);
            }
            else if (emp.Type == 1)// type 1 corresponde a PARTTIME
            {
                PartTimeEmployee e = new PartTimeEmployee();
                e.Id = viejo.Id;
                e.Name = emp.Name;
                e.StartDate = emp.StartDate;
                e.HourlyRate = emp.HourlyRate;
                ctr.UpdateEmployee(e);
            }
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {

            ctr.DeleteEmployee(id);
        }
    }
}