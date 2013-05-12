using System;
using System.Collections.Generic;

namespace BalloonsPop5Game
{
    class Balloons
    {
        static void Main(string[] args)
        {
            //Todo: Get the topFiveWinnersChart to the AddToBoard or scoreboard so it is created there

            PlayField field = new PlayField(5, 10);
            ScoreBoard scoreboard = new ScoreBoard();
            Player player = new Player(0);
            ConsoleRenderer consoleRenderer = new ConsoleRenderer();
            string[,] topFiveWinnersChart = new string[5, 2];
            
            string commandInput = null;
            int userMoves = 0;

            consoleRenderer.PrintField(field);
           
            while (commandInput != "EXIT")
            {
                Console.WriteLine("Enter a row and column: ");
                commandInput = Console.ReadLine();
                Console.WriteLine();
                commandInput = commandInput.ToUpper().Trim();

                switch (commandInput)
                {
                    case "RESTART":
                        field = new PlayField(5, 10);
                        player = new Player(0);
                        break;

                    case "TOP":
                        consoleRenderer.PrintWinnerBoard(scoreboard);
                        break;

                    case "EXIT":
                        Console.WriteLine("Good Bye! ");
                        System.Threading.Thread.Sleep(2000);
                        break;

                    default:
                        if ((commandInput.Length == 3) && (commandInput[0] >= '0' && commandInput[0] <= '9') &&
                            (commandInput[2] >= '0' && commandInput[2] <= '9') &&
                            (commandInput[1] == ' ' || commandInput[1] == '.' || commandInput[1] == ','))
                        {
                            int userInputRow = int.Parse(commandInput[0].ToString());
                            if (userInputRow > 4)
                            {
                                Console.WriteLine("Wrong input ! Try Again ! \n");
                                continue;
                            }
                            
                            int userInputColumn = int.Parse(commandInput[2].ToString());
                            if (!field.MakeChangesToField(userInputRow, userInputColumn))
                            {
                                Console.WriteLine("Illegal move: cannot pop missing ballon!\n");
                                continue;
                            }
                            player.UpdateMovesAmmount();
                            if (field.ClearedLevel()) //is empty
                            {

                                Console.WriteLine("Gratz ! You completed the level in {0} moves.\n", userMoves);
                                if (scoreboard.CheckIfSkilled(player))
                                {
                                    scoreboard.SortWinnerBoard(topFiveWinnersChart);
                                    consoleRenderer.PrintWinnerBoard(scoreboard);

                                    System.Threading.Thread.Sleep(3000);
                                }
                                else
                                {
                                    Console.WriteLine("I am sorry you are not skillful enough for TopFive chart!");
                                    System.Threading.Thread.Sleep(3000);
                                }
                                field = new PlayField(5, 10);
                                userMoves = 0;
                            }
                            consoleRenderer.PrintField(field);
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Wrong input ! Try Again !\n");
                            break;
                        }
                }
            }
        }
    }
}


