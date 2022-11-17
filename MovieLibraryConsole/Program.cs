using MovieLibraryEntities.Context;
using MovieLibraryEntities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MovieLibraryConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Movie Library\n\n");
            string choice;
            do
            {
                Console.WriteLine("1) Search Movies");
                Console.WriteLine("2) Add Movie");
                Console.WriteLine("3) Update Movie");
                Console.WriteLine("4) Delete Movie");
                Console.WriteLine("X) Exit");
                Console.Write("Enter choice: ");
                choice = Console.ReadLine();
                Console.WriteLine("");

                switch (choice)
                {
                    case "1": // 1. Search Movies from database
                        using (var context = new MovieContext())
                        {
                            Console.Write("Search Title: ");
                            string movieSearch = Console.ReadLine().ToLower();

                            var movie = context.Movies.Where(x => x.Title.ToLower().Contains(movieSearch)).ToList();
                            int movieCount = 0;
                            foreach (var m in movie)
                            {
                                movieCount++;
                            }
                            System.Console.WriteLine($"{movieCount} movies found:");
                            foreach (var m in movie)
                            {
                                System.Console.WriteLine($"Movie {m.Id}: \"{m.Title}\"");
                            }
                        }
                        break;
                    case "2": // 2. Add Movie to database
                        using (var context = new MovieContext())
                        {
                            System.Console.Write("Enter Title: ");
                            var movieTitle = Console.ReadLine();

                            if (movieTitle == "" || movieTitle == null) // if entry is null
                            {
                                System.Console.WriteLine("Title cannot be empty.");
                            }
                            else
                            {
                                // Create new movie
                                var movie = new Movie();
                                movie.Title = movieTitle;

                                // Save movie object to database
                                context.Movies.Add(movie);
                                context.SaveChanges();
                                System.Console.WriteLine($"Created Movie {movie.Id}: \"{movie.Title}\"");
                            }
                        }
                        break;
                    case "3": // 3. Update Movie in database
                        using (var context = new MovieContext())
                        {
                            int movieCount = 0;
                            foreach (var mov in context.Movies)
                            {
                                movieCount++;
                            }
                            if (movieCount > 0) // if movies exist
                            {
                                Console.Write("Enter Movie ID to Update: ");
                                var movieSearch = Console.ReadLine();

                                if (int.TryParse(movieSearch, out int number) && int.Parse(movieSearch) > 0 && int.Parse(movieSearch) <= movieCount) // if entry is integer and is between 1 and number of movies
                                {
                                    var movie = context.Movies.Where(x => x.Id == int.Parse(movieSearch)).FirstOrDefault();
                                    System.Console.Write($"Found Movie {movie.Id}: \"{movie.Title}\" - Update? (Y/N) ");
                                    string confirm = Console.ReadLine();

                                    if (confirm.ToLower() == "y")
                                    {
                                        System.Console.Write("Enter New Title: ");
                                        var movieTitle = Console.ReadLine();

                                        if (movieTitle == "" || movieTitle == null) // if entry is null
                                        {
                                            System.Console.WriteLine("Title cannot be empty.");
                                        }
                                        else
                                        {
                                            movie.Title = movieTitle;
                                            context.SaveChanges();
                                            System.Console.WriteLine($"Changed Movie {movie.Id} to \"{movie.Title}\"");
                                        }
                                    }
                                }
                                else
                                {
                                    System.Console.WriteLine($"Movie ID \"{movieSearch}\" not found.");
                                }
                            }
                            else
                            {
                                System.Console.WriteLine($"No movies found.");
                            }
                        }
                        break;
                    case "4": // 4. Delete Movie from database
                        using (var context = new MovieContext())
                        {
                            int movieCount = 0;
                            foreach (var mov in context.Movies)
                            {
                                movieCount++;
                            }
                            if (movieCount > 0) // if movies exist
                            {
                                Console.Write("Enter Movie ID to Delete: ");
                                var movieSearch = Console.ReadLine();

                                if (int.TryParse(movieSearch, out int number) && int.Parse(movieSearch) > 0 && int.Parse(movieSearch) <= movieCount) // if entry is integer and is between 1 and number of movies
                                {
                                    var movie = context.Movies.Where(x => x.Id == int.Parse(movieSearch)).FirstOrDefault();
                                    System.Console.Write($"Found Movie {movie.Id}: \"{movie.Title}\" - Delete? (Y/N) ");
                                    string confirm = Console.ReadLine();

                                    if (confirm.ToLower() == "y")
                                    {
                                        context.Movies.Remove(movie);
                                        context.SaveChanges();
                                        System.Console.WriteLine($"Removed Movie {movie.Id}");
                                    }
                                }
                                else
                                {
                                    System.Console.WriteLine($"Movie ID \"{movieSearch}\" not found.");
                                }
                            }
                            else
                            {
                                System.Console.WriteLine($"No movies found.");
                            }
                        }
                        break;
                    default:
                        break;
                }
                Console.WriteLine("");
            } while (choice != null && choice.ToLower() != "x");
        }
    }
}