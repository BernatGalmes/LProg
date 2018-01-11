using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LP_week3
{
    class Person : IComparable
    {
        private string Name;
        private DateTime Birthday;

        public Person(String name, DateTime birthday)
        {
            this.Name = name;
            this.Birthday = birthday;
        }


        public override bool Equals(Object obj)
        {
            if (!(obj is Person)) return false;

            Person p_comp = (Person)obj;
            return Birthday == p_comp.Birthday & Name == p_comp.Name;
        }

        public override int GetHashCode()
        {
            return Birthday.GetHashCode() ^ Name.GetHashCode();
        }

        public override string ToString()
        {
            return "Name: " + Name + ", birthdate: " + Birthday;
        }

        /**
         * return 
         *      <0  -> this instance precedes obj in the sort order. 
         *      0   -> This instance occurs in the same position in the sort order as obj.
         *      >0  -> This instance follows obj in the sort order.   
         */
        public int CompareTo(object obj)
        {
            if (!(obj is Person))
                throw new Exception("bad use");

            Person pComp = (Person)obj;
            int value = this.Name.CompareTo(pComp.Name);

            if (value == 0)
            {
                return this.Birthday.CompareTo(pComp.Birthday);
            }
            return value;
        }
    }
}
