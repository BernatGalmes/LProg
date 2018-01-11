using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Exercise_1
{
    class Program
    {
        static void Main(string[] args)
        {
            Terrain T = new Terrain();
            Position i_pos = new Position(0,0);
            Path path = new Path(ref T, i_pos);
            float totalProb;
            
            path.GenerateRandomPath();

            int i = 0;
            float probAcum = 0;
            try
            {
                Type[] argsMethod = Type.EmptyTypes;
                foreach (Position p in path)
                {
                    Cell c = T.read(p);
                    Type cellType = c.GetType();
                    Console.WriteLine(cellType.Name.ToString()+" Position: X="+p.X+" y="+p.Y);
                    MethodInfo getProbDam = cellType.GetMethod("GetProbabilityDamage", argsMethod);
                    if (getProbDam != null)
                    {
                        Object objProb = getProbDam.Invoke(c, null);
                        Console.WriteLine("\tDamage prob.: " +(float)objProb);
                        probAcum += (float)objProb;
                    }
                    i++;
                }
                if ( i != 0) // empty path
                {
                    totalProb = probAcum / i;
                }else
                {
                    totalProb = 0;
                }

                Console.WriteLine("Total probability damage = {0}", totalProb);
                Console.Read();
            }
            catch(Exception e)
            {
                Console.WriteLine(e.StackTrace);
                Console.WriteLine(e.Message);

            }

            
        }
    }
}
