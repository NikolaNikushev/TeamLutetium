using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Telerik.JustMock;
using System.Text;
using System.IO;
using BalloonsPop;

namespace TestBalloonPop5Game
{
    [TestClass]
    public class ConsoleCommunicatorUnitTest
    {
        [TestMethod]
        public void RenderGameField()
        {
            Engine engine = new Engine(5, 10);
            engine.RunGame();
            Console.SetIn(new System.IO.StringReader("top"));
            engine.ReadAction();
            CurrentAction expected = CurrentAction.PrintingTopBoard;

            Assert.AreEqual(expected, engine.currentAction);

            Mock.Assert(() => engine.Communicator.RenderGameField(engine.Field), Occurs.AtLeastOnce());
           
        }
    }
}