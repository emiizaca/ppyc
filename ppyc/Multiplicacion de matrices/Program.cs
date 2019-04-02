using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Multiplicacion_de_matrices
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("MATRIZ A");
            Console.WriteLine("Inserte el número de filas de la primer Matriz");
            int filas1 = int.Parse(Console.ReadLine());
            Console.WriteLine("Inserte el número de columnas de la primer Matriz");
            int columnas1 = int.Parse(Console.ReadLine());
            Console.WriteLine("MATRIZ B");
            Console.WriteLine("Inserte el número de filas de la segunda Matriz");
            int filas2 = int.Parse(Console.ReadLine());
            Console.WriteLine("Inserte el número de columnas de la segunda Matriz");
            int columnas2 = int.Parse(Console.ReadLine());

            int[,] matriz1 = new int[filas1, columnas1];
            int[,] matriz2 = new int[filas2, columnas2];
            int[,] matrizResultado = new int[filas1, columnas2];

            if (columnas1 == filas2)
            {
                Console.WriteLine("Ingrese los datos de la matriz 1: ");
                matriz1 = LlenarMatriz(matriz1);
                Console.WriteLine("La matriz 1, quedo de la siguiente manera: ");
                mostrarMatriz(matriz1);

                Console.WriteLine("Ingrese los datos de la matriz 2: ");
                matriz2 = LlenarMatriz(matriz2);
                Console.WriteLine("La matriz 2, quedo de la siguiente manera: ");
                mostrarMatriz(matriz2);

                matrizResultado = multiplicarMatrices(matriz1, matriz2, matrizResultado);
                Console.WriteLine("El resultado de la multiplicacion de las matrices es: ");
                mostrarMatriz(matrizResultado);
                Console.Read();
            }
        }

        private static int[,] multiplicarMatrices(int[,] matriz1, int[,] matriz2, int[,] resultado)
        {
            for (int f = 0; f < matriz1.GetLength(0); f++)
            {
                for (int c = 0; c < matriz2.GetLength(1); c++)
                {
                    resultado[f, c] = 0;
                    for (int z = 0; z < matriz1.GetLength(1); z++)
                    {
                        resultado[f, c] = matriz1[f, z] * matriz2[z, c] + resultado[f, c];
                    }
                }
            }
            return resultado;
        }

        private static void mostrarMatriz(int[,] matriz)
        {
            for (int f = 0; f < matriz.GetLength(0); f++)
            {
                for (int c = 0; c < matriz.GetLength(1); c++)
                {
                    Console.Write("{0}  ", matriz[f,c]);
                    if(c == (matriz.GetLength(1)-1))
                        Console.WriteLine();
                }
            }
        }

        private static int[,] LlenarMatriz(int[,] matriz)
        {
            for (int f = 0; f < matriz.GetLength(0); f++)
            {
                for (int c = 0; c < matriz.GetLength(1); c++)
                {
                    Console.WriteLine("Ingresa Dato (Fila: {0} - Columna: {1}): ", f + 1, c + 1);
                    matriz[f, c] = int.Parse(Console.ReadLine());
                }
            }
            return matriz;
        }
    }
}
