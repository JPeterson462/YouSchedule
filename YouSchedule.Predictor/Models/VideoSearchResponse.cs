using System;
using System.Collections.Generic;

namespace YouSchedule.Predictor.Models
{
    public class VideoSearchResponse : SearchResponse
    {
        public List<VideoSearchResponseResult> Items { get; set; }
    }

    public class VideoSearchResponseResult : SearchResponseResult
    {
        public VideoIdStructure Id { get; set; }
        public VideoSnippet Snippet { get; set; }
    }

    public class VideoIdStructure
    {
        public string VideoId { get; set; }
    }

    public class VideoSnippet
    {
        public string Title { get; set; }
        public DateTimeOffset PublishTime { get; set; }
    }
}
