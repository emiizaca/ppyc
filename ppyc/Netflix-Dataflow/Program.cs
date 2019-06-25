using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netflix_Dataflow
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Task<int>> tareas = new List<Task<int>>();

            for (int i = 1; i <= 10; i++) //creo 10 tareas que devuelven un numero;
            {
                int valorBase = i;
                tareas.Add(Task.Factory.StartNew((b) =>
                {
                    int n = (int)b;
                    return n * n;
                }, valorBase));
            }

            var continuacion = Task.WhenAll(tareas);

            Task<int> t2 = Task.Factory.StartNew(() =>
            {
                int suma = 0;
                for (int i = 1; i < continuacion.Result.Length; i++)
                {
                    Console.WriteLine("La tarea {0} devuelve el número: {1}", i, continuacion.Result[i]);
                    suma += continuacion.Result[i];
                }
                return suma;
            });

            Console.WriteLine("La suma de todos los numeros es: {0}", t2.Result);
            Console.ReadLine();
        }
    }
}
