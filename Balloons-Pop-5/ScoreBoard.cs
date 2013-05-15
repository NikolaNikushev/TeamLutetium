using System;
using System.Collections.Generic;

namespace BalloonsPop
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

        //Sorts the winner board by ascending order
       public void  SortWinnerBoard()
        {
            this.winnerBoard.Sort();
        }

        //Checks if the first 5 places are empty and adds the player to those places
       public bool CheckIfSkilledAndAddToBoard(Player player)
       {
           bool isSkilled = false;
           byte winnerMaxPosition = 5;
           for (int boardPosition = 0; boardPosition < winnerMaxPosition; boardPosition++)
           {
               if (this.winnerBoard.Count <= winnerMaxPosition || this.winnerBoard[boardPosition].Name == null)
               {
                   AddToBoard(ConsoleRenderer.AddPlayerToBoard(boardPosition, player.Moves));
                   isSkilled = true;
                   break;
               }
           }

           //Checks if the player current score beats his previous score and adds him to the top 5 board
           int worstMoves = 0;
           int worstMovesBoardPosition = 0;
           if (isSkilled == false)
           {
               for (int winnerPosition = 0; winnerPosition < winnerMaxPosition; winnerPosition++)
               {
                   if (this.winnerBoard[winnerPosition].Moves > worstMoves)
                   {
                       worstMovesBoardPosition = winnerPosition;
                       worstMoves = this.winnerBoard[winnerPosition].Moves;
                   }
               }
           }
           if (player.Moves < worstMoves && isSkilled == false)
           {
               AddToBoard(ConsoleRenderer.AddPlayerToBoard(worstMovesBoardPosition, player.Moves));
               isSkilled = true;
           }
           return isSkilled;
       }

       public void AddToBoard(Player player)
       {
          winnerBoard.Add(player);
       }

    }
}