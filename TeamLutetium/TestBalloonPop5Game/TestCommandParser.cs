using System;
using BalloonsPop;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestBalloonPop5Game
{
    [TestClass]
    public class TestCommandParser
    {
        [TestMethod]
        public void TestParseCommandExit()
        {
            PlayField field = new PlayField(3, 4);
            CommandParser parser = new CommandParser();
            Command command= parser.ParseCommand("Exit", field);
            Assert.AreEqual(Command.Exit, command);
        }

        [TestMethod]
        public void TestParseCommandExitFirstLetterLowerCase()
        {
            PlayField field = new PlayField(3, 4);
            CommandParser parser = new CommandParser();
            Command command = parser.ParseCommand("exit", field);
            Assert.AreEqual(Command.Invalid, command);
        }

        [TestMethod]
        public void TestParseCommandRestart()
        {
            PlayField field = new PlayField(3, 4);
            CommandParser parser = new CommandParser();
            Command command = parser.ParseCommand("Restart", field);
            Assert.AreEqual(Command.Restart, command);
        }
        [TestMethod]
        public void TestParseCommandTop()
        {
            PlayField field = new PlayField(3, 4);
            CommandParser parser = new CommandParser();
            Command command = parser.ParseCommand("Top", field);
            Assert.AreEqual(Command.Top, command);
        }
        [TestMethod]
        public void TestParseCommandCoordinateForParsing()
        {
            PlayField field = new PlayField(3, 4);
            CommandParser parser = new CommandParser();
            Command command = parser.ParseCommand("2 3", field);
            Assert.AreEqual(Command.CoordinateForParsing, command);
        }
        [TestMethod]
        public void TestCheckIfCommandIsCoordinateExit()
        {
            PlayField field = new PlayField(3, 4);
            CommandParser parser = new CommandParser();
            bool isCoordinate = parser.CheckIfCommandIsCoordinate("Exit", field);
            Assert.AreEqual(false, isCoordinate);
        }

        [TestMethod]
        public void TestCheckIfCommandIsCoordinateIsCoordinateThreeOne()
        {
            PlayField field = new PlayField(3, 4);
            CommandParser parser = new CommandParser();
            bool isCoordinate = parser.CheckIfCommandIsCoordinate("3 1", field);
            Assert.AreEqual(false, isCoordinate);
        }

        [TestMethod]
        public void TestCheckIfCommandIsCoordinateIsCoordinateSevenFiveComma()
        {
            PlayField field = new PlayField(3, 4);
            CommandParser parser = new CommandParser();
            bool isCoordinate = parser.CheckIfCommandIsCoordinate("7, 5", field);
            Assert.AreEqual(false, isCoordinate);
        }

        [TestMethod]
        public void TestCheckIfCommandIsCoordinateIsCoordinateSevenFive()
        {
            PlayField field = new PlayField(3, 4);
            CommandParser parser = new CommandParser();
            bool isCoordinate = parser.CheckIfCommandIsCoordinate("7, ,     5", field);
            Assert.AreEqual(false, isCoordinate);
        }

        [TestMethod]
        public void TestCheckIfCommandIsCoordinateNegative()
        {
            PlayField field = new PlayField(3, 4);
            CommandParser parser = new CommandParser();
            bool isCoordinate = parser.CheckIfCommandIsCoordinate("-4, ,     -5", field);
            Assert.AreEqual(false, isCoordinate);
        }

        [TestMethod]
        public void TestCheckIfCommandIsCoordinateTwoOne()
        {
            PlayField field = new PlayField(3, 4);
            CommandParser parser = new CommandParser();
            bool isCoordinate = parser.CheckIfCommandIsCoordinate("2, ,     1", field);
            Assert.AreEqual(true, isCoordinate);
        }

        [TestMethod]
        public void TestCheckParseCommandToRowThree()
        {
            CommandParser parser = new CommandParser();
            int row= parser.ParseCommandToRow("3 5");
            Assert.AreEqual(3, row);
        }

        [TestMethod]
        public void TestCheckParseCommandToRowMinusThree()
        {
            CommandParser parser = new CommandParser();
            int row= parser.ParseCommandToRow("-3          , 5");
            Assert.AreEqual(-3, row);
        }

        [TestMethod]
        public void TestCheckParseCommandToColFive()
        {
            CommandParser parser = new CommandParser();
            int col= parser.ParseCommandToCol("3, 5");
            Assert.AreEqual(5, col);
        }

        [TestMethod]
        public void TestCheckParseCommandToColOneMillion()
        {
            CommandParser parser = new CommandParser();
            int col= parser.ParseCommandToCol("3, 1000000");
            Assert.AreEqual(1000000, col);
        }

        [TestMethod]
        public void TestCheckIfPoppableBallonsPoppable()
        {
            PlayField field = new PlayField(3, 3);
            CommandParser parser=new CommandParser();
            bool isPoppable = parser.CheckPoppableBalloon("2 1", field);
            Assert.AreEqual(true, isPoppable);
        }

        [TestMethod]
        public void TestCheckIfPoppableBallonsNotPoppable()
        {
            PlayField field = new PlayField(3, 3, 1);
            field.MakeChangesToField(1, 1);
            CommandParser parser = new CommandParser();
            int countPopped=0;
            for (int i = 0; i < field.GetLength(0); i++)
            {
                for (int j = 0; j < field.GetLength(1); j++)
                {
                    if (!parser.CheckPoppableBalloon( i.ToString()+" "+j.ToString(), field))
                    {
                        countPopped++;
                    }
                }
            }
            
            Assert.AreEqual(9, countPopped);
        }
    }
}
