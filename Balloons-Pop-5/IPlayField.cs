using System;
using System.Linq;

namespace BalloonsPop
{
    public interface IPlayField
    {
        byte this[int row, int col]
        {
            get;
        }
        void RespondToOuterChange(byte row, byte column, byte newValue);
        void MakeChangesToField(int row, int column);
        int GetLength(byte dimension);
        bool ClearedLevel();
    }
}
