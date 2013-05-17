using System;
using System.Linq;

namespace BalloonsPop
{
    /// <summary>
    /// Enumeration that gives option for the 
    /// current state of the engine
    /// </summary>
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
