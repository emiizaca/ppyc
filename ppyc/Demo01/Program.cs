using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Abecedario_dos_Task
{
    class Program
    {
        static void Main(string[] args)
        {
            char letra = 'a';

            Task t1 = Task.Factory.StartNew(() => 
            {
                while(letra < 'z')
                {
                    Console.WriteLine("La tarea 1 imprime: " + letra);
                    letra++;
                }
                
            });
            Task t2 = Task.Factory.StartNew(() =>
            {
                while (letra < 'z')
                {
                    Console.WriteLine("La tarea 2 imprime: " + letra);
                    letra++;
                }
            });

            Task.WaitAll(t1, t2);

            Console.WriteLine("Presione Enter para terminar le programa");
            Console.Read();
        }
    }
}
