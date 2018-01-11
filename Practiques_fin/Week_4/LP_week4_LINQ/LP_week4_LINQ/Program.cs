using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LP_week4_LINQ
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Person> p_list = new List<Person>();

            p_list.Add(new Person("Antonia", 'M', new DateTime(1991, 12,31)));
            p_list.Add(new Person("Toni", 'F'));
            p_list.Add(new Person("Toni", 'M', new DateTime(2011, 10, 31)));
            p_list.Add(new Person("Biela", 'F', new DateTime(1985, 12, 4)));
            p_list.Add(new Person("Toni", 'M', new DateTime(1991, 1, 12)));
            p_list.Add(new Person("Maria", 'F', new DateTime(2008, 12, 31)));
            p_list.Add(new Person("Toni", 'M'));
            p_list.Add(new Person("Joana", 'F', new DateTime(1965, 12, 31)));
            p_list.Add(new Person("Toni", 'M', new DateTime(1972, 12, 31)));
            p_list.Add(new Person("Amparo", 'F', new DateTime(1986, 12, 12)));
            p_list.Add(new Person("Toni", 'M'));
            p_list.Add(new Person("Loli", 'F', new DateTime(1991, 12, 12)));
            p_list.Add(new Person("Toni", 'M', new DateTime(2000, 12, 31)));
            p_list.Add(new Person("Yoli", 'F'));
            p_list.Add(new Person("Toni", 'M', new DateTime(2005, 12, 31)));

            p_list.Add(new Person("Toni", 'M', new DateTime(1996, 12, 31)));//17
            p_list.Add(new Person("Toni", 'M', new DateTime(1997, 12, 31)));//18
            p_list.Add(new Person("Toni", 'M', new DateTime(2008, 12, 31)));//19

            


            //Show all persons
            print_queryResult(p_list, "All persons");

            //List of female Persons
            var Query_females =
                from p in p_list
                where p.P_sex == 'F'
                select p;

            Console.WriteLine();
            print_queryResult(Query_females, "List of female persons");

            //persons greater than 18
            DateTime now = DateTime.Today;
            DateTime Date_18ago = new DateTime(now.Year - 18, now.Month, now.Day);
            var Query_18 =
                from p in p_list
                where p.getAge() > 18
                select p;
            
            print_queryResult(Query_18, "List of persons greater than 18");

            //person greater than 21 years
            DateTime Date_21ago = new DateTime(now.Year - 21, now.Month, now.Day);
            var Query_21 =
                from p in p_list
                where p.getAge() > 21
                select p;
            
            print_queryResult(Query_21, "List of persons greater than 21");

            //the oldest person
            var Query_older = 
                from p in p_list
                where p.getAge() == p_list.Max(m => m.getAge())
                select p;

            print_queryResult(Query_older, "older guy");

            //Difference between older and younger
            var ages = from p in p_list where p.getAge() != -1 select p.getAge();
            var age_difference = ages.Max() - ages.Min();

            Console.WriteLine("\nDifference between older and younger: " + age_difference);

            //Average age
            var averageAge = ages.Average();

            Console.WriteLine("\nAverage age: " + averageAge);

            //Average age by sex
            var avgMale = p_list.Where(x=> x.P_sex =='M').Select(x => x.getAge()).Average();
            var avgFemale = p_list.Where(x => x.P_sex == 'F').Select(x => x.getAge()).Average();
            Console.WriteLine("\nMale avg age: " + avgMale);
            Console.WriteLine("Female avg age: " + avgFemale);

            //Male person sorted youngest to oldest
            var males_sortedByAge =
                from p in p_list
                where p.getAge() != -1 && p.P_sex == 'M'
                orderby p.getAge() ascending
                select p;

            print_queryResult(males_sortedByAge, "Males sorted by age");

            //check if all person have birthday filled
            var num_WithoutBirth = (from p in p_list where p.getAge() == -1 select p).Count();

            if(num_WithoutBirth != 0)
            {
                Console.WriteLine("\nThere are persons without birthday setted!!");
            }else
            {
                Console.WriteLine("\nEvery body have birthday setted!!");
            }


        }

        private static void print_queryResult( dynamic result_query, string QueryTitle)
        {
            Console.WriteLine(" \n"+QueryTitle+":");
            foreach (Person P in result_query)
            {
                Console.WriteLine(P.ToString());
            }
        }
    }
}
