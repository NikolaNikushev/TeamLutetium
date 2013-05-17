using System;
using System.Linq;

namespace BalloonsPop
{
    public class CommandParser:ICommandParser
    {
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

        public bool CheckIfCommandIsCoordinate(string commandInput, IPlayField field)
        {
            string[] commandWords = commandInput.Split(' ');
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
                    else return false;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }

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

        public int ParseCommandToRow(string inputCommandString)
        {
            string[] coordinates = inputCommandString.Split(' ');
            int row = 0;
            int.TryParse(coordinates[0], out row);

            return row;
        }

        public int ParseCommandToCol(string inputCommandString)
        {
            string[] coordinates = inputCommandString.Split(' ');
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
