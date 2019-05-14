using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatronWaitAllOneByOne
{
    class Program
    {
        static void Main(string[] args)
        {
            Random r = new Random();
            int procesos = 10;
            int suma = 0;
            List<Task<int>> tareas = new List<Task<int>>();
            for (int i = 0; i < procesos; i++)
            {
                tareas.Add(Task.Factory.StartNew(() =>
                {
                    int numeroASumar = r.Next(1, 10);
                    return numeroASumar;
                }));
            }

            while (tareas.Count > 0)
            {
                int indice = Task.WaitAny(tareas.ToArray());
                Console.WriteLine("El proceso " + tareas[indice].Id + " finalizó, con el resultado: " + tareas[indice].Result);
                suma += tareas[indice].Result;
                tareas.RemoveAt(indice);
            }

            Console.WriteLine("El resultado de la suma es: " + suma);
            Console.Read();
        }
    }
}
