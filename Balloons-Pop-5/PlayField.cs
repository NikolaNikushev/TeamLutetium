using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BalloonsPop5Game
{
    public class PlayField:IChangeable
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
    }
}
