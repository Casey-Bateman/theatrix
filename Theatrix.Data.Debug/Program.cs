using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Theatrix.Data.Debug
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the Theatrix.Data Debugger.");
            Console.WriteLine();

            var isRunning = true;
            while (isRunning) 
            {
                Console.Write("> ");
                var userInput = Console.ReadLine().ToLower();

                if (String.IsNullOrWhiteSpace(userInput))
                {
                    continue; 
                }

                var userBreakDown = userInput.Split(' ');
                if (userBreakDown.Length <= 1)
                {
                    continue;
                }

                switch (userBreakDown[0])
                {
                    case "exit":
                        isRunning = false;
                        break;
                    case "fm":
                        var searchPhrase = userInput.Replace("fm", "").Trim();
                        TestFlixterMoviesAPI(searchPhrase);
                        break;
                }
            } 
            

        }

        private static void TestFlixterMoviesAPI(string searchPhrase)
        {
            var flixterDao = new FlixterDao();
            var results = flixterDao.GetMovies(searchPhrase);

            if (results.Count == 0)
            {
                Console.WriteLine("No movies have been found.");
                return;
            }

            Console.WriteLine(results.Count + " movie(s) found:");
            results.ForEach(movie => Console.WriteLine(movie.Title + " (" + movie.Year + ")"));
            Console.WriteLine();
        }
    }
}
