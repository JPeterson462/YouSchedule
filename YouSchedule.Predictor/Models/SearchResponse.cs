using System;
using System.Collections.Generic;

namespace YouSchedule.Predictor.Models
{
    public class SearchResponse
    {
        public PaginationDetails PageInfo { get; set; }
        public string NextPageToken { get; set; }
    }

    public class PaginationDetails
    {
        public int TotalResults { get; set; }
        public int ResultsPerPage { get; set; }
    }

    public class SearchResponseResult
    {

    }
}
