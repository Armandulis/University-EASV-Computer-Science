using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Json
{
    public class MovieReviews
    {
        public int Reviewer { get; set; }
        public int Movie { get; set; }
        public int Grade { get; set; }
        public string Date { get; set; }
    }
}
