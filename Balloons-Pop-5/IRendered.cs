using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BalloonsPop
{
  public  interface IRendered
    {
         void PrintField(PlayField field);
         void PrintWinnerBoard(ScoreBoard scoreBoard);
    }
}
