using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections;
using System.Timers;

namespace Netflix
{
    class Program
    {
        private static string dirArchivo = "D:\\Uni\\Programacion Paralela y Concurrente\\ratings.txt";
        private static Dictionary<string, int> puntuaciones = new Dictionary<string, int>();
        private static System.Timers.Timer aTimer;

        static void Main(string[] args)
        {
            string linea = "";
            string[] usuarioPuntaje;

            StreamReader ratings = new StreamReader(dirArchivo);

            while (linea != null)
            {
                linea = ratings.ReadLine();//leo linea por linea
                if (linea != null)
                {
                    usuarioPuntaje = linea.Split(',');//split y tomo solo el elemento del identificador de usuario
                    if (puntuaciones.ContainsKey(usuarioPuntaje[1]))
                    {
                        puntuaciones[usuarioPuntaje[1]]++;
                    }
                    else
                    {
                        puntuaciones[usuarioPuntaje[1]] = 1;
                    }
                }
            }
            ratings.Close();

            var solo10 = puntuaciones.OrderByDescending(x => x.Value).ToList().Take(10);//creo una nueva lista, ordenando descendientemente los primeros 10

            Console.WriteLine("Usuario               Cantidad de veces que punteo");
            foreach (var item in solo10)//Recorro el diccionario para mostrar los valores
            {
                Console.WriteLine("{0}              {1} ", item.Key, item.Value);
            }

            Console.Read();
        }
    }
}
