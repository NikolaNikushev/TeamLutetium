using System;
using System.Linq;

namespace BalloonsPop5Game
{
    public interface IChangeable
    {
        void RespondToOuterChange(byte row, byte column, byte newValue);
    }
}
