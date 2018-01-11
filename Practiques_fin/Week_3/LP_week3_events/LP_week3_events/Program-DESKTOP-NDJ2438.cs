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
            P.Name_person = "Joan";

            P.Name_person = "Antoni";

            P.Name_person = "Pancuato";
        }
    }
}
