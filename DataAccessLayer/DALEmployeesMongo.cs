using MongoDB.Bson;
using MongoDB.Driver;
using Shared.Caching;
using Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class DALEmployeesMongo : IDALEmployees
    {
        public void AddEmployee(Employee emp)
        {
            var con = new MongoClient();
            var db = con.GetDatabase("testingMongo");
            var col = db.GetCollection<BsonDocument>("People");

            if (emp is Shared.Entities.FullTimeEmployee)
            {
                FullTimeEmployee em = (FullTimeEmployee)emp;
                BsonDocument ins = new BsonDocument();
                ins.Add("id", getID());
                ins.Add("Name", em.Name);
                ins.Add("Salary", em.Salary);
                ins.Add("StartDate", em.StartDate);
                ins.Add("type", 1);
                col.InsertOne(ins);

            }
            else if (emp is Shared.Entities.PartTimeEmployee)
            {
                PartTimeEmployee em = (PartTimeEmployee)emp;
                BsonDocument ins = new BsonDocument();
                ins.Add("id", getID());
                ins.Add("Name", em.Name);
                ins.Add("HourlyRate", em.HourlyRate);
                ins.Add("StartDate", em.StartDate);
                ins.Add("type", 2);
                col.InsertOne(ins);

            }

        }

        public int getID()
        {
            var con = new MongoClient();
            var db = con.GetDatabase("testingMongo");
            var col = db.GetCollection<BsonDocument>("People");
            var count = col.CountDocuments(new BsonDocument());
            int id = Convert.ToInt32(count) + 1;
            return id;
        }

        public void DeleteEmployee(int id)
        {
            var client = new MongoClient();
            var db = client.GetDatabase("testingMongo");
            var collection = db.GetCollection<BsonDocument>("People");
            collection.DeleteOne(Builders<BsonDocument>.Filter.Eq("id", id));
        }

        public void UpdateEmployee(Employee emp)
        {
            var client = new MongoClient();
            var db = client.GetDatabase("testingMongo");
            var collection = db.GetCollection<BsonDocument>("People");
            if (emp is FullTimeEmployee)
            {
                FullTimeEmployee emp2 = (FullTimeEmployee)emp;
                var obj = collection.UpdateOne(Builders<BsonDocument>.Filter.Eq("id", emp.Id), Builders<BsonDocument>.Update.Set("", "")
                    .Set("StartDate", emp2.StartDate)
                    .Set("Salary", emp2.Salary)
                    .Set("Name", emp2.Name)
                    );
            }
            else
            {
                PartTimeEmployee emp2 = (PartTimeEmployee)emp;
                var obj = collection.UpdateOne(Builders<BsonDocument>.Filter.Eq("id", emp.Id), Builders<BsonDocument>.Update.Set("", "")
                    .Set("StartDate", emp2.StartDate)
                    .Set("HourlyRate", emp2.HourlyRate)
                    .Set("Name", emp2.Name)
                    );
            }

        }

        public List<Employee> GetAllEmployees(int id, string nombre, int type)
        {
            var client = new MongoClient();
            var db = client.GetDatabase("testingMongo");
            //var collection = db.GetCollection<Shared.Entities.Person>("People");
            var collection = db.GetCollection<BsonDocument>("People");

            var resp = collection.Find(_ => true).ToEnumerable();
            List<Employee> list = new List<Employee>();
            foreach (BsonDocument aux in resp)
            {
                if (aux.GetValue("type").ToInt32() == 1)
                {
                    FullTimeEmployee au = new FullTimeEmployee();
                    au.Id = aux.GetValue("id").ToInt32();
                    au.Name = aux.GetValue("Name").ToString();
                    au.Salary = (int)aux.GetValue("Salary");
                    au.StartDate = (DateTime)aux.GetValue("StartDate");
                    list.Add(au);
                }
                else
                {
                    PartTimeEmployee au = new PartTimeEmployee();
                    au.Id = aux.GetValue("id").ToInt32();
                    au.Name = aux.GetValue("Name").ToString();
                    au.HourlyRate = (int)aux.GetValue("HourlyRate");
                    au.StartDate = (DateTime)aux.GetValue("StartDate");
                    list.Add(au);
                }
            }
            return list;
        }

        public Employee GetEmployee(int id)
        {
            var client = new MongoClient();
            var db = client.GetDatabase("testingMongo");
            var collection = db.GetCollection<BsonDocument>("People");

            var aux = collection.Find(Builders<BsonDocument>.Filter.Eq("id", id)).FirstOrDefault();

            if (aux.GetValue("type").ToInt32() == 1)
            {
                FullTimeEmployee au = new FullTimeEmployee();
                au.Id = aux.GetValue("id").ToInt32();
                au.Name = aux.GetValue("Name").ToString();
                au.Salary = (int)aux.GetValue("Salary");
                au.StartDate = (DateTime)aux.GetValue("StartDate");
                return au;
            }
            else
            {
                PartTimeEmployee au = new PartTimeEmployee()
                {
                    Id = aux.GetValue("id").ToInt32(),
                    Name = aux.GetValue("Name").ToString(),
                    HourlyRate = (int)aux.GetValue("HourlyRate"),
                    StartDate = (DateTime)aux.GetValue("StartDate")
                };
                return au;
            }
        }
        
    }
}
