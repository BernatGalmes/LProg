using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise_1
{
    class Position
    {
        private int x;
        private int y;

        public Position(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
        public Position()
        {
        }
        public int X
        {
            get
            {
                return x;
            }

            set
            {
                x = value;
            }
        }

        public int Y
        {
            get
            {
                return y;
            }

            set
            {
                y = value;
            }
        }

        public override String ToString()
        {
             return "posX=" + this.X + ", posY=" + this.Y;
        }
    }
}
