using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ConsoleApplication1.ReviewService;
using ConsoleApplication1.MovieService;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            Service_ReviewClient reviewS = new Service_ReviewClient();
            Service_MovieClient movieS = new Service_MovieClient();

            // jeśli nie ma filmów to dodaj 3
            if (!movieS.GetAllMovies().Any())
            {
                int a = movieS.AddMovie(new Movie() { ReleaseYear = 1994, Title = "Home Alone" });
                movieS.AddMovie(new Movie() { ReleaseYear = 2005, Title = "Happiness pursuit" });
                movieS.AddMovie(new Movie() { ReleaseYear = 1990, Title = "Philadelphia" });
            }


            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("Menu:");
                Console.WriteLine("Get name");
                string name = Console.ReadLine();
                Console.WriteLine("Get surname");
                string surname = Console.ReadLine();

                Console.WriteLine("a) Add review");
                Console.WriteLine("b) Edit review");
                Console.WriteLine("c) Remove review");
                Console.WriteLine("d) Show review for film");
                Console.WriteLine("e) Add film");


                var key = Console.ReadKey();
                Console.WriteLine();
                if (key.Key == ConsoleKey.A)
                {
                    Console.WriteLine("Choose number from below list:");
                    foreach (var Movie in movieS.GetAllMovies())
                    {
                        Console.WriteLine(Movie.Id);
                        Console.WriteLine(Movie.ReleaseYear);
                        Console.WriteLine(Movie.Title);
                    }
                    Review review = new Review();
                    review.MovieId = int.Parse(Console.ReadLine().ToString());
                    // jeśli ten autor już napisał recencję do owego filmu to wróć do menu
                    if (reviewS.GetAllReviews().Where(x => x.MovieId == review.MovieId).Where(x => x.Author.Name == name && x.Author.Surname == surname).Any())
                    {
                        Console.WriteLine("This author has already written review to this film.");
                        continue;
                    }
                    if (movieS.GetMovie(review.MovieId) == null)
                    {
                        Console.WriteLine("Wrong number.");
                        continue;
                    }
                    Console.WriteLine("Write review content:");
                    review.Content = Console.ReadLine();
                    Console.WriteLine("Get review score(0-100):");
                    review.Score = int.Parse(Console.ReadLine().ToString());
                    if (review.Score < 0 || review.Score > 100)
                    {
                        Console.WriteLine("Wrong score.");
                        continue;
                    }
                    review.Author = new Person() { Name = name, Surname = surname };
                    reviewS.AddReview(review);
                }
                else if (key.Key == ConsoleKey.B)
                {
                    Console.WriteLine("Choose film to edit:");
                    //wyświetlamy id filmów recenzowanych przez klienta
                    var clientReviews = reviewS.GetAllReviews().Where(x => x.Author.Name == name && x.Author.Surname == surname);
                    if (!clientReviews.Any())
                    {
                        Console.WriteLine("No reviews");
                        continue;
                    }
                    foreach (var R in clientReviews)
                    {
                        Console.WriteLine("Review's Id : {0}, Title of the movie: {1} ", R.Id, movieS.GetAllMovies().FirstOrDefault(x => x.Id == R.MovieId).Title);

                    }
                    Console.WriteLine("Choose id of review");
                    int reviewId = int.Parse(Console.ReadLine());
                    Review choosenReview = reviewS.GetReview(reviewId);
                    Console.WriteLine("Review's content:");
                    Console.WriteLine(choosenReview.Content);
                    Console.WriteLine("Review's score : {0}", choosenReview.Score);
                    Console.WriteLine("Editing review about `{0}`", movieS.GetMovie(choosenReview.MovieId).Title);
                    Console.WriteLine("Change content to:");
                    string newContent = Console.ReadLine();
                    if (newContent != "")
                    {
                        choosenReview.Content = newContent;
                    }
                    Console.WriteLine("Get new score (0-100)");
                    int newScore = int.Parse(Console.ReadLine().ToString());
                    if (newScore >= 0 && newScore <= 100)
                    {
                        choosenReview.Score = newScore;
                    }
                    reviewS.UpdateReview(choosenReview);
                }
                else if (key.Key == ConsoleKey.C)
                {
                    Console.WriteLine("Choose film to remove:");
                    //wyświetlamy id filmów recenzowanych przez klienta
                    var clientReviews = reviewS.GetAllReviews().Where(x => x.Author.Name == name && x.Author.Surname == surname);
                    if (!clientReviews.Any())
                    {
                        Console.WriteLine("No reviews");
                        continue;
                    }
                    foreach (var R in clientReviews)
                    {
                        Console.WriteLine("Review's Id : {0}, Title of the movie: {1}", R.Id, movieS.GetAllMovies().FirstOrDefault(x => x.Id == R.MovieId).Title);
                        Console.WriteLine("Content of review: {0}", R.Content);

                    }
                    Console.WriteLine("Choose review's id");
                    int reviewId = int.Parse(Console.ReadLine());
                    Console.WriteLine("Do you really want to remove review for movie `{0}` (type N/T)", movieS.GetMovie(reviewS.GetReview(reviewId).MovieId).Title);
                    key = Console.ReadKey();
                    if (key.Key == ConsoleKey.T)
                    {
                        reviewS.DeleteReview(reviewId);
                    }
                }
                else if (key.Key == ConsoleKey.D)
                {
                    Console.WriteLine("List of movies:");
                    foreach (var M in movieS.GetAllMovies())
                    {
                        Console.WriteLine("MovieId: {0}, Title: {1}", M.Id, M.Title);
                    }
                    Console.WriteLine("Get id of movie.");
                    int movieId = int.Parse(Console.ReadLine());
                    var reviews = reviewS.GetAllReviews().Where(x => x.MovieId == movieId);
                    // jeśli nie ma żadnego review
                    if (!reviews.Any())
                    {
                        Console.WriteLine("No reviews for this movie.");
                        continue;
                    }
                    Console.WriteLine("Results for `{0}`:", movieS.GetMovie(movieId).Title);
                    int sum = 0;
                    foreach (var R in reviews)
                    {
                        sum += R.Score;
                    }
                    int average = sum / reviews.Count();
                    Console.WriteLine("Average score : {0}", average);
                    foreach (var R in reviews)
                    {
                        Console.WriteLine("Author Name: {0}, Surname: {1}, Score: {2}", R.Author.Name, R.Author.Surname, R.Score);
                        Console.WriteLine("Content: {0}", R.Content);
                    }
                }
                else if (key.Key == ConsoleKey.E)
                {
                    Console.WriteLine("Add movie.");
                    Console.WriteLine("Get Title");
                    Movie movie = new Movie();
                    movie.Title = Console.ReadLine();
                    Console.WriteLine("Enter the release year");
                    movie.ReleaseYear = int.Parse(Console.ReadLine());
                    if (movieS.GetAllMovies().Where(x => x.Title == movie.Title && x.ReleaseYear == movie.ReleaseYear).Any())
                    {
                        Console.WriteLine("This film exists in database");
                        continue;
                    }
                    else
                    {
                        movieS.AddMovie(movie);
                    }
                }

                Console.WriteLine();
                Console.WriteLine("All reviews:");
                foreach (var R in reviewS.GetAllReviews())
                {
                    Console.WriteLine("ReviewId: {0}, Movie: {4}, AuthorName: {1},\n\r Content: {2}, Score: {3}", R.Id, R.Author.Name, R.Content, R.Score, movieS.GetMovie(R.MovieId).Title);
                }


            }
        }
    }
}
