using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise_1
{
    class Terrain
    {

        const int SIZE = 100;

        private Cell[,] terrain;

        
        public Terrain()
        {
            terrain = new Cell[SIZE, SIZE];

            //init the terrain with different types of cells
            for(int i = 0; i < SIZE; i++)
            {
                for(int j = 0;j < SIZE; j++)
                {
                    if (i % 3 == 0)
                    {
                        terrain[i, j] = new normal_cell();
                    }
                    else if (i % 5 == 0)
                    {
                        terrain[i, j] = new safe_cell();
                    }
                    else if (i % 7 == 0)
                    {
                        terrain[i, j] = new dang_cell();
                    }
                    else
                    {
                        terrain[i, j] = new Cell();
                    }
                }
            }
        }

        /// <summary>
        /// Get the size of the terrain
        /// </summary>
        public static int SIZE1
        {
            get
            {
                return SIZE;
            }
        }

        /// <summary>
        /// Get the cell of the given position
        /// </summary>
        /// <param name="p">Position of the cell that you want to get</param>
        /// <returns>The cell of the given position</returns>
        public Cell read(Position p)
        {
            return terrain[p.X, p.Y];
        }


        /// <summary>
        /// Put a cell on the given position of the terrain
        /// </summary>
        /// <param name="p">Position of the cell that you want to get</param>
        /// <param name="value">Cell that you want to put</param>
        public void write(Position p, Cell value)
        {
            terrain[p.X, p.Y] = value;
        }
    }
}
