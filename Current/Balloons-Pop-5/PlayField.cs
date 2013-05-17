using System;
using System.Collections.Generic;
using System.Linq;

namespace BalloonsPop
{
    /// <summary>
    /// Class that represents the playfield of the game
    /// </summary>
    public class PlayField : IPlayField
    {
        private byte[,] field;
        public byte MaxBubbleNumber{get; private set;}

        /// <summary>
        /// Constructor for the class PlayField
        /// Creates a field as a matrix, having rows, columns and 
        /// the maximum value a ballon in the playfield could have.
        /// The matrix is created on via a random generator
        /// </summary>
        /// <param name="rowsNumber">Number of rows in field</param>
        /// <param name="colsNumber">Number of columns in field</param>
        /// <param name="maxBubbleNumber">Maximal value of bubbles in field</param>
        public PlayField(byte rowsNumber, byte colsNumber, byte maxBubbleNumber=4)
        {
            if (maxBubbleNumber == 0)
            {
                throw new ArgumentException("Cannot create a field with maxBubbleNumber"+ 
                    " less or equal to zero");
            }
            this.MaxBubbleNumber = maxBubbleNumber;
            this.field = GenerateRandomField(rowsNumber, colsNumber);
        }

        public byte this[int row, int col]
        {
            get
            {
                return this.field[row, col];
            }
        }

        public int GetLength(byte dimension)
        {
            if (dimension == 0 || dimension == 1)
            {
                return this.field.GetLength(dimension);
            }
            else
            {
                throw new ArgumentException("Argument dimension could be either 0 or 1!");
            }
        }

        /// <summary>
        /// Makes balloons on top to fall if there are
        /// no ballons beneath
        /// </summary>
        /// <returns>Returns true if there are no more ballons to fall and false if there are ballons left</returns>
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

        /// <summary>
        /// Removes all ballons neighbouring the balloon that the player has shot,
        /// plus the baloons that are neighbouring the neighbours and so on
        /// </summary>
        /// <param name="row">Number of shot row</param>
        /// <param name="column">Number of shot column</param>
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
            if (rows <= 0 || columns <= 0)
            {
                throw new ArgumentException("Cannot create a playfield with zero or negative rows or cols");
            }
            byte[,] randomField = new byte[rows, columns];
            Random randomNumberGenerator = new Random();
            for (byte row = 0; row < rows; row++)
            {
                for (byte column = 0; column < columns; column++)
                {
                    byte currentNumber = (byte)randomNumberGenerator.Next(1, this.MaxBubbleNumber+1);
                    randomField[row, column] = currentNumber;
                }
            }

            return randomField;
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
