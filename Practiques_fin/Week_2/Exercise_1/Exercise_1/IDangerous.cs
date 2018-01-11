using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise_1
{
    interface IDangerous
    {
        /// <summary>
        /// Gets the probability of damage of the cell
        /// </summary>
        /// <returns></returns>
        float GetProbabilityDamage();
    }

    public class dang_cell : Cell, IDangerous
    {
        public float GetProbabilityDamage()
        {
            return 80.9F;
        }
    }

    public class safe_cell : Cell, IDangerous
    {
        public float GetProbabilityDamage()
        {
            return 5.2F;
        }
    }

    public class normal_cell : Cell, IDangerous
    {
        public float GetProbabilityDamage()
        {
            return 50.6F;
        }
    }
}
