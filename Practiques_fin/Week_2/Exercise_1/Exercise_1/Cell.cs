using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise_1
{
    public class Cell
    {
        private int Cost;
        
        /// <summary>
        /// Get the cost to move to this cell
        /// </summary>
        public int MovementCost
        {
            get { return Cost; }
            set { Cost = value; }
        }
    }
}
