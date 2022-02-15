using Entities.Json;
using Newtonsoft.Json;
using RatingAndSorting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace RatingAndSorting
{
    public class Methods : IMethods
    {
        IEnumerable<MovieReviews> reviews;

        public Methods(IEnumerable<MovieReviews> list)
        {
            reviews = list;
        }
       
        // 1. On input N, what are the number of reviews from reviewer N?
        public int AmountOfReviews(int reviewer)
        {

            int counter = 0;

            foreach (MovieReviews movieReviews in reviews)
            {
                if (movieReviews.Reviewer == reviewer)
                {
                    counter++;
                }
            }
            return counter;
        }

        //2. On input N, what is the average rate that reviewer N had given?
        public double AvarageRateReviewer(int reviewer)
        {
            int counter = 0;
            int avarageRating = 0;

            foreach (MovieReviews movieReviews in reviews)
            {
                if (movieReviews.Reviewer == reviewer)
                {
                    avarageRating = avarageRating + movieReviews.Grade;
                    counter++;
                }
            }
            double total = avarageRating / counter;
            return total;
        }
        // 3. On input N and G, how many times has reviewer N given a movie grade G?
        public int AmountOfTimesReviewerRadedG(int reviewer, int grade)
        {
            int counter = 0;

            foreach (MovieReviews movieReviews in reviews)
            {
                if (movieReviews.Reviewer == reviewer && movieReviews.Grade == grade)
                {
                    counter++;
                }
            }
            return counter;

        }

        //4. On input N, how many have reviewed movie N? 
        public int AmountOfReviewsForMovie(int movie)
        {
            int counter = 0;

            foreach (MovieReviews movieReviews in reviews)
            {
                if (movieReviews.Movie == movie)
                {
                    counter++;
                }
            }

            return counter;
        }

        //5. On input N, what is the average rate the movie N had received?
        public double AvarageMovieGrade(int movie)
        {
            int counter = 0;
            int avarageRating = 0;

            foreach (MovieReviews movieReviews in reviews)
            {
                if (movieReviews.Movie == movie)
                {
                    avarageRating = avarageRating + movieReviews.Grade;
                    counter++;
                }
            }
            double total = avarageRating / counter;

            return total;
        }

        //6. On input N and G, how many times had movie N received grade G?
        public int AmountOfMovieRecievedGrade(int movie, int grade)
        {
            int counter = 0;

            foreach (MovieReviews movieReviews in reviews)
            {
                if (movieReviews.Movie == movie && movieReviews.Grade == grade)
                {
                    counter++;
                }
            }
            return counter;
        }

        //7. What is the id(s) of the movie(s) with the highest number of top rates (5)?
        public MovieReviews Top5Movies()
        {

            List < MovieReviews > listOfMovies5 = new List<MovieReviews>();
            foreach (var item in reviews)
            {
                if ( item.Grade == 5) { listOfMovies5.Add(item); }
            }


            var rew = listOfMovies5.GroupBy(i => i.Movie);
            MovieReviews currentMost = null;
            MovieReviews tempRev =null;
            int mosedSummedGroup =0;
            foreach (var group in rew)
            {
                var currentGroupSum = 0;

                foreach (var item in group)
                {
                    currentGroupSum++;
                    tempRev = item;
                }

                if (currentGroupSum > mosedSummedGroup)
                {
                    currentMost = tempRev;
                }
            }
            return currentMost;
        }

        //8. What reviewer(s) had done most reviews?
        public MovieReviews TopReviewers()
        {
            var rew = reviews.GroupBy(i => i.Reviewer).OrderByDescending(grp => grp.Count()).Select(grp => grp.Key).First();
            var objRew = reviews.FirstOrDefault(r => r.Reviewer == rew);

            return objRew;
        }

        //9. On input N, what is top N of movies? The score of a movie is its average rate.
        public int TopMovies()
        {

            int count = 0;
            int sum = 0;
            double avgRate = 0;
            int movieId = 1;
            foreach (var item in reviews)
            {
                movieId = item.Movie;
                if (item.Movie == movieId)
                {
                    sum += item.Grade;
                    count++;

                    if (sum / count > avgRate)
                    {
                        movieId = item.Movie;
                        avgRate = sum / count;
                    }
                }
                return movieId;
            }
            return movieId;

        }

        // 10. On input N, what are the movies that reviewer N has reviewed?
        //The list should be sorted decreasing by rate first, and date secondly.
        public IOrderedEnumerable<MovieReviews> ListOfMoviesFromReviever(int reviwer)
        {
            var counter = 0;
            List<MovieReviews> pictedList = new List<MovieReviews>();

            foreach (MovieReviews movieReviews in reviews)
            {
                if (movieReviews.Reviewer == reviwer)
                {
                    counter++;
                    pictedList.Add(movieReviews);
                }
            }

            var sortedList = pictedList.OrderBy(m => m.Date).OrderBy(m => m.Grade);
            
            return sortedList;
        }

        //11. On input N, what are the reviewers that have reviewed movie N? The list should be sorted decreasing by rate first, and date secondly
        public IOrderedEnumerable<MovieReviews> ReviewersOfMovie(int movie)
        {
            List<MovieReviews> wowList = new List<MovieReviews>();

            foreach (MovieReviews movieReviewer in reviews)
            {
                if (movieReviewer.Movie == movie)
                {

                    wowList.Add(movieReviewer);

                }
            }
            var sortedList = wowList.OrderBy(m => m.Date).OrderBy(m => m.Grade);
            return sortedList;
        }

    }

}
