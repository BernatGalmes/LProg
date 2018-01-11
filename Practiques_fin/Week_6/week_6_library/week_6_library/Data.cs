using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Windows.Controls;

namespace week6_library
{
    public class Data
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

        public List<Degree> Degrees
        {
            get
            {
                return degrees;
            }

            set
            {
                degrees = value;
            }
        }

        public Data()
        {
            this.students = new List<Student>();
            this.Degrees = new List<Degree>();
        }

        public DataTable DataTableStudents()
        {
            DataTable dataTable = new DataTable("students table");

            DataColumn column;

            //Setting column names as Property names
            column = new DataColumn();
            column.DataType = Type.GetType("System.String");
            column.ColumnName = "Name";
            dataTable.Columns.Add(column);

            column = new DataColumn();
            column.DataType = Type.GetType("System.String");
            column.ColumnName = "Surname";
            dataTable.Columns.Add(column);

            column = new DataColumn();
            column.DataType = Type.GetType("System.String");
            column.ColumnName = "Degreeid";
            dataTable.Columns.Add(column);
            // dataTable.PrimaryKey = new DataColumn[] {}
            /*DataGridComboBoxColumn c_deg = new DataGridComboBoxColumn();
            c_deg.Header = "Degree";
            dataTable.Columns.Add(c_deg);*/
            //c_deg.ItemsSource = item.Degree.Code.ToString();
            DataRow row;
            foreach (Student item in this.Students)
            {
                row = dataTable.NewRow();
                row["Name"] = item.Name;
                row["Surname"] = item.Surname;
                row["Degreeid"] = item.Degreeid;
                dataTable.Rows.Add(row);
            }
            //put a breakpoint here and check datatable
            return dataTable;
        }

        public DataTable DataTableDegree()
        {
            DataTable dataTable = new DataTable("Degree table");

            DataColumn column;

            //Setting column names as Property names
            column = new DataColumn();
            column.DataType = Type.GetType("System.String");
            column.ColumnName = "Name";
            dataTable.Columns.Add(column);

            column = new DataColumn();
            column.DataType = Type.GetType("System.String");
            
            column.ColumnName = "ID";
            dataTable.Columns.Add(column);

            column = new DataColumn();
            column.DataType = Type.GetType("System.String");
            column.ColumnName = "Code";
            dataTable.Columns.Add(column);

           
            foreach (Degree item in this.Degrees)
            {
                DataRow row;
                row = dataTable.NewRow();
                row["Name"] = item.Name;
                row["ID"] = item.Id;
                row["Code"] = item.Code;
                dataTable.Rows.Add(row);
            }
            //put a breakpoint here and check datatable
            return dataTable;

        }
        
        public List<Student> filterByName(String fil)
        {
            return this.students.Where(st => st.Name.Contains(fil)).ToList();
        }

        public List<Student> filterBySurname(String fil)
        {
            return this.students.Where(st => st.Surname.Contains(fil)).ToList();
        }

      /*  public List<Student> filterByDegree(String fil)
        {
            return this.students.Where(st => st.Degree.Name.Contains(fil) || st.Degree.Code.ToString().Contains(fil)).ToList();
        }*/

        public void generateData()
        {
            int i;
            Random num_gen = new Random(RANDOM_SEED);

            //generate degrees
            i = 0;
            while (i < N_DEGREES)
            {
                this.Degrees.Add(gen_randDegree(num_gen, i));
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

            Degree d = this.Degrees[r.Next(0, N_DEGREES)];

            return new Student(name, surname, sex, d.Id);
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
            foreach (var dg in this.Degrees)
            {
                Console.WriteLine("Student: " + dg);
            }
        }
    }
}
