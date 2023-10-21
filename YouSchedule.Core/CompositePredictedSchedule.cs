using System;
namespace YouSchedule.Core
{
    public class CompositePredictedSchedule
    {
        // HighPrediction is within a 2 hour cluster, LowPrediction is within a 24 hour cluster
        public PredictedSchedule HighPredictionLongform { get; set; }
        public PredictedSchedule LowPredictionLongform { get; set; }

        public PredictedSchedule HighPredictionShortform { get; set; }
        public PredictedSchedule LowPredictionShortform { get; set; }
    }
}
