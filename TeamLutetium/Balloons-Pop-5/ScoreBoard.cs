using System;
using System.Collections.Generic;

namespace BalloonsPop
{
    /// <summary>
    /// Class that holds the information about 
    /// the best playrs and their results 
    /// </summary>
    public class ScoreBoard
    {
        /// <summary>
        /// Represents a list of Players
        /// </summary>
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

        /// <summary>
        /// Method that sorts the players in the winner board, according to their results
        /// </summary>
        public void SortWinnerBoard()
        {
            this.winnerBoard.Sort();
        }

        /// <summary>
        /// Method that adds only best players to board
        /// </summary>
        /// <param name="player">A parameter of type Player that holds information
        /// about the player name and score</param>
        /// <param name="communicator">A parameter implementing IUICommunicator interface,
        /// that executes the communication between the game itself and the human player</param>
        /// <returns>Returns if player was added to board(if he is skillful enough) and
        /// false if he wasn`t so skillful to be added</returns>
        public bool AddSkillfulPlayerToBoard(Player player, IUICommunicator communicator)
        {
            bool isSkilled = false;
            byte winnerMaxPosition = 5;
            for (int boardPosition = 0; boardPosition < winnerMaxPosition; boardPosition++)
            {
                if (this.winnerBoard.Count <= winnerMaxPosition || this.winnerBoard[boardPosition].Name == null)
                {
                    AddToBoard(communicator.ProvidePlayerPersonalData(boardPosition, player.Moves));
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
                AddToBoard(communicator.ProvidePlayerPersonalData(worstMovesBoardPosition, player.Moves));
                isSkilled = true;
            }
            return isSkilled;
        }
        
        private void AddToBoard(Player player)
        {
            winnerBoard.Add(player);
        }

    }
}