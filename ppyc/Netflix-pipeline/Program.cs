using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netflix_pipeline
{
    class Program
    {
        static void Main(string[] args)
        {
            string archivo = "D:\\Universidad\\Programacion Paralela y Concurrente\\ratings.txt"; //direccion de archivo!

            Task<List<int>> t1 = Task.Factory.StartNew(() =>
            {
                List<int> usuarios = new List<int>();

                foreach (var linea in File.ReadLines(archivo))
                {
                    //
                    // movie id, user id, rating (1..5), date (YYYY-MM-DD)
                    //
                    string[] tokens = linea.Split(',');

                    int movieid = Convert.ToInt32(tokens[0]);
                    int userid = Convert.ToInt32(tokens[1]);
                    int rating = Convert.ToInt32(tokens[2]);
                    DateTime date = Convert.ToDateTime(tokens[3]);

                    usuarios.Add(userid);
                }
                return usuarios;
            });

            Task<Dictionary<int, int>> t2 = t1.ContinueWith((antecedente) =>
            {
                Dictionary<int, int> ReviewsPorUsuario = new Dictionary<int, int>();

                foreach (var usuarioId in antecedente.Result)
                {
                    if (!ReviewsPorUsuario.ContainsKey(usuarioId))
                        ReviewsPorUsuario.Add(usuarioId, 1);
                    else
                        ReviewsPorUsuario[usuarioId]++;
                }
                return ReviewsPorUsuario;
            });

            t2.Wait();

            var top10 = t2.Result.OrderByDescending(x => x.Value).Take(10);

            Console.WriteLine();
            Console.WriteLine("** Top 10 usuarios con mas reviews de peliculas");

            foreach (var user in top10)
                Console.WriteLine("{0}: {1}", user.Key, user.Value);

            Console.WriteLine("Presione Enter para finalizar");
            Console.ReadLine();
        }
    }
}

//https://docs.microsoft.com/es-es/dotnet/api/system.threading.tasks.task.continuewith?view=netframework-4.8#System_Threading_Tasks_Task_ContinueWith_System_Action_System_Threading_Tasks_Task_System_Object__System_Object_