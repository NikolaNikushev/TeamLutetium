using System;
using System.Collections.Generic;

namespace BalloonsPop5Game
{
    public class ScoreBoard : IComparable<ScoreBoard>
    {
        public int Value {get; private set;}
        public string Name {get;private set;}

        private readonly List<ScoreBoard> scoreBoard = new List<ScoreBoard>();

        public ScoreBoard(int value, string name)
        {
            this.Value = value;
            this.Name = name;
        }

        public int CompareTo(ScoreBoard other)
        {
            if (this.Value>other.Value)
            {
                return 1;
            }
            else if (this.Value==other.Value)
            {
                return 0;
            }
            else if(this.Value<other.Value)
            {
                return -1;
            }
            else
            {
                throw new ArgumentException(
                    "The properties Value of different instances could be either greater than each other or equal.");
            }
        }

       public List<ScoreBoard> SortWinnerBoard(string[,] tableToSort)
        {
            for (int row = 0; row < 5; ++row)
            {
                if (tableToSort[row, 0] == null)
                {
                    break;
                }
                scoreBoard.Add(new ScoreBoard(int.Parse(tableToSort[row, 0]), tableToSort[row, 1]));
            }
            scoreBoard.Sort();

            return scoreBoard;
        }

       public void PrintWinnerBoard()
        {
            if (scoreBoard.Count == 0)
            {
                Console.WriteLine("The score board is empty!");
            }
            else
            {
                Console.WriteLine("---------TOP FIVE Players-----------");
                for (int winnerPosition = 0; winnerPosition < scoreBoard.Count; ++winnerPosition)
                {
                    ScoreBoard slot = scoreBoard[winnerPosition];
                    Console.WriteLine("{2}.   {0} with {1} moves.", slot.Name, slot.Value, winnerPosition + 1);
                }
                Console.WriteLine("----------------------------------");
            }

            Console.WriteLine();
        }
    }
}