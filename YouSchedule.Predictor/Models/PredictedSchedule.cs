using System;
using System.Collections.Generic;

namespace YouSchedule.Predictor.Models
{
    public class PredictedSchedule
    {
        public List<PredictedVideo> Videos { get; set; }
    }

    public class PredictedVideo
    {
        public double Likelihood { get; set; }

        public int MinimumDurationSeconds { get; set; }
        public int MaximumDurationSeconds { get; set; }
        public int AverageDurationSeconds { get; set; }

        public RelativeDateTime MinimumPostTime { get; set; }
        public RelativeDateTime MaximumPostTime { get; set; }
        public RelativeDateTime AveragePostTime { get; set; }
    }
}
