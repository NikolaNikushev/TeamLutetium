using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BalloonsPop;

namespace TestBalloonPop5Game
{
    [TestClass]
    public class PlayerUnitTest
    {
        [TestMethod]
        public void CreateAPlayer()
        {
            Player player = new Player();
            Assert.IsNotNull(player);
            Assert.AreEqual(0, player.Moves);
            Assert.IsNull(player.Name);
        }

        [TestMethod]
        public void CreateAPlayerAndIncreaseHisScore()
        {
            Player player = new Player();
            Assert.IsNotNull(player);
            Assert.AreEqual(0, player.Moves);
            Assert.IsNull(player.Name);

            player.UpdateMovesAmmount();
            Assert.AreEqual(1, player.Moves);

            player = new Player(3, "Gosho");
            Assert.AreEqual(3, player.Moves);

            player.UpdateMovesAmmount();
            Assert.AreEqual(4, player.Moves);
        }

        [TestMethod]
        public void CreateAPlayerWithName()
        {
            Player player = new Player(0, "bob");
            Assert.IsNotNull(player);
            Assert.AreEqual(0, player.Moves);
            Assert.AreEqual("bob", player.Name);
        }

        [TestMethod]
        public void CompareOnePlayerToThreeOther()
        {
            Player player1 = new Player(2, "steven");
            Player player2 = new Player(3, "bob");
            Player player3 = new Player(1, "mihail");
            Player player4 = new Player(2, "lauren");
           
            Assert.AreEqual(-1,  player1.CompareTo(player2));
            Assert.AreEqual(1, player1.CompareTo(player3));
            Assert.AreEqual(0, player1.CompareTo(player4));
        }
    }
}
