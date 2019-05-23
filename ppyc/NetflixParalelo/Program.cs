using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetflixParalelo
{
    class Program
    {
        static void Main(string[] args)
        {
            var sw = System.Diagnostics.Stopwatch.StartNew();

            string archivo = "D:\\Universidad\\Programacion Paralela y Concurrente\\ratings.txt";

            sw.Restart();

            Dictionary<int, int> ReviewsPorUsuario = new Dictionary<int, int>();

            Parallel.ForEach(File.ReadLines(archivo), //Origen de datos
                () => { return new Dictionary<int, int>(); }, //Inicializa almacen local(TLS)
                (line, loopState, tls) => //Cuerpo de la tarea
                {
                    int userID = parse(line);
                    if (!tls.ContainsKey(userID))
                        tls.Add(userID, 1);
                    else
                        tls[userID] += 1;
                    return tls;
                },
                (tls) => //Reduccion
                {
                    lock(ReviewsPorUsuario)
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

            long timems = sw.ElapsedMilliseconds;

            Console.WriteLine();
            Console.WriteLine("** Top 10 usuarios reviewing peliculas:");

            foreach (var user in top10)
                Console.WriteLine("{0}: {1}", user.Key, user.Value);

            double time = timems / 1000.0;  // convert milliseconds to secs

            Console.WriteLine();
            Console.WriteLine("** Done! Time: {0:0.000} secs", time);
            Console.WriteLine();

            Console.Write("Presione para salir...");
            Console.ReadKey();
        }



        private static int parse(string line)
        {
            char[] separators = { ',' };

            string[] tokens = line.Split(separators);

            int movieid = Convert.ToInt32(tokens[0]);
            int userid = Convert.ToInt32(tokens[1]);
            int rating = Convert.ToInt32(tokens[2]);
            DateTime date = Convert.ToDateTime(tokens[3]);

            return userid;
        }
    }
}
