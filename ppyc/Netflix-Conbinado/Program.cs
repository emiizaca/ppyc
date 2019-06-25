using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netflix_Conbinado
{
    class Program
    {
        static void Main(string[] args)
        {
            string rutaArchivo = "D:\\Universidad\\Programacion Paralela y Concurrente\\ratings.txt";

            Task<List<int>> limpiarArchivo = Task.Factory.StartNew(() =>
            {
                List<int> usuarios = new List<int>();
                foreach (var linea in File.ReadLines(rutaArchivo))
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

            Dictionary<int, int> ReviewsPorUsuario = new Dictionary<int, int>();

            Parallel.ForEach(limpiarArchivo.Result, //Origen de datos
                () => { return new Dictionary<int, int>(); }, //Inicializa almacen local de cada hilo(TLS)
                (userID, loopState, tls) => //Cuerpo de la tarea
                {
                    if (!tls.ContainsKey(userID))
                        tls.Add(userID, 1);
                    else
                        tls[userID] += 1;
                    return tls;
                },
                (tls) => //Reduccion
                {
                    lock (ReviewsPorUsuario)
                    {
                        foreach (int userID in tls.Keys)
                        {
                            int cantVotos = tls[userID];
                            if (!ReviewsPorUsuario.ContainsKey(userID))
                                ReviewsPorUsuario.Add(userID, cantVotos);
                            else
                                ReviewsPorUsuario[userID] += cantVotos;
                        }
                    }
                });

            var top10 = ReviewsPorUsuario.OrderByDescending(x => x.Value).Take(10);

            Console.WriteLine();
            Console.WriteLine("** Top 10 usuarios reviewing peliculas:");

            foreach (var user in top10)
                Console.WriteLine("{0}: {1}", user.Key, user.Value);

            Console.Write("Presione para salir...");
            Console.ReadKey();
        }
    }
}
