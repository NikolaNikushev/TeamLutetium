using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BalloonsPop5Game
{
    class ConsoleRenderer : IRendered
    {
        public void PrintField(PlayField field)
        {
            Console.Clear();
            PrintGameInstructions();
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

        public void PrintWinnerBoard(ScoreBoard scoreBoard)
        {
            if (scoreBoard.GetLength() == 0)
            {
                Console.WriteLine("The score board is empty!");
            }
            else
            {
                Console.WriteLine("---------TOP FIVE Players-----------");
                for (int winnerPosition = 0; winnerPosition < scoreBoard.GetLength(); ++winnerPosition)
                {
                    Player player = scoreBoard[winnerPosition];
                    Console.WriteLine("{2}.   {0} with {1} moves.", player.Name, player.Moves, winnerPosition + 1);
                }
                Console.WriteLine("----------------------------------");
            }

            Console.WriteLine();
        }

        public static string[,] AddToChart(string[,] chart, int chartPosition, int points)
        {
            Console.WriteLine("Type in your name.");
            string userName = Console.ReadLine();

            chart[chartPosition, 0] = points.ToString();
            chart[chartPosition, 1] = userName;

            return chart;
        }

        public void PrintGameInstructions()
        {
            Console.WriteLine("Welcome to “Balloons Pops” game. " +
            "Please try to pop the balloons. \nUse 'top' to view the top scoreboard,\n" +
            "'restart' to start a new game and 'exit' to quit the game. \n");
        }
    }
}
