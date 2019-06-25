using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netflix_WaitAllOneByOne
{
    class Program
    {
        static void Main(string[] args)
        {
            string archivo = "D:\\Universidad\\Programacion Paralela y Concurrente\\ratings.txt"; //direccion de archivo!

            int procesos = 8;
            List<Task<int>> tareas = new List<Task<int>>();

            for (int i = 0; i < procesos; i++)
            {
                tareas.Add(Task.Factory.StartNew(() =>
                {
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
                }));
            }
        }
    }
}
