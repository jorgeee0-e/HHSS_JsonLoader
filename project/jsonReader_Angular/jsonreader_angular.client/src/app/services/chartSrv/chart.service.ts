import { ElementRef, Injectable, ViewChild } from '@angular/core';
import { Chart, registerables } from 'chart.js';
Chart.register(...registerables);

@Injectable({
  providedIn: 'root'
})
export class ChartService {

  constructor() { }

  @ViewChild('forChart', { static: false }) forChart!: ElementRef<HTMLCanvasElement>;

  loadChart(graphTitle: String, key: string, primaryDatasetKey: any, labels: any, ctx:string, charType: any) {
    //var canvas = this.forChart.nativeElement;
    //var ctx = canvas.getContext('2d');
    var myChart = Chart.getChart('line-chart')
    // validar si el grafico ya existe
    if (myChart) {
      myChart.destroy();
    }
    
    
    //crear gráfico
     myChart = new Chart(ctx, {
      type: "line",
      data: {
        labels: labels,
        datasets: [{
          label: key,
          data: primaryDatasetKey,
          fill: false,
          borderColor: 'rgb(75, 192, 192)',
          
        }]
      }
    })
  }
 
}
