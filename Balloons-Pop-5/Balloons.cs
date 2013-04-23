using System;
using System.Collections.Generic;

namespace BalloonsPop5Game
{
    class Balloons
    {
        static byte[,] GenerateLevel(byte rows, byte columns)
        {
            PrintInstructions();
            byte[,] randomFields = new byte[rows, columns];
            Random randomNumber = new Random();
            for (byte row = 0; row < rows; row++)
            {
                for (byte column = 0; column < columns; column++)
                {
                    byte currentNumber = (byte)randomNumber.Next(1, 5);
                    randomFields[row, column] = currentNumber;
                }
            }
            return randomFields;
        }

        private static void PrintInstructions()
        {
            Console.WriteLine("Welcome to “Balloons Pops” game. " +
           "Please try to pop the balloons. \nUse 'top' to view the top scoreboard,\n" +
              "'restart' to start a new game and 'exit' to quit the game. \n");
        }

        static void checkLeft(byte[,] level, int row, int column, int searchedItem)
        {
            int newRow = row;
            int newColumn = column - 1;
            try
            {
                if (level[newRow, newColumn] == searchedItem)
                {
                    level[newRow, newColumn] = 0; 
                    checkLeft(level, newRow, newColumn, searchedItem);
                }
                else
                {
                    return;
                }
            }
            catch (IndexOutOfRangeException)
            {
                Console.WriteLine("Index was out of range!");
                //return;
            }
        }

        static void checkRight(byte[,] level, int row, int column, int searchedItem)
        {
            int newRow = row;
            int newColumn = column + 1;
            try
            {
                if (level[newRow, newColumn] == searchedItem)
                {
                    level[newRow, newColumn] = 0;
                    checkRight(level, newRow, newColumn, searchedItem);
                }
                else
                {
                    return;
                }
            }
            catch (IndexOutOfRangeException)
            {
                Console.WriteLine("Index was out of range!\n");
                //return;
            }
        }

        static void checkUp(byte[,] level, int row, int column, int searchedItem)
        {
            int newRow = row + 1;
            int newColumn = column;
            try
            {
                if (level[newRow, newColumn] == searchedItem)
                {
                    level[newRow, newColumn] = 0;
                    checkUp(level, newRow, newColumn, searchedItem);
                }
                else
                {
                    return;
                }
            }
            catch (IndexOutOfRangeException)
            {
                Console.WriteLine("Index was out of range!\n");
                //return;
            }
        }

        static void checkDown(byte[,] level, int row, int column, int searchedItem)
        {
            int newRow = row - 1;
            int newColumn = column;
            try
            {
                if (level[newRow, newColumn] == searchedItem)
                {
                    level[newRow, newColumn] = 0;
                    checkDown(level, newRow, newColumn, searchedItem);
                }
                else
                {
                    return;
                }
            }
            catch (IndexOutOfRangeException)
            {
                Console.WriteLine("Index was out of range!");
                //return;
            }

        }

        static bool change(byte[,] levelToModify, int rowAtm, int columnAtm)
        {
            if (levelToModify[rowAtm, columnAtm] == 0)
            {
                return true;
            }
            byte searchedTarget = levelToModify[rowAtm, columnAtm];
            levelToModify[rowAtm, columnAtm] = 0;

            checkLeft(levelToModify, rowAtm, columnAtm, searchedTarget);
            checkRight(levelToModify, rowAtm, columnAtm, searchedTarget);
            checkUp(levelToModify, rowAtm, columnAtm, searchedTarget);
            checkDown(levelToModify, rowAtm, columnAtm, searchedTarget);

            return false;
        }

        static bool finishedLevel(byte[,] level)
        {
            bool isWinner = true;
            Stack<byte> winners = new Stack<byte>();
            int columnLenght = level.GetLength(0);
            for (int j = 0; j < level.GetLength(1); j++)
            {
                for (int i = 0; i < columnLenght; i++)
                {
                    if (level[i, j] != 0)
                    {
                        isWinner = false;
                        winners.Push(level[i, j]);
                    }
                }
                for (int k = columnLenght - 1; (k >= 0); k--)
                {
                    try
                    {
                        level[k, j] = winners.Pop();
                    }
                    catch (Exception)
                    {
                        level[k, j] = 0;
                    }
                }
            }
            return isWinner;
        }

        static void sortAndPrintWinnerBoard(string[,] tableToSort)
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

        private static void PrintLevel(byte[,] level)
        {
            //Console.Clear();
            Console.Write("    ");
            for (byte column = 0; column < level.GetLongLength(1); column++)
            {
                Console.Write(column + " ");
            }

            Console.Write("\n   ");
            for (byte column = 0; column < level.GetLongLength(1) * 2 + 1; column++)
            {
                Console.Write("-");
            }

            Console.WriteLine();

            for (byte i = 0; i < level.GetLongLength(0); i++)
            {
                Console.Write(i + " | ");
                for (byte j = 0; j < level.GetLongLength(1); j++)
                {
                    if (level[i, j] == 0)
                    {
                        Console.Write("  ");
                        continue;
                    }
                    Console.Write(level[i, j] + " ");
                }
                Console.Write("| ");
                Console.WriteLine();
            }
            Console.Write("   ");
            for (byte column = 0; column < level.GetLongLength(1) * 2 + 1; column++)
            {
                Console.Write("-");
            }
            Console.WriteLine();
        }

        static void Main(string[] args)
        {
            string[,] topFive = new string[5, 2];
            byte[,] level = GenerateLevel(5, 10);
            PrintLevel(level);

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
                        level = GenerateLevel(5, 10);
                        PrintLevel(level);
                        userMoves = 0;
                        break;

                    case "TOP":
                        sortAndPrintWinnerBoard(topFive);
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
                            int userRow = int.Parse(commandInput[0].ToString());
                            if (userRow > 4)
                            {
                                Console.WriteLine("Wrong input ! Try Again ! \n");
                                continue;
                            }

                            int userColumn = int.Parse(commandInput[2].ToString());
                            if (change(level, userRow, userColumn))
                            {
                                Console.WriteLine("Illegal move: cannot pop missing ballon!\n");
                                continue;
                            }

                            userMoves++;
                            if (finishedLevel(level))
                            {
                                Console.WriteLine("Gratz ! You completed the level in {0} moves.\n", userMoves);
                                if (topFive.signIfSkilled(userMoves))
                                {
                                    sortAndPrintWinnerBoard(topFive);
                                }
                                else
                                {
                                    Console.WriteLine("I am sorry you are not skillful enough for TopFive chart!");
                                    System.Threading.Thread.Sleep(3000);
                                }
                                level = GenerateLevel(5, 10);
                                userMoves = 0;
                            }
                            PrintLevel(level);
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


