using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LP_week4_parallel_for
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] numbers = new int[10];
            
            for (int i = 0;i<numbers.Length;i++)
            {
                numbers[i] = i;
            }

            Console.WriteLine("Values using sequencial foreach: ");
            foreach (int n in numbers)
            {
                Console.WriteLine("Value: " + n);
            }

            Console.WriteLine("\nValues using parallel foreach: ");
            Parallel.ForEach(numbers,
                    n =>
                    {
                        Console.WriteLine("Value: " + n);
                    });

            //create a list of 4 expando objects with name and surname
            List<dynamic> l_eo = new List<dynamic>();


            l_eo.Add(new ExpandoObject());
            l_eo[0].Name = "Bernat";
            l_eo[0].Surname = "Galmés";

            l_eo.Add(new ExpandoObject());
            l_eo[1].Name = "Antonia";
            l_eo[1].Surname = "Santandreu";

            l_eo.Add(new ExpandoObject());
            l_eo[2].Name = "Miquel";
            l_eo[2].Surname = "Sureda";

            l_eo.Add(new ExpandoObject());
            l_eo[3].Name = "Maria";
            l_eo[3].Surname = "Bover";

            //display objects thaht its name contains an 'a'
            var xxx = from p in l_eo
                      where p.Name.Contains("a")
                      select p;
            Console.WriteLine("persons which name contains an 'a'");
            foreach (var p in xxx)
            {
                Console.WriteLine("name: {0} Surname: {1}", p.Name, p.Surname);
            }
        }
    }
}
