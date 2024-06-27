using CodeAssesment.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CodeAssesment
{
    public class MyDataController : ApiController
    {
        // GET api/<controller>
        public IEnumerable<MyDataModel> Get()
        {
            //return new string[] { "value1", "value2" };
            string filePath = Path.Combine(System.Web.Hosting.HostingEnvironment.MapPath("~/MyData"), "DataForTest.csv");

            List<MyDataModel> dataList = ReadDataFromCSV(filePath);

            if (dataList == null || dataList.Count == 0)
            {
                return new List<MyDataModel>(); 
            }

            return dataList;
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
        private List<MyDataModel> ReadDataFromCSV(string filePath)
        {
            List<MyDataModel> dataList = new List<MyDataModel>();

            using (StreamReader fileRead = new StreamReader(filePath))
            {
                string line;
                int row = 0;
                while ((line = fileRead.ReadLine()) != null)
                {
                    if (row > 0) // Ignore the Header
                    {
                        string[] x = line.Split(',');

                        MyDataModel data = new MyDataModel
                        {
                            ID = int.Parse(x[0]),
                            Name = x[1],
                            Size = int.Parse(x[2]),
                            Type = x[3],
                            Desc = x[4]
                        };

                        dataList.Add(data);
                    }

                    row++;
                }
            }

            return dataList;
        }
    }
    
}
