using System;
using System.Collections.Generic;

namespace YouSchedule.Predictor.Models
{
    public class VideoDetailsSearchResponse : SearchResponse
    {
        public List<VideoDetailsSearchResponseResult> Items = new List<VideoDetailsSearchResponseResult>();
    }

    public class VideoDetailsSearchResponseResult : SearchResponseResult
    {
        public VideoContentDetails ContentDetails { get; set; }
    }
    public class VideoContentDetails
    {
        public string Duration { get; set; }
    }
}
