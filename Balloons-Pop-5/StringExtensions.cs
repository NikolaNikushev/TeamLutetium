using System;

namespace BalloonsPop5Game
{
    static class StringExtensions
    {
        //public override string ToString()
        //{
        //    return string.Format("");
        //}

        public static bool signIfSkilled(this string[,] Chart, int points)
        {
            bool Skilled = false;
            
            for (int chartPosition = 0; chartPosition < 5; chartPosition++)
            {
                if (Chart[chartPosition, 0] == null)
                {
                    Chart = AddToChart(Chart, chartPosition, points);
                    Skilled = true;
                    break;
                }
            }

            int worstMoves = 0;
            int worstMovesChartPosition = 0;
            if (Skilled == false)
            {
                for (int i = 0; i < 5; i++)
                {
                    if (int.Parse(Chart[i, 0]) > worstMoves)
                    {
                        worstMovesChartPosition = i;
                        worstMoves = int.Parse(Chart[i, 0]);
                    }
                }
            }
            if (points < worstMoves && Skilled == false)
            {
                Chart = AddToChart(Chart, worstMovesChartPosition, points);
                Skilled = true;
            }
            return Skilled;
        }

        private static string[,] AddToChart(string [,] Chart, int chartPosition, int points )
        {
            Console.WriteLine("Type in your name.");
            string userName = Console.ReadLine();

            Chart[chartPosition, 0] = points.ToString();
            Chart[chartPosition, 1] = userName;
           
            return Chart;
        }
    }
}