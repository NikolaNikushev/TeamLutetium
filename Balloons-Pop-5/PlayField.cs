using System;
using System.Collections.Generic;
using System.Linq;

namespace BalloonsPop
{
    public class PlayField : IChangeable
    {
        private byte[,] field;

        public PlayField(byte rowsNumber, byte colsNumber)
        {
            this.field = GenerateRandomField(rowsNumber, colsNumber);
        }

        public byte[,] Field 
        {
            get
            {
                return this.field;
            } 
        }

        public byte this[int row, int col]
        {
            get
            {
                return this.field[row, col];
            }
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

        public void RespondToOuterChange(byte row, byte column, byte newValue)
        {
            this.field[row, column] = newValue;
        }

       public void MakeChangesToField(int row, int column)
       {
           if (field[row, column] != 0)
           {
               byte searchedTarget = field[row, column];
               PopNeighbouringBallons(field, row, column, searchedTarget);
           }
       }

       private byte[,] GenerateRandomField(byte rows, byte columns)
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

       public bool ClearedLevel()
       {
           bool isEmpty = true;
           Stack<byte> winners = new Stack<byte>();
           int fieldRows = field.GetLength(0);
           int fieldCols = field.GetLength(1);

           for (int col = 0; col < fieldCols; col++)
           {
               for (int row = 0; row < fieldRows; row++)
               {
                   if (field[row, col] != 0)
                   {
                       isEmpty = false;
                       winners.Push(field[row, col]);
                   }
               }
               for (int winnerPosition = fieldRows - 1; (winnerPosition >= 0); winnerPosition--)
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
           return isEmpty;
       }

       private static void PopNeighbouringBallons(byte[,] field, int row, int column, int searchedItem)
       {
           try
           {
               if (field[row, column] == searchedItem)
               {
                   field[row, column] = 0;
                   PopNeighbouringBallons(field, row - 1, column, searchedItem);
                   PopNeighbouringBallons(field, row + 1, column, searchedItem);
                   PopNeighbouringBallons(field, row, column + 1, searchedItem);
                   PopNeighbouringBallons(field, row, column - 1, searchedItem);
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
   }
}
