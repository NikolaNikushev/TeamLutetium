using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BalloonsPop;

namespace TestBalloonPop5Game
{
    [TestClass]
    public class ScoreboardUnitTest
    {
        [TestMethod]
        public void CreateAScoreBoard()
        {
            ScoreBoard scoreBoard = new ScoreBoard();
            Assert.IsNotNull(scoreBoard);
        }

        [TestMethod]
        public void AddPlayerToScoreBoard()
        {
            Engine engine = new Engine(5, 10);
            ScoreBoard scoreBoard = new ScoreBoard();
            Assert.IsNotNull(scoreBoard);

            Player misho = new Player(0, "misho");
            Assert.IsNotNull(misho);

            Console.SetIn(new System.IO.StringReader("misho"));
            scoreBoard.AddSkillfulPlayerToBoard(misho, engine.Communicator);
            Assert.AreEqual("misho", misho.Name);
            Assert.AreEqual(1, scoreBoard.GetLength());
        }

        [TestMethod]
        public void AddPlayersToScoreBoard()
        {
            Engine engine = new Engine(5, 10);
            ScoreBoard scoreBoard = new ScoreBoard();
            Assert.IsNotNull(scoreBoard);

            Player misho = new Player(0, "misho");
            Assert.IsNotNull(misho);

            Player stefan = new Player();
            Assert.IsNotNull(stefan);

            Player carl = new Player(0, "Carl");
            Assert.IsNotNull(carl);

            Player pepo = new Player(0, "Pepito");
            Assert.IsNotNull(pepo);

            Console.SetIn(new System.IO.StringReader("misho"));
            scoreBoard.AddSkillfulPlayerToBoard(misho, engine.Communicator);
            Assert.AreEqual("misho", misho.Name);

            Console.SetIn(new System.IO.StringReader(""));
            scoreBoard.AddSkillfulPlayerToBoard(stefan, engine.Communicator);
            Assert.AreEqual(null, stefan.Name);

            Console.SetIn(new System.IO.StringReader("carl"));
            scoreBoard.AddSkillfulPlayerToBoard(carl, engine.Communicator);

            Console.SetIn(new System.IO.StringReader("pepo"));
            scoreBoard.AddSkillfulPlayerToBoard(pepo, engine.Communicator);
            Assert.AreEqual(4, scoreBoard.GetLength());
        }

        [TestMethod]
        public void AddPlayersToScoreBoardAndSort()
        {
            Engine engine = new Engine(5, 10);
            ScoreBoard scoreBoard = new ScoreBoard();
            Assert.IsNotNull(scoreBoard);

            Player misho = new Player(20, "misho");
            Assert.IsNotNull(misho);

            Player noNamePlayer = new Player();
            Assert.IsNotNull(noNamePlayer);

            Player carl = new Player(1, "Carl");
            Assert.IsNotNull(carl);

            Player pepo = new Player(5, "Pepito");
            Assert.IsNotNull(pepo);

            Console.SetIn(new System.IO.StringReader("misho"));
            scoreBoard.AddSkillfulPlayerToBoard(misho, engine.Communicator);
            Assert.AreEqual("misho", misho.Name);

            Console.SetIn(new System.IO.StringReader(""));
            scoreBoard.AddSkillfulPlayerToBoard(noNamePlayer, engine.Communicator);

            Console.SetIn(new System.IO.StringReader("Carl"));
            scoreBoard.AddSkillfulPlayerToBoard(carl, engine.Communicator);

            Console.SetIn(new System.IO.StringReader("Pepito"));
            scoreBoard.AddSkillfulPlayerToBoard(pepo, engine.Communicator);

            Assert.AreEqual(4, scoreBoard.GetLength());
            scoreBoard.SortWinnerBoard();

            Assert.AreEqual(noNamePlayer.Name, scoreBoard[0].Name);
            Assert.AreEqual(carl.Name, scoreBoard[1].Name);
            Assert.AreEqual(pepo.Name, scoreBoard[2].Name);
            Assert.AreEqual(misho.Name, scoreBoard[3].Name);
        }

