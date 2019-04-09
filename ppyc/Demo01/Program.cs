using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo01
{
    class Program
    {
        static void Metodo1()
        {
            Console.WriteLine("Se inicio metodo 1");
        }
        static void Metodo2()
        {
            Console.WriteLine("Se inicio metodo 2");
        }

        static void Main(string[] args)
        {
            Task t1 = Task.Factory.StartNew(Metodo1);

            Task.WaitAll(t1);

            Task t2 = new Task(Metodo2);
            t2.Start();

            Task.WaitAll(t2);

            Console.WriteLine("Presione Enter para terminar le programa");
            Console.Read();
        }
    }
}
