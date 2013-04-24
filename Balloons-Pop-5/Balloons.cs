using System;
using System.Collections.Generic;

namespace BalloonsPop5Game
{
    class Balloons
    {
        static byte[,] GenerateField(byte rows, byte columns)
        {
            PrintInstructions();
            byte[,] randomField = new byte[rows, columns];
            Random randomNumberGenerator = new Random();
            for (byte row = 0; row < rows; row++)
            {
                for (byte column = 0; column < columns; column++)
                {
                    byte currentNumber = (byte)randomNumberGenerator.Next(1, 5);
                    randomField[row, column] = currentNumber;
                }
            }
            return randomField;

        }

        private static void PrintInstructions()
        {
            Console.WriteLine("Welcome to “Balloons Pops” game. " +
           "Please try to pop the balloons. \nUse 'top' to view the top scoreboard,\n" +
              "'restart' to start a new game and 'exit' to quit the game. \n");
        }


        static void CheckPosition(byte[,] field, int row, int column, int searchedItem)
        {
            try
            {
                if (field[row, column] == searchedItem)
                    {
                        field[row, column] = 0;
                        CheckPosition(field, row - 1, column, searchedItem);
                        CheckPosition(field, row + 1, column, searchedItem);
                        CheckPosition(field, row, column + 1, searchedItem);
                        CheckPosition(field, row, column - 1, searchedItem);
                    }
                    else
                    {
                        return;
                    }
            }
            catch (IndexOutOfRangeException)
            {
                return;
            }
        }

        static bool Change(byte[,] fieldToModify, int row, int column)
        {
            if (fieldToModify[row, column] == 0)
            {
                return true;
            }
            byte searchedTarget = fieldToModify[row, column];
            CheckPosition(fieldToModify, row, column, searchedTarget);

            return false;
        }

        static bool FinishedLevel(byte[,] field)
        {
            bool isWinner = true;
            Stack<byte> winners = new Stack<byte>();
            int columnLength = field.GetLength(0);
            for (int j = 0; j < field.GetLength(1); j++)
            {
                for (int i = 0; i < columnLength; i++)
                {
                    if (field[i, j] != 0)
                    {
                        isWinner = false;
                        winners.Push(field[i, j]);
                    }
                }
                for (int k = columnLength - 1; (k >= 0); k--)
                {
                    try
                    {
                        field[k, j] = winners.Pop();
                    }
                    catch (Exception)
                    {
                        field[k, j] = 0;
                    }
                }
            }
            return isWinner;
        }

        static List<ScoreBoard> SortWinnerBoard(string[,] tableToSort)
        {
            List<ScoreBoard> scoreBoard = new List<ScoreBoard>();

            for (int i = 0; i < 5; ++i)
            {
                if (tableToSort[i, 0] == null)
                {
                    break;
                }
                scoreBoard.Add(new ScoreBoard(int.Parse(tableToSort[i, 0]), tableToSort[i, 1]));
            }
            scoreBoard.Sort();

            return scoreBoard;
        }

        static void PrintWinnerBoard(List<ScoreBoard> scoreBoard)
        {
            if (scoreBoard.Count == 0)
            {
                Console.WriteLine("The score board is empty!");
            }
            else
            {
                Console.WriteLine("---------TOP FIVE Players-----------");
                for (int i = 0; i < scoreBoard.Count; ++i)
                {
                    ScoreBoard slot = scoreBoard[i];
                    Console.WriteLine("{2}.   {0} with {1} moves.", slot.Name, slot.Value, i + 1);
                }
                Console.WriteLine("----------------------------------");
            }

            Console.WriteLine();
        }

        private static void PrintField(byte[,] field)
        {
            //Console.Clear();
            Console.Write("    ");
            for (byte column = 0; column < field.GetLongLength(1); column++)
            {
                Console.Write(column + " ");
            }

            Console.Write("\n   ");
            for (byte column = 0; column < field.GetLongLength(1) * 2 + 1; column++)
            {
                Console.Write("-");
            }

            Console.WriteLine();

            for (byte i = 0; i < field.GetLongLength(0); i++)
            {
                Console.Write(i + " | ");
                for (byte j = 0; j < field.GetLongLength(1); j++)
                {
                    if (field[i, j] == 0)
                    {
                        Console.Write("  ");
                        continue;
                    }
                    Console.Write(field[i, j] + " ");
                }
                Console.Write("| ");
                Console.WriteLine();
            }
            Console.Write("   ");
            for (byte column = 0; column < field.GetLongLength(1) * 2 + 1; column++)
            {
                Console.Write("-");
            }
            Console.WriteLine();
        }

        static void Main(string[] args)
        {
            string[,] topFiveWinnersChart = new string[5, 2];
            byte[,] field = GenerateField(5, 10);
            PrintField(field);

            string commandInput = null;
            int userMoves = 0;

            while (commandInput != "EXIT")
            {
                Console.WriteLine("Enter a row and column: ");
                commandInput = Console.ReadLine();
                Console.WriteLine();
                commandInput = commandInput.ToUpper().Trim();

                switch (commandInput)
                {
                    case "RESTART":
                        field = GenerateField(5, 10);
                        PrintField(field);
                        userMoves = 0;
                        break;

                    case "TOP":
                        PrintWinnerBoard(SortWinnerBoard(topFiveWinnersChart));
                        break;

                    case "EXIT":
                        Console.WriteLine("Good Bye! ");
                        System.Threading.Thread.Sleep(2000);
                        break;

                    default:
                        if ((commandInput.Length == 3) && (commandInput[0] >= '0' && commandInput[0] <= '9') &&
                            (commandInput[2] >= '0' && commandInput[2] <= '9') &&
                            (commandInput[1] == ' ' || commandInput[1] == '.' || commandInput[1] == ','))
                        {
                            int userInputRow = int.Parse(commandInput[0].ToString());
                            if (userInputRow > 4)
                            {
                                Console.WriteLine("Wrong input ! Try Again ! \n");
                                continue;
                            }

                            int userInputColumn = int.Parse(commandInput[2].ToString());
                            if (Change(field, userInputRow, userInputColumn))
                            {
                                Console.WriteLine("Illegal move: cannot pop missing ballon!\n");
                                continue;
                            }

                            userMoves++;
                            if (FinishedLevel(field))
                            {
                                Console.WriteLine("Gratz ! You completed the level in {0} moves.\n", userMoves);
                                if (topFiveWinnersChart.CheckIfSkilled(userMoves))
                                {
                                    PrintWinnerBoard(SortWinnerBoard(topFiveWinnersChart));
                                }
                                else
                                {
                                    Console.WriteLine("I am sorry you are not skillful enough for TopFive chart!");
                                    System.Threading.Thread.Sleep(3000);
                                }
                                field = GenerateField(5, 10);
                                userMoves = 0;
                            }
                            PrintField(field);
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Wrong input ! Try Again !\n");
                            break;
                        }
                }
            }
        }
    }
}


