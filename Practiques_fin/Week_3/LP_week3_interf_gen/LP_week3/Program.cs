using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LP_week3
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Person> ListPersons = new List<Person>();

            Person p1 = new Person("Joan", new DateTime(1991, 01, 12));
            ListPersons.Add(p1);

            Person p2 = new Person("Maria", new DateTime(1998, 4, 12));
            ListPersons.Add(p2);

            Person p3 = new Person("Lluis", new DateTime(2016, 4, 08));
            ListPersons.Add(p3);

            Person p4 = new Person("Lluis", new DateTime(2012, 4, 08));
            ListPersons.Add(p4);

            Person p5 = new Person("Miquel", new DateTime(2016, 4, 08));
            ListPersons.Add(p5);

            Person p6 = new Person("Antonia", new DateTime(2016, 4, 08));
            ListPersons.Add(p6);

            //Check if the given person exist
            bool trobat = false;
            Person p_looked = new Person("Miquel", new DateTime(2016, 4, 08));
            foreach ( Person p in ListPersons)
            {
                if (ListPersons.Contains(p_looked))
                {
                    trobat = true;
                    break;
                }
            }

            if (trobat)
            {
                Console.WriteLine("Person found");
            }
            else
            {
                Console.WriteLine("Person not found");
            }

            Console.WriteLine("Unsorted persons");
            foreach (Person p in ListPersons)
            {
                Console.WriteLine(p);
            }
            ListPersons.Sort();

            Console.WriteLine("Sorted persons");
            foreach (Person p in ListPersons)
            {
                Console.WriteLine(p);
            }

        }
    }
}
