using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BalloonsPop
{
    public enum CurrentAction
    {
        IsRunning,
        IsWaitingForValidInput,
        IsNotRunning,
        PrintingTopBoard,
        Restarting,
        CheckingCoordinates,
        FinishedGame
    }
}
