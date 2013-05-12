using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BalloonsPop5Game
{
  public  interface IRendered
    {
         void PrintGameInstructions();
         void PrintField(PlayField field);
         void PrintWinnerBoard(ScoreBoard scoreBoard);
    }
}
