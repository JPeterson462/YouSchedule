import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-fetch-data',
  templateUrl: './fetch-data.component.html'
})
export class FetchDataComponent {
  public prediction: CompositePredictedSchedule;

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    http.get<CompositePredictedSchedule>(baseUrl + 'predictor').subscribe(result => {
      this.prediction = result;
    }, error => console.error(error));
  }
}

interface CompositePredictedSchedule {
    highPredictionLongform: PredictedSchedule;
    lowPredictionLongform: PredictedSchedule;
    highPredictionShortform: PredictedSchedule;
    lowPredictionShortform: PredictedSchedule;
}

interface PredictedSchedule {
    videos: PredictedVideo[];
}

interface PredictedVideo {
    likelihood: number;

    minimumDurationSeconds: number;
    maximumDurationSeconds: number;
    averageDurationSeconds: number;

    minimumPostTime: RelativeDateTime;
    maximumPostTime: RelativeDateTime;
    averagePostTime: RelativeDateTime;
}

interface RelativeDateTime {
    dayOfWeekAsString: string;
    dayOfWeek: number;
    hour: number;
    minute: number;
    second: number;
    secondsSinceSunday: number;
}
