using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BalloonsPop;
using Telerik.JustMock;
using System.Linq;
using Telerik.JustMock.Helpers;
using System.IO;

namespace TestBalloonPop5Game
{
    [TestClass]
    public class EngineUnitTest
    {
        [TestMethod]
        public void CreateEngineAndRunGame()
        {
            Engine engine = new Engine(5, 10);
            engine.RunGame();
            CurrentAction expected = CurrentAction.IsRunning;
            Assert.AreEqual(expected, engine.currentAction);
        }

        [TestMethod]
        public void CreateEngineAndExitGame()
        {
            Engine engine = new Engine(5, 10);
            engine.RunGame();
            Console.SetIn(new System.IO.StringReader("exit"));
            engine.ReadAction();
            CurrentAction expected = CurrentAction.IsNotRunning;

            Assert.AreEqual(expected, engine.currentAction);
        }

        [TestMethod]
        public void CreateEngineAndShowTopBoard()
        {
            Engine engine = new Engine(5, 10);
            engine.RunGame();
            Console.SetIn(new System.IO.StringReader("top"));
            engine.ReadAction();
            CurrentAction expected = CurrentAction.PrintingTopBoard;
            Assert.AreEqual(expected, engine.currentAction);
        }

        [TestMethod]
        public void CreateEngineAndRestartGame()
        {
            Engine engine = new Engine(5, 10);
            engine.RunGame();
            Console.SetIn(new System.IO.StringReader("restart"));
            engine.ReadAction();
            CurrentAction expected = CurrentAction.Restarting;

            Assert.AreEqual(expected, engine.currentAction);
        }

        [TestMethod]
        public void CreateEngineAndEnterValidCoordinates()
        {
            Engine engine = new Engine(5, 10);
            engine.RunGame();
            Console.SetIn(new System.IO.StringReader("4 4"));
            engine.ReadAction();
            CurrentAction expected = CurrentAction.CheckingCoordinates;
            Assert.AreEqual(expected, engine.currentAction);

            Console.SetIn(new System.IO.StringReader("4 8"));
            engine.ReadAction();
            Assert.AreEqual(expected, engine.currentAction);

            Console.SetIn(new System.IO.StringReader("4 1"));
            engine.ReadAction();
            Assert.AreEqual(expected, engine.currentAction);

            Console.SetIn(new System.IO.StringReader("4 0"));
            engine.ReadAction();
            Assert.AreEqual(expected, engine.currentAction);
        }

        [TestMethod]
        public void CreateEngineAndEnterInvalidCoordinates()
        {
            Engine engine = new Engine(5, 10);
            engine.RunGame();

            Console.SetIn(new System.IO.StringReader("44 9"));
            engine.ReadAction();
            CurrentAction expected = CurrentAction.IsWaitingForValidInput;
            Assert.AreEqual(expected, engine.currentAction);

            Console.SetIn(new System.IO.StringReader("aa bb"));
            engine.ReadAction();
            Assert.AreEqual(expected, engine.currentAction);

            Console.SetIn(new System.IO.StringReader("2100 02100"));
            engine.ReadAction();
            Assert.AreEqual(expected, engine.currentAction);

            Console.SetIn(new System.IO.StringReader("12 31321"));
            engine.ReadAction();
            Assert.AreEqual(expected, engine.currentAction);

            Console.SetIn(new System.IO.StringReader("noone"));
            engine.ReadAction();
            Assert.AreEqual(expected, engine.currentAction);

            Console.SetIn(new System.IO.StringReader("tests"));
            engine.ReadAction();
            Assert.AreEqual(expected, engine.currentAction);

            Console.SetIn(new System.IO.StringReader("a b"));
            engine.ReadAction();
            Assert.AreEqual(expected, engine.currentAction);
        }

        [TestMethod]
        public void CreateEngineAndFinishTheGame()
        {
            Engine engine = new Engine(5, 10);
            engine.RunGame();
            for (int i = 0; i < 11; i++)
            {
                Console.SetIn(new System.IO.StringReader("4 4"));
                engine.ReadAction();
            }
            CurrentAction expected = CurrentAction.IsWaitingForValidInput;
            Assert.AreEqual(expected, engine.currentAction);
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 11; j++)
                {
                    Console.SetIn(new System.IO.StringReader(i + " " + j));
                    engine.ReadAction();
                    if (engine.currentAction == CurrentAction.FinishedGame)
                    {
                        break;
                    }
                }
            }
            expected = CurrentAction.FinishedGame;
            Assert.AreEqual(expected, engine.currentAction);
        }
    }
}
