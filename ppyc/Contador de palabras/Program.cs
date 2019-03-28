using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contador_de_palabras
{
    class Program
    {
        private static Dictionary<string, int> contadorPalabras = new Dictionary<string, int>();

        static void Main(string[] args)
        {
            Console.WriteLine("Escriba un parrafo: "); //se le pide al usuario que ingrese el parrafo

            string parrafo = Console.ReadLine(); //se toma el parrafo de consola

            string[] palabras = parrafo.Split(' '); //se separan las palabras del parrafo en un arreglo

            foreach (var item in palabras)//Se recorre la lista, para llenar el diccionario
            {
                if (contadorPalabras.ContainsKey(item))
                {
                    contadorPalabras[item]++;
                }
                else
                {
                    contadorPalabras[item] = 1;
                }
            }

            Console.WriteLine("Contenido del diccionario: ");//mensaje al usuario

            foreach (var item in contadorPalabras)//Recorro el diccionario para mostrar los valores
            {
                Console.WriteLine("{0}   {1} ", item.Key, item.Value);
            }

            Console.WriteLine("Size: " + contadorPalabras.Count);//Mostrar cantidad de palabras distintas

            Console.Read();

        }
    }
}
