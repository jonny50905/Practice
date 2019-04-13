using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MutipleTaskExceptionHandle
{
    class Program
    {
        static  void Main(string[] args)
        {
            string host = "https://lobworkshop.azurewebsites.net";
            string path = "/api/RemoteSource/Source3";
            string url = $"{host}{path}";
            List<string> urls = new List<string>();
            urls.Add("https://www.google.com");
            urls.Add("https://www.osososososoaoaoaoaoa.com");
            urls.Add("https://www.osososososoaoaoaoaoa.com");
            List<Task> tasks = new List<Task>();
            foreach (var item in urls)
            {
                HttpClient client = new HttpClient();
                var result = client.GetStringAsync(item);

                tasks.Add(result);
            }

           
            try
            {
                Task all = Task.WhenAll(tasks);
                all.Wait();
            }
            catch (AggregateException ae)
            {
                Console.WriteLine(ae);
                foreach (var item in tasks)
                {
                    Console.WriteLine("============================");
                    Console.WriteLine(item.Status);
                    if (item.Status == TaskStatus.Faulted)
                    {
                        Console.WriteLine(item.Exception);
                        Console.WriteLine("============================");
                    }
                }
            }


            Console.WriteLine("Press any key for continuing...");
            Console.ReadKey();
        }
    }
}
