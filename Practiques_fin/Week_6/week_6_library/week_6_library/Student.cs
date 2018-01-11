using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace week6_library
{
    public class Student
    {
        private String name;
        private String surname;
        private char Sex;
        private int degreeid;

        public Student(string name, string surname, char sex, int deg)
        {
            this.name = name;
            Surname = surname;
            Sex = sex;
            this.degreeid = deg;
        }

        public Student(string name, string surname)
        {
            this.name = name;
            Surname = surname;
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

        public int Degreeid
        {
            get
            {
                return degreeid;
            }

            set
            {
                degreeid = value;
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
