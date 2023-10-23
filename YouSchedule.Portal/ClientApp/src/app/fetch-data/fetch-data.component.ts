import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
// @ts-ignore
import Chart from 'chart.js/auto';

@Component({
  selector: 'app-fetch-data',
  templateUrl: './fetch-data.component.html'
})
export class FetchDataComponent {
  public prediction: CompositePredictedSchedule;

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    http.get<CompositePredictedSchedule>(baseUrl + 'predictor').subscribe(result => {
        this.prediction = result;

        const canvas = <HTMLCanvasElement>document.getElementById('longFormVideos');
        //const ctx = canvas.getContext('2d');

        var myLineChart = new Chart(canvas, {
            type: 'line',
            data: {
                //labels: ["January", "February", "March", "April", "May", "June", "July"],
                datasets: [{
                    label: "High Prediction Long Form",
                    data: [65, 59, 80, 81, 56, 55, 40],
                    backgroundColor: [
                        'rgba(105, 0, 132, .2)',
                    ],
                    borderColor: [
                        'rgba(200, 99, 132, .7)',
                    ],
                    borderWidth: 2
                },
                {
                    label: "Low Prediction Long Form",
                    data: [28, 48, 40, 19, 86, 27, 90],
                    backgroundColor: [
                        'rgba(0, 137, 132, .2)',
                    ],
                    borderColor: [
                        'rgba(0, 10, 130, .7)',
                    ],
                    borderWidth: 2
                }
                ]
            },
            options: {
                responsive: true
            }
        });
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
