using System;

namespace BalloonsPop5Game
{
    public class ScoreBoard : IComparable<ScoreBoard>
    {
        public int Value {get; private set;}
        public string Name {get;private set;}

        public ScoreBoard(int value, string name)
        {
            this.Value = value;
            this.Name = name;
        }

        public int CompareTo(ScoreBoard other)
        {
            if (this.Value>other.Value)
            {
                return 1;
            }
            else if (this.Value==other.Value)
            {
                return 0;
            }
            else if(this.Value<other.Value)
            {
                return -1;
            }
            else
            {
                throw new ArgumentException(
                    "The properties Value of different instances could be either greater than each other or equal.");
            }
        }
    }
}