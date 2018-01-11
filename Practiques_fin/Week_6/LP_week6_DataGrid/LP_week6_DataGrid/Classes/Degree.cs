using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LP_week6_DataGrid.Classes
{
    public class Degree
    {
        private int code;
        private string name;
        
        public Degree(int code, string name)
        {
            this.code = code;
            this.Name = name;
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

        public override string ToString()
        {
            return "Name: " + Name + " - " + Code;
        }
    }
}
