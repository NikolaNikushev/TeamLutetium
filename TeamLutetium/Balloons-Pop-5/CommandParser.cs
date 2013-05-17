using System;
using System.Linq;

namespace BalloonsPop
{
    /// <summary>
    /// Class that makes operations over a user command
    /// </summary>
    public class CommandParser:ICommandParser
    {
        /// <summary>
        /// Method used to parse command from string to Command Type
        /// </summary>
        /// <param name="commandInput">String containing the command from user. 
        /// If it is not a coordinate, the first letter is capital, the rest are lower case.</param>
        /// <param name="field">Parameter of type PlayField, representing the playfield of the game</param>
        /// <returns>Returns the parsed command- a Command Type parameter.</returns>
        public Command ParseCommand(string commandInput, IPlayField field)
        {
            bool isValidCoordinate = CheckIfCommandIsCoordinate(commandInput, field);
            if (isValidCoordinate && CheckPoppableBalloon(commandInput, field))
            {
                return Command.CoordinateForParsing;
            }

            else if (CheckIfValidCommand(commandInput))
            {
                return (Command)Enum.Parse(typeof(Command), commandInput);
            }

            else
            {
                return Command.Invalid;
            }
        }

        /// <summary>
        /// Method that checks if a given command represents a valid coordinate in the game field
        /// </summary>
        /// <param name="commandInput">String containing the command from user. 
        /// If it is not a coordinate, the first letter is capital, the rest are lower case.</param>
        /// <param name="field">Parameter of type PlayField, representing the playfield of the game</param>
        /// <returns>Returns true if command is a valid coordinate and false, if it is not</returns>
        public bool CheckIfCommandIsCoordinate(string commandInput, IPlayField field)
        {
            string[] commandWords = commandInput.Split(new Char[]{' ', ',', '_', '.'}, StringSplitOptions.RemoveEmptyEntries);
            if (commandWords.Length == 2)
            {
                byte row = 0;
                byte col = 0;
                bool areCoordinates = byte.TryParse(commandWords[0], out row) && byte.TryParse(commandWords[1], out col);
                if (areCoordinates)
                {
                    if (0 <= row && row < field.GetLength(0) && 0 <= col && col < field.GetLength(1))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// Method that checks if commandInput given by the user represents a balloon in the 
        /// field which is poppable(different than zero)
        /// </summary>
        /// <param name="commandInput">String containing the command from user. 
        /// If it is not a coordinate, the first letter is capital, the rest are lower case.</param>
        /// <param name="field">Parameter of type PlayField, representing the playfield of the game</param>
        /// <returns>Returns true if the balloon is poppable(different than zero and false if not)</returns>
        public bool CheckPoppableBalloon(string commandInput, IPlayField field)
        {
            int row = ParseCommandToRow(commandInput);
            int col = ParseCommandToCol(commandInput);
            if (field[row, col] == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Parses a the inputCommandString to extend the value of row
        /// </summary>
        /// <param name="commandInput">String containing the command from user. 
        /// If it is not a coordinate, the first letter is capital, the rest are lower case.</param>
        /// <returns>Integer representing the value of coordinate row</returns>
        public int ParseCommandToRow(string inputCommandString)
        {
            string[] coordinates = inputCommandString.Split(new Char[] { ' ', ',', '_', '.' }, StringSplitOptions.RemoveEmptyEntries);
            int row = 0;
            int.TryParse(coordinates[0], out row);

            return row;
        }

        /// <summary>
        /// Parses a the inputCommandString to extend the value of column
        /// </summary>
        /// <param name="commandInput">String containing the command from user. 
        /// If it is not a coordinate, the first letter is capital, the rest are lower case.</param>
        /// <returns>Integer representing the value of coordinate column</returns>
        public int ParseCommandToCol(string inputCommandString)
        {
            string[] coordinates = inputCommandString.Split(new Char[] { ' ', ',', '_', '.' }, StringSplitOptions.RemoveEmptyEntries);
            int col = 0;
            int.TryParse(coordinates[1], out col);

            return col;
        }

        private bool CheckIfValidCommand(string commandInput)
        {
            foreach (string command in Enum.GetNames(typeof(Command)))
            {
                if (command == commandInput)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
