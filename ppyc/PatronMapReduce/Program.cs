using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PatronMapReduce
{
    class Program
    {
        static int[] enteros = new int[] { 1, 10, 4, 10, 20, 30, 5 };
        static int count = 0;

        static void Main(string[] args)
        {
            Parallel.For(0, enteros.Length, () => 0, (index, loopState, subtotal) =>
            {
                if (enteros[index] > 5)
                {
                    ++subtotal;
                    Console.WriteLine("Hilo {0}: Tarea {1}: Valor {2}, Parcial {3}", Thread.CurrentThread.ManagedThreadId, index, enteros[index], subtotal);
                }
                return subtotal;
            },
            (subtotal) =>
            {
                Interlocked.Add(ref count, subtotal);
            });
            Console.WriteLine("El contador es: {0}\n", count);
            Console.WriteLine("Preione Enter para continuar.");
            Console.ReadLine();

        }
    }
}
