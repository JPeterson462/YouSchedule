import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { Chart } from 'chart.js';

import { AfterViewInit } from '@angular/core';

@Component({
    selector: 'app-fetch-data',
    templateUrl: './fetch-data.component.html',
    styleUrls: ['./fetch-data.component.css']
})
export class FetchDataComponent implements AfterViewInit {
    public prediction: CompositePredictedSchedule;

    http: HttpClient;
    baseUrl: string;

    constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
        this.http = http;
        this.baseUrl = baseUrl;
    }

    ngAfterViewInit() {
        this.http.get<CompositePredictedSchedule>(this.baseUrl + 'predictor').subscribe(result => {
            this.prediction = result;
            this.graphPredictions();
        }, error => console.error(error));
    }

    graphPredictions() {
        const canvas = <HTMLCanvasElement>document.getElementById('longFormPredictions');
        const ctx = canvas.getContext('2d');

        var myChart = new Chart(ctx, {
            type: 'line',
            data: {
                labels: ["Red", "Blue", "Yellow", "Green", "Purple", "Orange"],
                datasets: [{
                    label: 'Low Accuracy',
                    data: [12, 19, 3, 5, 2, 3],
                    backgroundColor: 'rgba(150, 150, 150, 0.5)',
                    borderColor: 'rgba(150, 150, 150, 1)',
                    borderWidth: 2,
                    fill: 1
                }, {
                        label: 'High Accuracy',
                        data: [8, 18, 1, 3, 5, 1],
                        backgroundColor: 'rgba(0, 150, 0, 0.5)',
                        borderColor: 'rgba(0, 150, 0, 1)',
                        borderWidth: 2,
                        fill: 'origin'
                    }]
            },
            options: {
                scales: {
                    yAxes: [{
                        ticks: {
                            beginAtZero: true
                        }
                    }]
                }
            }
        });

        var tst = false;
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
