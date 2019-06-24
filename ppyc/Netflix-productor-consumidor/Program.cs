using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netflix_productor_consumidor
{
    class Program
    {
        static void Main(string[] args)
        {
            string archivo = "D:\\Universidad\\Programacion Paralela y Concurrente\\ratings.txt"; //direccion de archivo!

            int max = 10000; //tamaño de la coleccion entre el productor y consumidor

            BlockingCollection<string> workQ = new BlockingCollection<string>(max);

            // Note: inform .NET scheduler that tasks are long-running:
            var fabrica = new TaskFactory(TaskCreationOptions.LongRunning, TaskContinuationOptions.None);

            Task productor = fabrica.StartNew(() =>
            {
                //El productor pone las lineas del archivo en la work Queue
                foreach (var linea in File.ReadLines(archivo))
                {
                    workQ.Add(linea);
                }

                // Señal de que hemos terminado de añadir a la cola:
                workQ.CompleteAdding();
            }
            );

            Dictionary<int, int> ReviewsByUser = new Dictionary<int, int>();
            int numCores = System.Environment.ProcessorCount; //numero de cores en nuestra pc

            Task<Dictionary<int, int>>[] consumidores = new Task<Dictionary<int, int>>[numCores];

            for (int i = 0; i < numCores; i++) //Un consumidor por core
            {

                consumidores[i] = fabrica.StartNew<Dictionary<int, int>>(() =>
                {
                    Dictionary<int, int> localD = new Dictionary<int, int>();

                    while (!workQ.IsCompleted)
                    {
                        try
                        {
                            string linea = workQ.Take();
                            int userid = parse(linea);

                            if (!localD.ContainsKey(userid))
                                localD.Add(userid, 1);
                            else
                                localD[userid]++;
                        }
                        catch (ObjectDisposedException)
                        {
                            //ignorar la exception
                        }
                        catch (InvalidOperationException)
                        {
                            //ignorar la exception
                        }
                    }//fin del while

                    return localD;
                });//fin lampda de cosumidores
            }//fin del for

            int completado = 0;

            while(completado < numCores)
            {
                int tid = Task.WaitAny(consumidores);

                Dictionary<int, int> localD = consumidores[tid].Result;

                foreach (var userid in localD.Keys)
                {
                    int numReviews = localD[userid];

                    if (!ReviewsByUser.ContainsKey(userid))
                        ReviewsByUser.Add(userid, numReviews);
                    else
                        ReviewsByUser[userid] += numReviews;
                }

                completado++;
                consumidores = consumidores.Where((t) => t != consumidores[tid]).ToArray();
            }//fin while

            var top10 = ReviewsByUser.OrderByDescending(x => x.Value).Take(10);

            Console.WriteLine();
            Console.WriteLine("** Top 10 usuruarios con mas reviews de peliculas");

            foreach (var user in top10)
                Console.WriteLine("{0}: {1}", user.Key, user.Value);

            Console.WriteLine("Presione Enter para finalizar");
            Console.ReadLine();

        }//fin del main

        private static int parse(string line)
        {
            char[] separators = { ',' };

            //
            // movie id, user id, rating (1..5), date (YYYY-MM-DD)
            //
            string[] tokens = line.Split(separators);

            int movieid = Convert.ToInt32(tokens[0]);
            int userid = Convert.ToInt32(tokens[1]);
            int rating = Convert.ToInt32(tokens[2]);
            DateTime date = Convert.ToDateTime(tokens[3]);

            return userid;
        }
    }
}
