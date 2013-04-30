using System;
using System.Collections.Generic;
using System.Linq;

namespace BalloonsPop5Game
{
    public class PlayField : IChangeable
    {
        private byte[,] field;

        public PlayField(byte rowsNumber, byte colsNumber)
        {
            this.field = GenerateField(rowsNumber, colsNumber);
        }

        public byte[,] GetFieldContent()
        {
            return this.field;
        }

        public int GetLength(byte dimension)
        {
            if (dimension==0 || dimension==1)
            {
                return this.field.GetLength(dimension);
            }
            else
            {
                throw new ArgumentException("Argument dimension could be either 0 or 1!");
            }
        }

        private byte[,] GenerateField(byte rows, byte columns)
        {
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

        public void RespondToOuterChange(byte row, byte column, byte newValue)
        {
            this.field[row, column] = newValue;
        }

        private static void PrintInstructions()
        {
            Console.WriteLine("Welcome to “Balloons Pops” game. " +
           "Please try to pop the balloons. \nUse 'top' to view the top scoreboard,\n" +
              "'restart' to start a new game and 'exit' to quit the game. \n");
        }

       public bool FinishedLevel()
        {
            bool isWinner = true;
            Stack<byte> winners = new Stack<byte>();
            int columnLength = field.GetLength(0);
            for (int col = 0; col < field.GetLength(1); col++)
            {
                for (int row = 0; row < columnLength; row++)
                {
                    if (field[row, col] != 0)
                    {
                        isWinner = false;
                        winners.Push(field[row, col]);
                    }
                }
                for (int winnerPosition = columnLength - 1; (winnerPosition >= 0); winnerPosition--)
                {
                    try
                    {
                        field[winnerPosition, col] = winners.Pop();
                    }
                    catch (Exception)
                    {
                        field[winnerPosition, col] = 0;
                    }
                }
            }
            return isWinner;
        }

       public static void CheckPosition(byte[,] field, int row, int column, int searchedItem)
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

      public bool MakeChangesToField(int row, int column)
       {
           bool madeChanges = false;
           if (field[row, column] != 0)
           {
               madeChanges = true;
               byte searchedTarget = field[row, column];
               CheckPosition(field, row, column, searchedTarget);
           }
           return madeChanges;
       }

        public void PrintField()
        {
            Console.Clear();
            PrintInstructions();
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

            for (byte row = 0; row < field.GetLongLength(0); row++)
            {
                Console.Write(row + " | ");
                for (byte col = 0; col < field.GetLongLength(1); col++)
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
            for (byte column = 0; column < field.GetLongLength(1) * 2 + 1; column++)
            {
                Console.Write("-");
            }
            Console.WriteLine();
        }
    }
}
