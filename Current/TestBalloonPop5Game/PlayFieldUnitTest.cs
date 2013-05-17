using System;
using System.Text;
using System.Collections.Generic;
using BalloonsPop;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestBalloonPop5Game
{
    [TestClass]
    public class PlayFieldUnitTest
    {
        [TestMethod]
        [ExpectedException (typeof(ArgumentException))]
        public void TestPlayFieldConstructorNegativeMaxBubbleNumber()
        {
            PlayField field = new PlayField(3, 4, 0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestPlayFieldConstructorZeroRow()
        {
            PlayField field = new PlayField(0, 3, 2);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestPlayFieldConstructorZeroCol()
        {
            PlayField field = new PlayField(1, 0, 2);
        }

        [TestMethod]
        public void TestPlayFieldConstructorMaxBubbleNumberSeven()
        {
            PlayField field = new PlayField(3, 4, 7);
            bool allBubblesValid = true;
            for (int i = 0; i < field.GetLength(0); i++)
            {
                for (int j = 0; j < field.GetLength(1); j++)
                {
                    if (field[i, j] <= 0 || field[i,j] > 7)
                    {
                        allBubblesValid = false;
                        break;
                    }
                }
            }
            Assert.AreEqual(3, field.GetLength(0));
            Assert.AreEqual(4, field.GetLength(1));
            Assert.AreEqual(true, allBubblesValid);
        }


        [TestMethod]
        public void TestPlayFieldConstructorMaxBubbleNumberOne()
        {
            PlayField field = new PlayField(3, 4, 1);
            bool allBubblesValid = true;
            for (int i = 0; i < field.GetLength(0); i++)
            {
                for (int j = 0; j < field.GetLength(1); j++)
                {
                    if (field[i, j]!= 1)
                    {
                        allBubblesValid = false;
                        break;
                    }
                }
            }
            Assert.AreEqual(3, field.GetLength(0));
            Assert.AreEqual(4, field.GetLength(1));
            Assert.AreEqual(true, allBubblesValid);
        }

        [TestMethod]
        public void TestGetLengthArgumentZeroFieldFourRowsThreeCols()
        {
            PlayField field=new PlayField(4,3);
            int rows=field.GetLength(0);
            Assert.AreEqual(4,rows);
        }

        [TestMethod]
        public void TestGetLengthArgumentOneFieldFourRowsThreeCols()
        {
            PlayField field = new PlayField(4,3);
            int cols = field.GetLength(1);
            Assert.AreEqual(3, cols);
        }

        [TestMethod]
        [ExpectedException (typeof(ArgumentException))]
        public void TestGetLengthArgumentTwoFieldFourRowsThreeCols()
        {
            PlayField field = new PlayField(4, 3);
            int cols = field.GetLength(2);
            Assert.AreEqual(3, cols);
        }
    }
}
