using Entities.Json;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace Infrastructure.Data
{
    public class JRatingsReader
    {
        public IEnumerable<MovieReviews> ReadJson()
        {

            
            IEnumerable<MovieReviews> reviews;
            using (StreamReader r = new StreamReader("C://Users//Arman//source//repos//JsonReaderAndMovieReviews/ratings.json"))
            {
                string json = r.ReadToEnd();
                 reviews = JsonConvert.DeserializeObject<IEnumerable<MovieReviews>>(json);
            }
            
            return reviews;

        }

    }
}
