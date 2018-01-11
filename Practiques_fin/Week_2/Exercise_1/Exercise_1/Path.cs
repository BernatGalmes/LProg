using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise_1
{
    /// <summary>
    /// Object of class Path represents a sequence of positions that describe
    /// a path in the terrain
    /// </summary>
    class Path : IEnumerable
    {
        private Position [] path;
        private Terrain Terr;
        private int last;

        const int MAX_PATH = 10;
        
         /// <summary>
         /// Construct of a path
         /// </summary>
         /// <param name="terr">Terrain where the path is</param>
         /// <param name="initialPos">Initial position of the path</param>
        public Path(ref Terrain terr, Position initialPos)
        {
            Terr = terr;
            path = new Position[MAX_PATH];

            last = 0;
            path[last] = initialPos;
            
        }

        /// <summary>
        /// Allows acces to path with indexer
        /// </summary>
        /// <param name="pos">position of the path that you want to get</param>
        /// <returns>Position in the terrain that belongs to given position of the path</returns>
        public Position this[int pos]
        {
            get
            {
                if (pos >= path.Length)
                {
                    throw new Exception("Too long path");
                }
                return path[pos];                
            }
            set
            {
                if (pos >= path.Length)
                {
                    throw new Exception("Too high position");
                }
                path[pos] = value;
            }
        }

        public int Last
        {
            get
            {
                return last;
            }
        }
        
        /// <summary>
        /// Moves the path to the position of the terrain in the given direction 
        /// from the current position.
        /// </summary>
        /// <param name="direction">Takes values 'RIGHT' 'LEFT' 'UP' 'DOWN' </param>
        public void Move(string direction)
        {
            Position prevPos = this.path[last];
            last++;
            switch (direction)
            {
                case "LEFT":
                    this.path[last] = new Position(prevPos.X - 1, prevPos.Y);
                    break;

                case "RIGHT":
                    this.path[last] = new Position(prevPos.X + 1, prevPos.Y);
                    break;

                case "UP":
                    this.path[last] = new Position(prevPos.X, prevPos.Y + 1);
                    break;
                case "DOWN":
                    this.path[last] = new Position(prevPos.X, prevPos.Y - 1);
                    break;
                default:
                    break;
            }
        }


        /// <summary>
        /// Returns the total cost of move throw the given terrain 
        /// (take into account that first and last cell are half traveled )
        /// </summary>
        /// <param name="t">Terrain of the path</param>
        public void GetCost(Terrain t)
        {
            int totalcost = 0;
            foreach(Position pos in path)
            {
                totalcost += t.read(pos).MovementCost;
            }
        }

        public void GenerateRandomPath()
        {
            this.Move("RIGHT");
            this.Move("RIGHT");
            this.Move("RIGHT");
            this.Move("RIGHT");
            this.Move("RIGHT");
            this.Move("UP");
            this.Move("UP");
        }

            
        /// <summary>
        /// Show the path at console
        /// </summary>     
        public void mostrarCami()
        {
            for(int i = 0; i <= last; i++)
            {
                Console.WriteLine(this.path[i]);
            }
        }

        public IEnumerator GetEnumerator()
        {
            return new MyEnumerator(this.path, this.last);
        }
        
        /// <summary>
        /// Implementation to allow foreach method
        /// </summary>
        private class MyEnumerator : IEnumerator
        {
            private Position[] path;
            int position;
            int last;

            //constructor
            public MyEnumerator(Position[] path, int last)
            {
                this.path = path;
                position = -1;
                this.last = last;
            }

            private IEnumerator getEnumerator()
            {
                return (IEnumerator)this;
            }


            //IEnumerator
            public bool MoveNext()
            {
                position++;
                return (position <= last);
            }

            //IEnumerator
            public void Reset()
            { position = -1; }

            //IEnumerator
            public object Current
            {
                get
                {
                    try
                    {
                        return path[position];
                    }

                    catch (IndexOutOfRangeException)
                    {
                        throw new InvalidOperationException();
                    }
                }
            }
        }         
    }
}
