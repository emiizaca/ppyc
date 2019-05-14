using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abecedario
{
    class Program
    {
        static void Main(string[] args)
        {
            char letra = 'a';

            Task t1 = Task.Factory.StartNew(() =>
            {
                while (letra < 'z')
                {
                    if(letra % 2 == 0)
                    {
                        Console.WriteLine("La tarea 1 imprime: " + letra);
                        letra++;
                    }
                }
            });

            Task t2 = Task.Factory.StartNew(() =>
            {
                while (letra < 'z')
                {
                    if (letra % 2 != 0)
                    {
                        Console.WriteLine("La tarea 2 imprime: " + letra);
                        letra++;
                    }
                }
            });

            Task.WaitAll(t1, t2);

            Console.WriteLine("Presione Enter para terminar el programa");
            Console.Read();
        }
    }
}
