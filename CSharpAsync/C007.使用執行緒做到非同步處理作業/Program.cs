using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace C007.使用執行緒做到非同步處理作業
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine($"建立兩個執行緒物件");
            Task task1 = Task.Run(() =>
            {
                Console.WriteLine($"執行緒1 的 ID={Thread.CurrentThread.ManagedThreadId} ( 輸出 X )");
                Thread.Sleep(900);
                for (int i = 0; i < 20; i++)
                {
                    Thread.Sleep(100); Console.Write("X");
                }
                //Console.WriteLine();
            });
            //thread1.IsBackground = true;
            Task task2 = Task.Run(() =>
            {
                Console.WriteLine($"執行緒2 的 ID={Thread.CurrentThread.ManagedThreadId} ( 輸出 - )");
                Thread.Sleep(900);
                for (int i = 0; i < 20; i++)
                {
                    Thread.Sleep(150); Console.Write("-");
                }
                //Console.WriteLine();
            });
            Console.WriteLine($"啟動執行兩個執行緒");

            //thread1.Start();
            //thread2.Start();

            //thread1.Join(); // 等待 執行緒1 執行完畢
            //thread2.Join(); // 等待 執行緒2 執行完畢

            //task1.Wait();
            //task2.Wait();
            Console.WriteLine();

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();

            /*
             這個範例是在說如果你不Join或wait thread，那麼當執行到Console.ReadKey()你把它按掉
             main thread就會被結束，其它在這main thread底下的thread就會一起被kill
             */
        }
    }
}
