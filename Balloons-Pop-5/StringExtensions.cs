using System;

namespace BalloonsPop5Game
{
    static class StringExtensions
    {
        public static bool CheckIfSkilled(this string[,] chart, int points)
        {
            bool isSkilled = false;
            
            for (int chartPosition = 0; chartPosition < 5; chartPosition++)
            {
                if (chart[chartPosition, 0] == null)
                {
                    chart = AddToChart(chart, chartPosition, points);
                    isSkilled = true;
                    break;
                }
            }

            int worstMoves = 0;
            int worstMovesChartPosition = 0;
            if (isSkilled == false)
            {
                for (int i = 0; i < 5; i++)
                {
                    if (int.Parse(chart[i, 0]) > worstMoves)
                    {
                        worstMovesChartPosition = i;
                        worstMoves = int.Parse(chart[i, 0]);
                    }
                }
            }
            if (points < worstMoves && isSkilled == false)
            {
                chart = AddToChart(chart, worstMovesChartPosition, points);
                isSkilled = true;
            }
            return isSkilled;
        }

        private static string[,] AddToChart(string [,] chart, int chartPosition, int points )
        {
            Console.WriteLine("Type in your name.");
            string userName = Console.ReadLine();

            chart[chartPosition, 0] = points.ToString();
            chart[chartPosition, 1] = userName;
           
            return chart;
        }
    }
}