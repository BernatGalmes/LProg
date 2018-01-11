using System;
using System.Collections.Generic;
using System.Linq;

namespace LP_week6_DataGrid.Classes
{
    class Data
    {
        const int RANDOM_SEED = 15;

        const int N_STUDENTS = 200;
        const int N_DEGREES = 3;

        private List<Student> students;
        private List<Degree> degrees;

        public List<Student> Students
        {
            get
            {
                return students;
            }

            set
            {
                students = value;
            }
        }

        public Data()
        {
            this.students = new List<Student>();
            this.degrees = new List<Degree>();
            generateData();
        }

        public List<Student> filterByName(String fil)
        {
            return this.students.Where(st => st.Name.Contains(fil)).ToList();
        }

        public List<Student> filterBySurname(String fil)
        {
            return this.students.Where(st => st.Surname.Contains(fil)).ToList();
        }

        public List<Student> filterByDegree(String fil)
        {
            return this.students.Where(st => st.Degree.Name.Contains(fil) || st.Degree.Code.ToString().Contains(fil)).ToList();
        }

        private void generateData()
        {
            int i;
            Random num_gen = new Random(RANDOM_SEED);

            //generate degrees
            i = 0;
            while (i < N_DEGREES)
            {
                this.degrees.Add(gen_randDegree(num_gen, i));
                i++;
            }

            //generate students
            i = 0;
            while (i < N_STUDENTS)
            {
                this.students.Add(gen_randStudent(num_gen));
                i++;
            }

        }

        private Student gen_randStudent(Random r)
        {
            string name = getRandomString(r, 10);
            string surname = getRandomString(r, 10);

            char sex;

            if (r.Next(1, 2) == 0)
            {
                sex = 'M';
            }
            else
            {
                sex = 'F';
            }

            DateTime? birthday = new DateTime(DateTime.MinValue.Ticks);

            Degree d = this.degrees[r.Next(0, N_DEGREES - 1)];

            return new Student(name, surname, sex, d);
        }

        private Degree gen_randDegree(Random r, int code)
        {
            string name = getRandomString(r, 10);
            return new Degree(code, name);
        }

        private string getRandomString(Random r, int n_chars)
        {
            string input = "abcdefghijklmnopqrstuvwxyz0123456789";
            string generated = "";

            int i = 0;
            while (i < n_chars)
            {
                char c = (char)input[r.Next(0, input.Length - 1)];
                generated = generated + c;
                i++;
            }
            return generated;
        }

        public void viewData()
        {
            Console.WriteLine("Table of students: ");
            foreach (var st in this.students)
            {
                Console.WriteLine("Student: " + st);
            }

            Console.WriteLine("Table of degrees: ");
            foreach (var dg in this.degrees)
            {
                Console.WriteLine("Student: " + dg);
            }
        }
    }
}
