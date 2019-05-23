using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CancelarParallelFor
{
    class Program
    {
        static void Main(string[] args)
        {
            int cancelValue = 3;
            //if(!int.TryParse(args[0], out cancelValue))
            //{
            //    return;
            //}

            Parallel.For(0, 6, (index, loopState) =>
            {
                Console.WriteLine("Tarea {0} inició...", index);
                HalfOperation();
                if(cancelValue == index)
                {
                    loopState.Break();
                    Console.WriteLine("Operacion loop cancelando. Tarea {0} cancelada...", index);
                    return;
                }
                if(loopState.LowestBreakIteration.HasValue)
                {
                    if(index > loopState.LowestBreakIteration)
                    {
                        Console.WriteLine("Tarea {0} cancelada.", index);
                        return;
                    }
                }
                HalfOperation();
                Console.WriteLine("Tarea {0} finalizó.", index);
            });

            Console.WriteLine("Presione Enter para finalizar!!");
            Console.ReadLine();
        }

        static void HalfOperation()
        {
            Thread.SpinWait(int.MaxValue / 10);
        }
    }
}
