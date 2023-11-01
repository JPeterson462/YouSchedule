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
            type: 'bar',
            data: {
                labels: ["Red", "Blue", "Yellow", "Green", "Purple", "Orange"],
                datasets: [{
                    label: '# of Votes',
                    data: [12, 19, 3, 5, 2, 3],
                    backgroundColor: [
                        'rgba(255, 99, 132, 0.2)',
                        'rgba(54, 162, 235, 0.2)',
                        'rgba(255, 206, 86, 0.2)',
                        'rgba(75, 192, 192, 0.2)',
                        'rgba(153, 102, 255, 0.2)',
                        'rgba(255, 159, 64, 0.2)'
                    ],
                    borderColor: [
                        'rgba(255,99,132,1)',
                        'rgba(54, 162, 235, 1)',
                        'rgba(255, 206, 86, 1)',
                        'rgba(75, 192, 192, 1)',
                        'rgba(153, 102, 255, 1)',
                        'rgba(255, 159, 64, 1)'
                    ],
                    borderWidth: 1
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
