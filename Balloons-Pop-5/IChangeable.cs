using System;
using System.Linq;

namespace BalloonsPop
{
    public interface IChangeable
    {
        void RespondToOuterChange(byte row, byte column, byte newValue);
    }
}
