using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LP_week6_DataGrid.Classes
{
    public class Student
    {
        private String name;
        private String surname;
        private char Sex;
        private Degree degree;

        public Student(string name, string surname, char sex, Degree deg)
        {
            this.name = name;
            Surname = surname;
            Sex = sex;
            this.degree = deg;
        }

        public String Name
        {
            set
            {
                name = value;
            }
            get
            {
                return name;
            }
        }

        public string Surname
        {
            get
            {
                return surname;
            }

            set
            {
                surname = value;
            }
        }

        public char P_sex
        {
            set
            {
                Sex = value;
            }
            get
            {
                return Sex;
            }
        }

        public Degree Degree
        {
            get
            {
                return degree;
            }

            set
            {
                degree = value;
            }
        }

        public override int GetHashCode()
        {
            //Get hash code for the Code field. 
            int hash2 = this.Name.GetHashCode();

            int hash3 = this.Surname.GetHashCode();

            int hash4 = this.Sex.GetHashCode();
            //Calculate the hash code for the product. 
            return hash2 ^ hash3 ^ hash4;
        }

        public override string ToString()
        {
            return "Name: " + name + ", Sex: " + Sex;
        }

    }
}
