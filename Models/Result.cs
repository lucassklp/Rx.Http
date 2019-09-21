using System.Collections.Generic;

namespace Models
{
    public class Result
    {
        public int page { get; set; }

        public List<Movie> results { get; set; }

        public int total_result { get; set; }
        public int total_pages { get; set; }
    }
}