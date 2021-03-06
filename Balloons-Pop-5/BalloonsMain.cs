﻿using System;
using System.Collections.Generic;

namespace BalloonsPop
{
    class BalloonsMain
    {
        private const byte GAMEROWS = 5;
        private const byte GAMECOLS = 10;
        static void Main(string[] args)
        {
            Engine engine = new Engine(GAMEROWS, GAMECOLS);
            engine.RunGame();
            while (engine.currentAction != Engine.CurrentAction.IsNotRunning)
            {
                engine.ReadAction();
            }

        }
    }
}


