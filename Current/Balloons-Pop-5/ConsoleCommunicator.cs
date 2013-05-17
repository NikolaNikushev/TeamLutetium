using System;
using System.Linq;

namespace BalloonsPop
{
    /// <summary>
    /// Used to display information to the console.
    /// </summary>
    public class ConsoleCommunicator : IUICommunicator
    {
        /// <summary>
        /// Prints the field and game instructions to the console.
        /// </summary>
        /// <param name="field">The field that will be printed</param>
        public void RenderGameField(IPlayField field)
        {
            Console.Clear();
            Console.WriteLine("Welcome to “Balloons Pops” game. " +
            "Please try to pop the balloons. \nUse 'top' to view the top scoreboard,\n" +
            "'restart' to start a new game and 'exit' to quit the game. \n");
            Console.Write("    ");
            for (byte column = 0; column < field.GetLength(1); column++)
            {
                Console.Write(column + " ");
            }

            Console.Write("\n   ");
            for (byte column = 0; column < field.GetLength(1) * 2 + 1; column++)
            {
                Console.Write("-");
            }

            Console.WriteLine();

            for (byte row = 0; row < field.GetLength(0); row++)
            {
                Console.Write(row + " | ");
                for (byte col = 0; col < field.GetLength(1); col++)
                {
                    if (field[row, col] == 0)
                    {
                        Console.Write("  ");
                        continue;
                    }
                    Console.Write(field[row, col] + " ");
                }
                Console.Write("| ");
                Console.WriteLine();
            }

            Console.Write("   ");
            for (byte column = 0; column < field.GetLength(1) * 2 + 1; column++)
            {
                Console.Write("-");
            }
            Console.WriteLine();
        }

        /// <summary>
        /// Prints the winner board in ascending order
        /// </summary>
        /// <param name="scoreBoard">The current score board that will be printed</param>
        public void RenderWinnerBoard(ScoreBoard scoreBoard)
        {
            if (scoreBoard.GetLength() == 0)
            {
                Console.WriteLine("The score board is empty!");
            }
            else
            {
                byte scoreBoardWinnerCount = (byte)scoreBoard.GetLength();
                if (scoreBoardWinnerCount > 5)
                {
                    scoreBoardWinnerCount = 5;
                }

                Console.WriteLine("---------TOP FIVE Players-----------");
                for (int winnerPosition = 0; winnerPosition < scoreBoardWinnerCount; ++winnerPosition)
                {
                    Player player = scoreBoard[winnerPosition];
                    Console.WriteLine("{2}.   {0} with {1} moves.", player.Name, player.Moves, winnerPosition + 1);
                }
                Console.WriteLine("----------------------------------");
            }

            Console.WriteLine();
        }

        /// <summary>
        /// Used to get the player name from the console and current position
        /// that he will be taking with the ammount of moves that he has made
        /// </summary>
        /// <param name="chartPosition">The position the player is getting placed in</param>
        /// <param name="moves">The moves that the player has made</param>
        /// <returns></returns>
        public Player ProvidePlayerPersonalData(int chartPosition, int moves)
        {
            Console.WriteLine("Type in your name.");
            string userName = Console.ReadLine();

            Player player = new Player(moves, userName);

            return player;
        }

        /// <summary>
        /// Asks for the command and checks if the command provided 
        /// is longer than 0 characters.
        /// </summary>
        /// <returns>The command that is provided</returns>
        public string ProvidePlayerCommand()
        {
            Console.WriteLine("Command: ");
            string commandInput = Console.ReadLine();
            Console.WriteLine();
            commandInput = commandInput.ToLower().Trim();
            if (commandInput.Length > 1)
            {
                commandInput = commandInput.Substring(0, 1).ToUpper() + commandInput.Substring(1);
            }
            return commandInput;
        }

        /// <summary>
        /// Prints a specific message on the console designated to give instructions to player
        /// </summary>
        public void PrintUserMessage(string message)
        {
            Console.WriteLine(message);
        }
    }
}
