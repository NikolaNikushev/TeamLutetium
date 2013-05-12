using System;
using System.Collections.Generic;

namespace BalloonsPop5Game
{
    public class ScoreBoard
    {

       private readonly List<Player> winnerBoard = new List<Player>();

       public ScoreBoard() 
       {

       }

       public int GetLength()
       {
           return this.winnerBoard.Count;
       }

        public Player this[int index]
        {
            get
            {
                return this.winnerBoard[index];
            }
        }
       public List<Player> SortWinnerBoard(string[,] tableToSort)
        {
            for (int row = 0; row < 5; ++row)
            {
                if (tableToSort[row, 0] == null)
                {
                    break;
                }
                
                winnerBoard.Add(new Player(int.Parse(tableToSort[row, 0]), tableToSort[row, 1]));
            }
            winnerBoard.Sort();

            return winnerBoard;
        }

       public bool CheckIfSkilled(Player player)
       {
           bool isSkilled = false;

           for (int chartPosition = 0; chartPosition < 5; chartPosition++)
           {
               if (this.winnerBoard[chartPosition] == null)
               {
                   AddToChart(player);
                   isSkilled = true;
                   break;
               }
           }

           int worstMoves = 0;
           int worstMovesChartPosition = 0;
           if (isSkilled == false)
           {
               for (int i = 0; i < 5; i++)
               {
                   if (this.winnerBoard[0].Moves > worstMoves)
                   {
                       worstMovesChartPosition = i;
                       worstMoves = this.winnerBoard[i].Moves;
                   }
               }
           }
           if (player.Moves < worstMoves && isSkilled == false)
           {
               chart = ConsoleRenderer.AddToChart(this.winnerBoard, worstMovesChartPosition);
               isSkilled = true;
           }
           return isSkilled;
       }

       public void AddToChart(Player player)
       {
          winnerBoard.Add(player);
       }

    }
}