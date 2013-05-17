using System;
using System.Linq;

namespace BalloonsPop
{
    /// <summary>
    /// Interface determing methods that should be implemented by
    /// a class that makes communication between the game and user
    /// (that controls the user interface communication)
    /// </summary>
    public interface IUICommunicator
    {
        void RenderGameField(IPlayField field);
        void RenderWinnerBoard(ScoreBoard scoreBoard);
        Player ProvidePlayerPersonalData(int chartPosition, int moves);
        string ProvidePlayerCommand();
        void PrintUserMessage(string message);
    }
}
