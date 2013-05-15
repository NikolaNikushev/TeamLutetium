using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BalloonsPop
{
    public enum Command
    {
        Restart,
        Top,
        Exit,
        Invalid,
        ParseCoordinate,
        MissingBalloon
    }

    class Engine
    {
        private PlayField field;
        private ScoreBoard scoreBoard;
        private Player player;
        private ConsoleRenderer consoleRenderer;

        public Engine(byte rowsAmmount, byte colsAmmount)
        {
            this.InitialisePlayField(rowsAmmount, colsAmmount);
        }

        public bool IsRunning
        {
            get;
            private set;
        }

        private void InitialisePlayField(byte rowsNumber, byte colsNumber)
        {
            field = new PlayField(rowsNumber, colsNumber);
            scoreBoard = new ScoreBoard();
            player = new Player();
            consoleRenderer = new ConsoleRenderer();
        }

        public void RunGame()
        {
            this.IsRunning = true;
        }

        public void ReadCommand()
        {
            string commandInput = null;
            consoleRenderer.PrintField(field);

            while (commandInput != "EXIT")
            {
                Console.WriteLine("Enter a row and column: ");
                commandInput = Console.ReadLine();
                Console.WriteLine();
                commandInput = commandInput.ToUpper().Trim();
                HandCommand(commandInput);
            }
        }

        private void HandCommand(string commandInput)
        {
            switch (ParseCommand(commandInput))
            {
                case Command.Restart:
                    field = new PlayField(5, 10);
                    player = new Player();
                    consoleRenderer.PrintField(field);
                    break;

                case Command.Top:
                    consoleRenderer.PrintWinnerBoard(scoreBoard);
                    break;

                case Command.Exit:
                    Console.WriteLine("Good Bye! ");
                    System.Threading.Thread.Sleep(2000);
                    break;

                case Command.ParseCoordinate:
                    int userInputRow = int.Parse(commandInput[0].ToString());
                    int userInputColumn = int.Parse(commandInput[2].ToString());
                    field.MakeChangesToField(userInputRow, userInputColumn);

                    player.UpdateMovesAmmount();
                    if (field.ClearedLevel()) //is empty
                    {

                        Console.WriteLine("Gratz ! You completed the level in {0} moves.\n", player.Moves);
                        if (scoreBoard.CheckIfSkilledAndAddToBoard(player))
                        {
                            scoreBoard.SortWinnerBoard();
                            consoleRenderer.PrintWinnerBoard(scoreBoard);

                            System.Threading.Thread.Sleep(3000);
                        }
                        else
                        {
                            Console.WriteLine("I am sorry you are not skillful enough for TopFive chart!");
                            System.Threading.Thread.Sleep(3000);
                        }
                        field = new PlayField(5, 10);
                        player = new Player();
                    }
                    consoleRenderer.PrintField(field);
                    break;

                case Command.Invalid:
                    if (CheckCommandForCoordinate(commandInput) && !CheckPopableBalloon(commandInput))
                    {
                        Console.WriteLine("Illegal move: cannot pop missing ballon!\n");
                    }
                    else
                    {
                        Console.WriteLine("Wrong input ! Try Again !\n");
                    }
                  
                    break;
            }
        }

        private Command ParseCommand(string commandInput)
        {
            bool isValidCoordinate = CheckCommandForCoordinate(commandInput);
            if (isValidCoordinate && CheckPopableBalloon(commandInput))
            {
                return Command.ParseCoordinate;
            }
            else if (CheckCommandForCommand(commandInput))
            {
                return (Command)Enum.Parse(typeof(Command), commandInput);
            }

            else
            {
                return Command.Invalid;
            }
        }

        private bool CheckCommandForCoordinate(string commandInput)
        {
            byte userInputRow = byte.Parse(commandInput[0].ToString());
            if (userInputRow > 4)
            {
                return false;
            }
            if ((commandInput.Length == 3) && (commandInput[0] >= '0' && commandInput[0] <= '9') &&
                       (commandInput[2] >= '0' && commandInput[2] <= '9') &&
                       (commandInput[1] == ' ' || commandInput[1] == '.' || commandInput[1] == ',') &&
                        byte.Parse(commandInput[0].ToString()) <= 4 && byte.Parse(commandInput[2].ToString()) <= 9)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool CheckPopableBalloon(string commandInput)
        {
            byte row = byte.Parse(commandInput[0].ToString());
            byte col = byte.Parse(commandInput[2].ToString());
            if (field[row, col] == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private bool CheckCommandForCommand(string commandInput)
        {
            foreach (string command in Enum.GetNames(typeof(Command)))
            {
                if (command == commandInput)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
