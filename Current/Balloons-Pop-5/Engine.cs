using System;
using System.Linq;

namespace BalloonsPop
{
    public class Engine
    {
        public CurrentAction currentAction;
        private IPlayField field;
        private ScoreBoard scoreBoard;
        private Player player;
        private IUICommunicator communicator;
        private ICommandParser parser;

        public Engine(byte rowsAmmount, byte colsAmmount)
        {
            field = new PlayField(rowsAmmount, colsAmmount);
            scoreBoard = new ScoreBoard();
            player = new Player();
            communicator = new ConsoleCommunicator();
            parser = new CommandParser();
        }

        public IUICommunicator Communicator
        {
            get
            {
                return this.communicator;
            }
                
        }

        public IPlayField Field
        {
            get
            {
                return this.field;
            }
        }

        public ScoreBoard ScoreBoard
        {
            get
            {
                return this.scoreBoard;
            }
        }

        public virtual void RunGame()
        {
            communicator.RenderGameField(field);
            this.currentAction = CurrentAction.IsRunning;
        }

        public void ReadAction()
        {  
            Command commandInput = Command.Invalid;
            this.currentAction = CurrentAction.IsRunning;
            string commandInputString = communicator.ProvidePlayerCommand();
            commandInput = parser.ParseCommand(commandInputString, field);
            HandleCommand(commandInput, commandInputString);
        }

        private void HandleCommand(Command commandInput, string commandInputString)
        {
            switch (commandInput)
            {
                case Command.Restart:
                    this.currentAction = CurrentAction.Restarting;
                    field = new PlayField(5, 10);
                    player = new Player();
                    communicator.RenderGameField(field);
                    break;

                case Command.Top:
                    this.currentAction = CurrentAction.PrintingTopBoard;
                    communicator.RenderWinnerBoard(scoreBoard);
                    break;

                case Command.Exit:
                    this.currentAction = CurrentAction.IsNotRunning;
                    communicator.PrintUserMessage("Good Bye! ");
                    System.Threading.Thread.Sleep(500);
                    break;

                case Command.CoordinateForParsing:
                    this.currentAction = CurrentAction.CheckingCoordinates;
                    int userInputRow = parser.ParseCommandToRow(commandInputString);
                    int userInputColumn = parser.ParseCommandToCol(commandInputString);

                    field.MakeChangesToField(userInputRow, userInputColumn);
                    player.UpdateMovesAmmount();
                    if (field.ClearedLevel()) //if field is empty
                    {
                        FinalizeGame();
                        this.currentAction = CurrentAction.FinishedGame;
                    }
                    communicator.RenderGameField(field);
                    break;

                case Command.Invalid:
                    this.currentAction = CurrentAction.IsWaitingForValidInput;
                    if (parser.CheckIfCommandIsCoordinate(commandInputString, field) && !parser.CheckPoppableBalloon(commandInputString, field))
                    {
                        communicator.PrintUserMessage("Illegal move: cannot pop missing ballon!\n");
                    }
                    else
                    {
                        communicator.PrintUserMessage("Wrong input ! Try Again !\n");
                    }
                    break;

            }
        }

        private void FinalizeGame()
        {
            communicator.PrintUserMessage(string.Format("Gratz ! You completed the level in {0} moves.\n", player.Moves));
            if (scoreBoard.AddSkillfulPlayerToBoard(player, communicator))
            {
                scoreBoard.SortWinnerBoard();
                communicator.RenderWinnerBoard(scoreBoard);

                System.Threading.Thread.Sleep(3000);
            }
            else
            {
                communicator.PrintUserMessage("I am sorry you are not skillful enough for TopFive chart!");
                System.Threading.Thread.Sleep(3000);
            }
            field = new PlayField(5, 10);
            player = new Player();
        }
    }
}
