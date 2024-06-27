using System;
using System.Data;
using System.IO;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Linq;
using CodeAssesment.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Headers;

namespace CodeAssesment
{
    public partial class _Default : Page
    {
        protected async void Page_Load(object sender, EventArgs e)
        {
            //string filePath = Server.MapPath("~/MyData/DataForTest.csv");
            //DataTable dtData = GetMyData(filePath);

            //myDataGrid.DataSource = dtData;
            //myDataGrid.DataBind();

            try
            {
                DataTable dt = await LoadDataAsync();

                if (dt != null)
                {
                    myDataGrid.DataSource = dt;
                    myDataGrid.DataBind();
                }
                else
                {
                    
                    Console.WriteLine("Could not load data from API.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en Page_Load: {ex.Message}");
            }
        }

        private async Task<DataTable> LoadDataAsync()
        {

            // URL de la API
            string apiUrl = "https://localhost:44385/api/MyData";

            try
            {
                HttpClientHandler handler = new HttpClientHandler();
                handler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true; // Permitir todos los certificados (INSEGURO para producción)

                HttpClient client = new HttpClient(handler);
                client.BaseAddress = new Uri(apiUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                
                HttpResponseMessage response = await client.GetAsync(apiUrl);

                // Check if the request was successful (status code 200 OK)
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsAsync<MyDataModel[]>();

                    
                    DataTable dt = new DataTable();
                    dt.Columns.Add("ID", typeof(int));
                    dt.Columns.Add("Name", typeof(string));
                    dt.Columns.Add("Size", typeof(int));
                    dt.Columns.Add("Type", typeof(string));
                    dt.Columns.Add("Desc", typeof(string));

                    
                    foreach (var item in data)
                    {
                        dt.Rows.Add(item.ID, item.Name, item.Size, item.Type, item.Desc);
                    }

                    return dt;

                }
                else
                {
                    Console.WriteLine($"Error: {response.StatusCode} - {response.ReasonPhrase}");
                }
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error calling API: {{ex.Message}}\");: {ex.Message}");
                return null;
            }
        }
           
        private DataTable GetMyData(string file)
        {
            DataTable dtMyDataSet = new DataTable();
            using (StreamReader fileRead = new StreamReader(file))
            {
                string line;
                int row = 0;
                while ((line = fileRead.ReadLine()) != null)
                {
                    Regex CSVParser = new Regex(",(?=(?:[^\"]*\"[^\"]*\")*(?![^\"]*\"))");

                    string[] x = CSVParser.Split(line);

                    if (row == 0)
                    {
                        foreach (string header in x)
                        {
                            dtMyDataSet.Columns.Add(header);
                        }

                    }
                    else
                    {
                        DataRow dRow = dtMyDataSet.NewRow();

                        for (int i = 0; i < x.Length; i++)
                        {
                            dRow[i] = x[i];
                        }
                        dtMyDataSet.Rows.Add(dRow);
                    }
                    row++;

                }
            }
            return dtMyDataSet;
        }

    }
}