        [TestMethod]
        public void AddSkillfulPlayersToScoreBoardAndSort()
        {
            Engine engine = new Engine(5, 10);
            ScoreBoard scoreBoard = new ScoreBoard();
            Assert.IsNotNull(scoreBoard);

            Player noNamePlayer = new Player(0, "");
            Assert.IsNotNull(noNamePlayer);

            Player carl = new Player(0, "carl");
            Assert.IsNotNull(carl);
            for (int i = 0; i < 3; i++)
            {
                carl.UpdateMovesAmmount();
            }

            Player pepo = new Player(0, "pepo");
            Assert.IsNotNull(pepo);
            for (int i = 0; i < 15; i++)
            {
                pepo.UpdateMovesAmmount();
            }

            Player stefi = new Player(0, "stefi");
            Assert.IsNotNull(stefi);
            for (int i = 0; i < 20; i++)
            {
                stefi.UpdateMovesAmmount();
            }

            Player misho = new Player(0, "misho");
            Assert.IsNotNull(misho);
            for (int i = 0; i < 30; i++)
            {
                misho.UpdateMovesAmmount();
            }

            Player bob = new Player(0, "bob");
            Assert.IsNotNull(bob);
            for (int i = 0; i < 35; i++)
            {
                bob.UpdateMovesAmmount();
            }

            Console.SetIn(new System.IO.StringReader("misho"));
            scoreBoard.AddSkillfulPlayerToBoard(misho, engine.Communicator);
            Assert.AreEqual("misho", misho.Name);
            Console.SetIn(new System.IO.StringReader(""));
            scoreBoard.AddSkillfulPlayerToBoard(noNamePlayer, engine.Communicator);

            Console.SetIn(new System.IO.StringReader("carl"));
            scoreBoard.AddSkillfulPlayerToBoard(carl, engine.Communicator);

            Console.SetIn(new System.IO.StringReader("pepo"));
            scoreBoard.AddSkillfulPlayerToBoard(pepo, engine.Communicator);

            Console.SetIn(new System.IO.StringReader("stefi"));
            scoreBoard.AddSkillfulPlayerToBoard(stefi, engine.Communicator);

            Console.SetIn(new System.IO.StringReader("bob"));
            scoreBoard.AddSkillfulPlayerToBoard(bob, engine.Communicator);

            Assert.AreEqual(6, scoreBoard.GetLength());
            scoreBoard.SortWinnerBoard();

            Assert.AreEqual(null, scoreBoard[0].Name);
            Assert.AreEqual(carl.Name, scoreBoard[1].Name);
            Assert.AreEqual(pepo.Name, scoreBoard[2].Name);
            Assert.AreEqual(stefi.Name, scoreBoard[3].Name);
            Assert.AreEqual(misho.Name, scoreBoard[4].Name);
            Assert.AreEqual(bob.Name, scoreBoard[5].Name);
        }

        [TestMethod]
        public void ReAddSkillfulPlayerAndSort()
        {
            Engine engine = new Engine(5, 10);
            ScoreBoard scoreBoard = new ScoreBoard();
            Assert.IsNotNull(scoreBoard);

            Player noNamePlayer = new Player(0, "");
            Assert.IsNotNull(noNamePlayer);

            Player carl = new Player(0, "carl");
            Assert.IsNotNull(carl);
            for (int i = 0; i < 3; i++)
            {
                carl.UpdateMovesAmmount();
            }

            Player pepo = new Player(0, "pepo");
            Assert.IsNotNull(pepo);
            for (int i = 0; i < 15; i++)
            {
                pepo.UpdateMovesAmmount();
            }

            Player stefi = new Player(0, "stefi");
            Assert.IsNotNull(stefi);
            for (int i = 0; i < 20; i++)
            {
                stefi.UpdateMovesAmmount();
            }

            Player misho = new Player(0, "misho");
            Assert.IsNotNull(misho);
            for (int i = 0; i < 30; i++)
            {
                misho.UpdateMovesAmmount();
            }

            Player bob = new Player(0, "bob");
            Assert.IsNotNull(bob);
            for (int i = 0; i < 35; i++)
            {
                bob.UpdateMovesAmmount();
            }

            Console.SetIn(new System.IO.StringReader("misho"));
            scoreBoard.AddSkillfulPlayerToBoard(misho, engine.Communicator);
            Assert.AreEqual("misho", misho.Name);
            Console.SetIn(new System.IO.StringReader(""));
            scoreBoard.AddSkillfulPlayerToBoard(noNamePlayer, engine.Communicator);

            Console.SetIn(new System.IO.StringReader("carl"));
            scoreBoard.AddSkillfulPlayerToBoard(carl, engine.Communicator);

            Console.SetIn(new System.IO.StringReader("pepo"));
            scoreBoard.AddSkillfulPlayerToBoard(pepo, engine.Communicator);

            Console.SetIn(new System.IO.StringReader("stefi"));
            scoreBoard.AddSkillfulPlayerToBoard(stefi, engine.Communicator);

            Console.SetIn(new System.IO.StringReader("bob"));
            scoreBoard.AddSkillfulPlayerToBoard(bob, engine.Communicator);

            Assert.AreEqual(6, scoreBoard.GetLength());
            scoreBoard.SortWinnerBoard();

            Assert.AreEqual(null, scoreBoard[0].Name);
            Assert.AreEqual(carl.Name, scoreBoard[1].Name);
            Assert.AreEqual(pepo.Name, scoreBoard[2].Name);
            Assert.AreEqual(stefi.Name, scoreBoard[3].Name);
            Assert.AreEqual(misho.Name, scoreBoard[4].Name);
            Assert.AreEqual(bob.Name, scoreBoard[5].Name);
            for (int i = 0; i < 6; i++)
            {
                misho.UpdateMovesAmmount();
            }

            Console.SetIn(new System.IO.StringReader(""));
            scoreBoard.AddSkillfulPlayerToBoard(misho, engine.Communicator);
            Assert.AreEqual(7, scoreBoard.GetLength());
            scoreBoard.SortWinnerBoard();

            Assert.AreEqual(null, scoreBoard[0].Name);
            Assert.AreEqual(carl.Name, scoreBoard[1].Name);
            Assert.AreEqual(pepo.Name, scoreBoard[2].Name);
            Assert.AreEqual(stefi.Name, scoreBoard[3].Name);
            Assert.AreEqual(misho.Name, scoreBoard[4].Name);
            Assert.AreEqual(bob.Name, scoreBoard[5].Name);
            Assert.AreEqual(null, scoreBoard[6].Name);
        }
    }
}