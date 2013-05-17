using System;
using System.Linq;

namespace BalloonsPop
{
    /// <summary>
    /// Class that represents a player 
    /// holding information about his name and his score
    /// </summary>
    public class Player : IComparable<Player>
    {
        /// <summary>
        /// Number of moves made by the player to finish game
        /// </summary>
        public int Moves { get; private set; }
        /// <summary>
        /// Name of the player
        /// </summary>
        public string Name { get; private set; }

        public Player(int moves, string name)
        {
            this.Moves = moves;
            this.Name = name;
        }

        //Used to get the final score
        /// <summary>
        /// Method that updates moves ammount
        /// by incrementing it with one
        /// </summary>
        public void UpdateMovesAmmount()
        {
            this.Moves++;
        }

        public Player()
        {
            this.Moves = 0;
        }

        //Used to sort the players on the winner board.
        /// <summary>
        /// Method needed to compare one player with another
        /// Makes comparison on the basis of the moves made.
        /// </summary>
        /// <param name="other">A parameter of type player
        /// Represents the player to which the comparison is made</param>
        /// <returns>Returns integer with value 1 if moves of the first are more,
        /// 0 if they are equal and -1 if second player has more moves
        /// </returns>
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
