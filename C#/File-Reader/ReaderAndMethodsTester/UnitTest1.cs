using Entities.Json;
using FileReader;
using RatingAndSorting;
using System;
using System.Collections.Generic;
using Xunit;

namespace ReaderAndMethodsTester
{
    public class UnitTest1
    {
        [Fact]
        public void AmountOfReviewsTest()
        {
            
            List<MovieReviews> list = new List<MovieReviews>();

            list.Add( new MovieReviews() {
                Reviewer = 1,
                Movie =1,
                Grade = 5,
                Date = "2019-9-2"
            });
            list.Add(new MovieReviews()
            {
                Reviewer = 1,
                Movie = 2,
                Grade = 2,
                Date = "2010-9-2"
            });
            list.Add(new MovieReviews()
            {
                Reviewer = 4,
                Movie = 3,
                Grade = 5,
                Date = "2015-9-2"
            });

            var methods = new Methods(list);

            Assert.True(methods.AmountOfReviews(1)==2);
        }

        [Fact]
        public void AvarageRateReviewerTest()
        {

            List<MovieReviews> list = new List<MovieReviews>();

            list.Add(new MovieReviews()
            {
                Reviewer = 1,
                Movie = 1,
                Grade = 5,
                Date = "2019-9-2"
            });
            list.Add(new MovieReviews()
            {
                Reviewer = 1,
                Movie = 2,
                Grade = 2,
                Date = "2010-9-2"
            });
            list.Add(new MovieReviews()
            {
                Reviewer = 4,
                Movie = 3,
                Grade = 5,
                Date = "2015-9-2"
            });

            var methods = new Methods(list);

            Assert.True(methods.AvarageRateReviewer(1) == 3);
        }

        [Fact]
        public void AmountOfTimesReviewerRadedGTest()
        {

            List<MovieReviews> list = new List<MovieReviews>();

            list.Add(new MovieReviews()
            {
                Reviewer = 1,
                Movie = 1,
                Grade = 5,
                Date = "2019-9-2"
            });
            list.Add(new MovieReviews()
            {
                Reviewer = 1,
                Movie = 2,
                Grade = 2,
                Date = "2010-9-2"
            });
            list.Add(new MovieReviews()
            {
                Reviewer = 1,
                Movie = 3,
                Grade = 5,
                Date = "2015-9-2"
            });

            var methods = new Methods(list);

            Assert.True(methods.AmountOfTimesReviewerRadedG(1, 5) == 2);
        }

        [Fact]
        public void AmountOfReviewsForMovieTest()
        {

            List<MovieReviews> list = new List<MovieReviews>();

            list.Add(new MovieReviews()
            {
                Reviewer = 1,
                Movie = 1,
                Grade = 5,
                Date = "2019-9-2"
            });
            list.Add(new MovieReviews()
            {
                Reviewer = 1,
                Movie = 2,
                Grade = 2,
                Date = "2010-9-2"
            });
            list.Add(new MovieReviews()
            {
                Reviewer = 4,
                Movie = 3,
                Grade = 5,
                Date = "2015-9-2"
            });

            var methods = new Methods(list);

            Assert.True(methods.AmountOfReviewsForMovie(2) == 1);
        }

        [Fact]
        public void AvarageMovieGradeTest()
        {

            List<MovieReviews> list = new List<MovieReviews>();

            list.Add(new MovieReviews()
            {
                Reviewer = 1,
                Movie = 1,
                Grade = 5,
                Date = "2019-9-2"
            });
            list.Add(new MovieReviews()
            {
                Reviewer = 1,
                Movie = 3,
                Grade = 2,
                Date = "2010-9-2"
            });
            list.Add(new MovieReviews()
            {
                Reviewer = 4,
                Movie = 3,
                Grade = 4,
                Date = "2015-9-2"
            });

            var methods = new Methods(list);

            Assert.True(methods.AvarageMovieGrade(3) == 3);
        }

        [Fact]
        public void AmountOfMovieRecievedGradeTest()
        {

            List<MovieReviews> list = new List<MovieReviews>();

            list.Add(new MovieReviews()
            {
                Reviewer = 1,
                Movie = 1,
                Grade = 5,
                Date = "2019-9-2"
            });
            list.Add(new MovieReviews()
            {
                Reviewer = 1,
                Movie = 1,
                Grade = 5,
                Date = "2010-9-2"
            });
            list.Add(new MovieReviews()
            {
                Reviewer = 4,
                Movie = 1,
                Grade = 5,
                Date = "2015-9-2"
            });

            var methods = new Methods(list);

            Assert.True(methods.AmountOfMovieRecievedGrade(1, 5) == 3);
        }

        [Fact]
        public void Top5MoviesTest()
        {

            List<MovieReviews> list = new List<MovieReviews>();

            list.Add(new MovieReviews()
            {
                Reviewer = 1,
                Movie = 1,
                Grade = 1,
                Date = "2019-9-2"
            });
            list.Add(new MovieReviews()
            {
                Reviewer = 1,
                Movie = 2,
                Grade = 2,
                Date = "2010-9-2"
            });
            var item1 = new MovieReviews() 
            {
                Reviewer = 4,
                Movie = 3,
                Grade = 5,
                Date = "2015-9-2"
            };
            list.Add(item1);

            var methods = new Methods(list);

            Assert.True(methods.Top5Movies() == item1);
        }

        [Fact]
        public void TopReviewersTest()
        {

            List<MovieReviews> list = new List<MovieReviews>();

            list.Add(new MovieReviews()
            {
                Reviewer = 1,
                Movie = 1,
                Grade = 5,
                Date = "2019-9-2"
            });
            
            var item1 = new MovieReviews()
            {
                Reviewer = 4,
                Movie = 3,
                Grade = 5,
                Date = "2015-9-2"
            };
            list.Add(item1);

            list.Add(new MovieReviews()
            {
                Reviewer = 4,
                Movie = 2,
                Grade = 2,
                Date = "2010-9-2"
            });

            var methods = new Methods(list);

            Assert.True(methods.TopReviewers() == item1);
        }

        [Fact]
        public void TopMoviesTest()
        {

            List<MovieReviews> list = new List<MovieReviews>();

            list.Add(new MovieReviews()
            {
                Reviewer = 1,
                Movie = 1,
                Grade = 5,
                Date = "2019-9-2"
            });
            list.Add(new MovieReviews()
            {
                Reviewer = 1,
                Movie = 1,
                Grade = 5,
                Date = "2010-9-2"
            });
            list.Add(new MovieReviews()
            {
                Reviewer = 4,
                Movie = 2,
                Grade = 5,
                Date = "2015-9-2"
            });

            var methods = new Methods(list);

            Assert.True(methods.TopMovies() == 1);
        }
    }
}
