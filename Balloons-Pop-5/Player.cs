using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BalloonsPop5Game
{
    public class Player : IComparable<Player>
    {
        public int Moves { get; private set; }
        public string Name { get; private set; }

        public Player(int moves, string name)
        {
            this.Moves = moves;
            this.Name = name;
        }

        public void UpdateMovesAmmount()
        {
            this.Moves++;
        }

        public Player(int moves)
        {
            this.Moves = moves;
        }

        public int CompareTo(Player other)
        {
            if (this.Moves>other.Moves)
            {
                return 1;
            }
            else if (this.Moves==other.Moves)
            {
                return 0;
            }
            else if(this.Moves<other.Moves)
            {
                return -1;
            }
            else
            {
                throw new ArgumentException(
                    "The properties Value of different instances could be either greater than each other or equal.");
            }
        }
    }
}
