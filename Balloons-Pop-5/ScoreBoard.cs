using System;

namespace BalloonsPop5Game
{
    public class ScoreBoard : IComparable<ScoreBoard>
    {
        public int Value;
        public string Name;

        public ScoreBoard(int value, string name)
        {
            this.Value = value;
            this.Name = name;
        }

        public int CompareTo(ScoreBoard other)
        {
            return Value.CompareTo(other.Value);
        }
    }
}