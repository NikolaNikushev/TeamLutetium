using System;
using System.Linq;

namespace BalloonsPop
{
    /// <summary>
    /// Interface determing operations over a userCommand
    /// </summary>
    public interface ICommandParser
    {
        Command ParseCommand(string commandInput, IPlayField field);
        bool CheckIfCommandIsCoordinate(string commandInput, IPlayField field);
        bool CheckPoppableBalloon(string commandInput, IPlayField field);
        int ParseCommandToRow(string inputCommand);
        int ParseCommandToCol(string inputCommand);
    }
}
