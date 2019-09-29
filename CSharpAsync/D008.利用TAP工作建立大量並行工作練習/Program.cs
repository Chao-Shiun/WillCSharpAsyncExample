using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace D008.利用TAP工作建立大量並行工作練習
{
    class Program
    {
        static async Task Main(string[] args)
        {
            string host = "https://lobworkshop.azurewebsites.net";
            string path = "/api/RemoteSource/Source3";
            string url = $"{host}{path}";


            Stopwatch sw = new Stopwatch();
            sw.Restart();
            List<Task> taskList = new List<Task>(10);

            for (int i = 0; i < 10; i++)
            {
                var index = string.Format("{0:D2}", i + 1);

                taskList.Add(Task.Run(async () =>
                {
                    var tid = string.Format("{0:D2}", Thread.CurrentThread.ManagedThreadId);
                    HttpClient client = new HttpClient();

                    var result1 = client.GetStringAsync(url);
                    var result2 = client.GetStringAsync(url);
                    await Task.WhenAll(result1, result2);
                    Console.WriteLine($"{index}-1 測試 (TID: {tid}) >>>> {DateTime.Now}");

                    Console.WriteLine($"{index}-1 測試 (TID: {tid}) ==== {await result1}");
                    Console.WriteLine($"{index}-1 測試 (TID: {tid}) <<<< {DateTime.Now}");

                    Console.WriteLine($"{index}-2 測試 (TID: {tid}) >>>> {DateTime.Now}");

                    Console.WriteLine($"{index}-2 測試 (TID: {tid}) ==== {await result2}");
                    Console.WriteLine($"{index}-2 測試 (TID: {tid}) <<<< {DateTime.Now}");
                }));
            }
            

            while (taskList.Count > 0)
            {
                var finishedResult = await Task.WhenAny(taskList);
                taskList.Remove(finishedResult);
            }
            sw.Stop();
            Console.WriteLine($"total {sw.ElapsedMilliseconds / 1000.0} secnods");

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }
}
