using System;
using System.Linq;

namespace BalloonsPop
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

        //Used to get the final score
        public void UpdateMovesAmmount()
        {
            this.Moves++;
        }

        public Player()
        {
            this.Moves = 0;
        }

        //Used to sort the players on the winner board.
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
