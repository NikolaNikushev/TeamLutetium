using System;
using System.Linq;

namespace BalloonsPop
{
    /// <summary>
    /// The engine is used to communicate with the player and the game.
    /// It checks the provided commands and sets commands to the code
    /// what the current action is and starts the process.
    /// </summary>
    public class Engine
    {
        public CurrentAction currentAction;
        private IPlayField field;
        private ScoreBoard scoreBoard;
        private Player player;
        private IUICommunicator communicator;
        private ICommandParser parser;

        /// <summary>
        /// Used to create a new engine and set it's parameters to default.
        /// Creates a new field with the provided rows and columns.
        /// Creates a new score board, a new player, a new communicator to 
        /// communicate with the console and a new parser, to parse the commands.
        /// </summary>
        /// <param name="rowsAmmount">The new field rows</param>
        /// <param name="colsAmmount">The new field columns</param>
        public Engine(byte rowsAmmount, byte colsAmmount)
        {
            field = new PlayField(rowsAmmount, colsAmmount);
            scoreBoard = new ScoreBoard();
            player = new Player();
            communicator = new ConsoleCommunicator();
            parser = new CommandParser();
        }

        /// <summary>
        /// Used for testing to get the current communicator and use it in the tests.
        /// </summary>
        public IUICommunicator Communicator
        {
            get
            {
                return this.communicator;
            }
                
        }

        /// <summary>
        /// Used for testing to get the current field that is being used.
        /// </summary>
        public IPlayField Field
        {
            get
            {
                return this.field;
            }
        }

        /// <summary>
        /// Used for testing to get the current score board that is being used.
        /// </summary>
        public ScoreBoard ScoreBoard
        {
            get
            {
                return this.scoreBoard;
            }
        }

        /// <summary>
        /// Runs the game. Prints the field and instructions 
        /// and sets the current action of the engine to running
        /// </summary>
        public virtual void RunGame()
        {
            communicator.RenderGameField(field);
            this.currentAction = CurrentAction.IsRunning;
        }

        /// <summary>
        /// Reads the command from the console and directs it to the handler what to do next.
        /// </summary>
        public void ReadAction()
        {  
            Command commandInput = Command.Invalid;
            this.currentAction = CurrentAction.IsRunning;
            string commandInputString = communicator.ProvidePlayerCommand();
            commandInput = parser.ParseCommand(commandInputString, field);
            HandleCommand(commandInput, commandInputString);
        }

        /// <summary>
        /// Handles the command that is sent from the ReadAction() method.
        /// Here are all the instructions to the engine what to do.
        /// </summary>
        /// <param name="commandInput">The command that has been started.</param>
        /// <param name="commandInputString">The command that is inputed from the console. </param>
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

        /// <summary>
        /// Prints the finished game message and checks if the player is skilled enough
        /// to enter in the top 5 board of winners.
        /// </summary>
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
