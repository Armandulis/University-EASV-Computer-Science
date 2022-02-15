using Entities.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RatingAndSorting
{
    public interface IMethods
    {
        // 1. On input N, what are the number of reviews from reviewer N?
        int AmountOfReviews(int reviewer);
        //2. On input N, what is the average rate that reviewer N had given?
        double AvarageRateReviewer(int reviewer);
        // 3. On input N and G, how many times has reviewer N given a movie grade G?
         int AmountOfTimesReviewerRadedG(int reviewer, int grade);
        //4. On input N, how many have reviewed movie N? 
         int AmountOfReviewsForMovie(int movie);
        //5. On input N, what is the average rate the movie N had received?
         double AvarageMovieGrade(int movie);
        //6. On input N and G, how many times had movie N received grade G?         int AmountOfMovieRecievedGrade(int movie, int grade);
        //7. What is the id(s) of the movie(s) with the highest number of top rates (5)?
        MovieReviews Top5Movies();
        //8. What reviewer(s) had done most reviews?
        MovieReviews TopReviewers();
        //9. On input N, what is top N of movies? The score of a movie is its average rate.
         int TopMovies();
        // 10. On input N, what are the movies that reviewer N has reviewed?
        //The list should be sorted decreasing by rate first, and date secondly.
        IOrderedEnumerable<MovieReviews> ListOfMoviesFromReviever(int reviwer);
        //11. On input N, what are the reviewers that have reviewed movie N? 
        //The list should be sorted decreasing by rate first, and date secondly
         IOrderedEnumerable<MovieReviews> ReviewersOfMovie(int movie);
    }
}
