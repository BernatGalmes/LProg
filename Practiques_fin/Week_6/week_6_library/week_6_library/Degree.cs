using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace week6_library
{
    public class Degree
    {
        private static int LAST_ID =0;

        private int id;
        private int code;
        private string name;
        
        public Degree(int code, string name)
        {
            this.code = code;
            this.Name = name;
            LAST_ID++;
            this.Id = LAST_ID;
        }

        public int Code
        {
            get
            {
                return code;
            }

            set
            {
                code = value;
            }
        }

        public string Name
        {
            get
            {
                return name;
            }

            set
            {
                name = value;
            }
        }

        public int Id
        {
            get
            {
                return id;
            }

            set
            {
                id = value;
            }
        }

        public override string ToString()
        {
            return "Name: " + code + " - " + Name;
        }
    }
}
