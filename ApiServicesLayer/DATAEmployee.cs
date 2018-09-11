using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiServices
{
    public class DATAEmployee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public int Salary { get; set; }
        public double HourlyRate { get; set; }
        public int Type { get; set; }

        public DATAEmployee(int id, string name, DateTime startDate, int type, int salary = 0, double hourlyRate = 0)
        {
            Id = id;
            Name = name;
            StartDate = startDate;
            Salary = salary;
            HourlyRate = hourlyRate;
            Type = type;
        }
    }
}