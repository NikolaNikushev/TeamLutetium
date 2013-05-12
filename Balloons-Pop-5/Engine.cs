using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BalloonsPop5Game
{
    class Engine
    {
        private PlayField field;
        private ScoreBoard scoreBoard;
        private Player player;
        public Engine()
        {
            
        }

        public void InitialisePlayField(byte rowsNumber, byte colsNumber, byte playerCount )
        {
            field = new PlayField(rowsNumber,colsNumber);
            scoreBoard = new ScoreBoard();
            player = new Player(0);
        }

        public void RunGame()
        {

        }
    }
}
