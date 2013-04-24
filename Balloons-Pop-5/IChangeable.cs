using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BalloonsPop5Game
{
    public interface IChangeable
    {
        void RespondToOuterChange(byte row, byte column, byte newValue);
    }
}
