using System;
using System.Linq;

namespace BalloonsPop
{
    public interface IUICommunicator
    {
        void RenderGameField(IPlayField field);
        void RenderWinnerBoard(ScoreBoard scoreBoard);
        Player ProvidePlayerPersonalData(int chartPosition, int moves);
        string ProvidePlayerCommand();
        void PrintUserMessage(string message);
    }
}
