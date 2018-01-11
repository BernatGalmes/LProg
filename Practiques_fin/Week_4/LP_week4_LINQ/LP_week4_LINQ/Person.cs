using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LP_week4_LINQ
{
    class Person
    {
        private String Name;
        private char Sex;
        private DateTime? Birthday;

        public Person (String name, char sex, DateTime? birthday = null)
        {
            Name = name;
            Sex = sex;
            Birthday = birthday;
        }

        public String P_Name
        {
            set
            {
                Name = value;
            }
            get {
                return Name;
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

        public DateTime? P_birthday
        {
            set
            {
                Birthday = value;
            }
            get
            {
                if (Birthday == null)
                {
                    return new DateTime();
                }
                return Birthday;
            }
        }

        /// <summary>
        /// Gets the age of the person
        /// </summary>
        /// <returns></returns>
        public int getAge()
        { 
            if (Birthday == null)
            {
                return -1;
            }

            DateTime now = DateTime.Today;
            DateTime bd = (DateTime)Birthday;
            bool Reached = false;
            if (bd.Month == now.Month) //ha nacido este mes
            {
                if (bd.Day < now.Day) // aun no ha cumplido
                {
                    Reached = true;
                }
            }
            else
            {
                if (bd.Month < now.Month) //aun no ha cumplido
                {
                    Reached = true;
                }
            }

            if (Reached)
            {
                return now.Year - bd.Year;
            }
            return now.Year - bd.Year - 1;
        }


        public override string ToString()
        {
            return "Name: " + Name + ", Sex: " + Sex + ", Birthday: " + Birthday + ", Age: " + this.getAge();
        }

    }
}
