using Entities.Json;
using Infrastructure.Data;
using RatingAndSorting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace FileReader
{
    class Printer
    {
        readonly IMethods methods;
        Stopwatch sw = new Stopwatch();

        public Printer() {

            

            Console.WriteLine("Reading file...");
            sw.Start();

            var jr = new JRatingsReader();

            methods = new Methods(jr.ReadJson());

            sw.Stop();
            
            Console.WriteLine("time for program to read the file: " + sw.Elapsed);
            Console.ReadLine();

            Functions();
        }

        public void Functions()
        {

            //1
            sw.Reset();
            sw.Start();
            Console.WriteLine("Number of reviews reviewer has given: " + methods.AmountOfReviews(571));
            sw.Stop();
            Console.WriteLine("Time Elapsed for this function: " + sw.Elapsed);
            Console.ReadLine();

            //2
            sw.Reset();
            sw.Start();
            Console.WriteLine("Avarge reviewer's rate:" + methods.AvarageRateReviewer(630));
            sw.Stop();
            Console.WriteLine("Time Elapsed for this function: " + sw.Elapsed);
            Console.ReadLine();

            //3
            sw.Reset();
            sw.Start();
            Console.WriteLine("The amount of times reviewer rated rated: " + methods.AmountOfTimesReviewerRadedG(130, 5));
            sw.Stop();
            Console.WriteLine("Time Elapsed for this function: " + sw.Elapsed);
            Console.ReadLine();

            //4
            sw.Reset();
            sw.Start();
            Console.WriteLine("Number of reviews movie was given:" + methods.AmountOfReviewsForMovie(2525645));
            sw.Stop();
            Console.WriteLine("Time Elapsed for this function: " + sw.Elapsed);
            Console.ReadLine();

            //5
            sw.Reset();
            sw.Start();
            Console.WriteLine("Avarge Movie's rate:" + methods.AvarageMovieGrade(1664010));
            sw.Stop();
            Console.WriteLine("Time Elapsed for this function: " + sw.Elapsed);
            Console.ReadLine();

            //6
            sw.Reset();
            sw.Start();
            Console.WriteLine("The amount of times movie have recieved grade G" + methods.AmountOfMovieRecievedGrade(1664010, 5));
            sw.Stop();
            Console.WriteLine("Time Elapsed for this function: " + sw.Elapsed);
            Console.ReadLine();

            //7
            sw.Reset();
            sw.Start();
            var topMovie = methods.Top5Movies();
            Console.WriteLine(" Best movie that scored most 5: " + topMovie.Movie);
            sw.Stop();
            Console.WriteLine("Time Elapsed for this function: " + sw.Elapsed);
            Console.ReadLine();

            //8
            sw.Reset();
            sw.Start();
            var reviewer = methods.TopReviewers();
            Console.WriteLine("reviewer(s) had done most reviews: " + reviewer.Reviewer);
            sw.Stop();
            Console.WriteLine("Time Elapsed for this function: " + sw.Elapsed);
            Console.ReadLine();

            //9
            sw.Reset();
            sw.Start();
            Console.WriteLine("Top Movie: " + methods.TopMovies());
            sw.Stop();
            Console.WriteLine("Time Elapsed for this function: " + sw.Elapsed);
            Console.ReadLine();

            //10 Takes a while to print all Movies, but function itself takes no more than 2 seconds
            var sortedList = methods.ListOfMoviesFromReviever(571);
            Console.WriteLine("Reviewer have reviewed these movies: ");
            foreach (MovieReviews movieReview in sortedList)
            {
                Console.WriteLine("Movie: "+ movieReview.Movie +" Grade: " + movieReview.Grade + " Date: " + movieReview.Date);
            }
            Console.ReadLine();

            //11 Takes a while to print all Reviewers, but function itself takes no more than 2 seconds
            sortedList = methods.ReviewersOfMovie(1664010);
            Console.WriteLine("Reviewers that have reviewed this movie: ");
            foreach (MovieReviews movieReviewer in sortedList)
            {
                Console.WriteLine("Reviewer: "+ movieReviewer.Reviewer +" Grade: " + movieReviewer.Grade + " Date: " + movieReviewer.Date);
            }
            Console.ReadLine();





        }

       

    }
}
