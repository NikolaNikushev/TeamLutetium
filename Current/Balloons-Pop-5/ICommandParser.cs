using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BalloonsPop
{
    public interface ICommandParser
    {
        Command ParseCommand(string commandInput, IPlayField field);
        bool CheckIfCommandIsCoordinate(string commandInput, IPlayField field);
        bool CheckPoppableBalloon(string commandInput, IPlayField field);
        int ParseCommandToRow(string inputCommand);
        int ParseCommandToCol(string inputCommand);
    }
}
