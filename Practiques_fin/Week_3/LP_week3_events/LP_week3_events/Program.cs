using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LP_week3_events
{
    class Program
    {
        static void Main(string[] args)
        {
            Person P = new Person ("Biel");

            // Create a class that listens to the list's change event.
            EventListener listener = new EventListener(P);

            Console.WriteLine("Iniciam programa");
            try
            {
                P.Name = "Joan";

                P.Name = "Antoni";

                P.Name = "Antoni";//check that nothing happen when put the same name

                P.Name = "Maria";//check that nothing happen when put the same name

                P.Name = "Pancuato";
            }
            catch(Exception e)
            {
                Console.WriteLine("Too ugly name!!");
            }
            
        }
    }
}
