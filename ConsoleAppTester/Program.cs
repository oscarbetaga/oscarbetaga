using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppTester
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                PostTest().GetAwaiter().GetResult();
                GetTest().GetAwaiter().GetResult();

                //IDictionary<string, string> list = new Dictionary<string, string>();
                //list.Add("Llave1", "");
                //list.Add("Llave2", "");
                //list.Add("Llave3", "");
                //list.Add("Llave4", "");
                //list.Add("Llave5", "");
                //list.Add("Llave6", "");
                //list.Add("Llave7", "");
                //list.Add("Llave8", "");
                //list.Add("Llave9", "");
                //list.Add("Llave10", "");
                //list.Add("Llave11", "");
                //list.Add("Llave12", "");
                //list.Add("Llave13", "");
                //list.Add("Llave14", "");
                //list.Add("Llave15", "");
                //list.Add("Llave16", "");
                //list.Add("Llave17", "");
                //list.Add("Llave18", "");
                //list.Add("Llave19", "");
                //list.Add("Llave20", "");

                //Stopwatch time = new Stopwatch();
                //time.Start();
                //foreach (var item in list) { }
                //time.Stop();
                //Console.WriteLine($"foreach Tiempo: {time.Elapsed}");

                //time.Reset();

                //time.Start();
                //list.ToList().ForEach(item => { });
                //time.Stop();
                //Console.WriteLine($"ForEach Tiempo: {time.Elapsed}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex}");
            }

            Console.ReadKey();
        }

        private static async Task PostTest()
        {
            var dto = new
            {
                variables = new
                {
                    TicketDate = new { value = "2021-10-01T16:15:42.165+0100", type = "Date" },
                    idImposition = new { value = "0", type = "Integer" },
                    ImpungmentAgendaTimes = new { value = "1", type = "Integer" },
                    ImpugnTimeOnRoad = new { value = "5", type = "Integer" },
                    ImpugnTimeElectronic = new { value = "11", type = "Integer" }
                }
            };

            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(dto), Encoding.UTF8, "application/json");
                string apiResponse = null;

                using (var response = await httpClient.PostAsync("https://as-aeu-sdm-dev-camunda.azurewebsites.net/engine-rest/decision-definition/key/ImmobilizationDesition/evaluate", content))
                {
                    apiResponse = await response.Content.ReadAsStringAsync();
                }

                Console.WriteLine("PRUEBA POST:");
                Console.WriteLine(string.Empty);
                Console.WriteLine(apiResponse);
                Console.WriteLine(string.Empty);
            }
        }

        private static async Task GetTest()
        {
            using (var httpClient = new HttpClient())
            {
                string apiResponse = null;

                using (var response = await httpClient.GetAsync("https://as-aeu-sdm-dev-camunda.azurewebsites.net/engine-rest/user"))
                {
                    apiResponse = await response.Content.ReadAsStringAsync();
                }

                Console.WriteLine("PRUEBA GET:");
                Console.WriteLine(string.Empty);
                Console.WriteLine(apiResponse);
            }
        }
    }
}
