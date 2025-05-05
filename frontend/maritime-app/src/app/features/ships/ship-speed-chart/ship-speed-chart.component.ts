import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ChartConfiguration, ChartType } from 'chart.js';
import { NgChartsModule } from 'ng2-charts';
import { ActivatedRoute } from '@angular/router';
import { ShipService } from '../../services/ship.service';

@Component({
  selector: 'app-ship-speed-chart',
  standalone: true,
  imports: [CommonModule, NgChartsModule],
  templateUrl: './ship-speed-chart.component.html',
})
export class ShipSpeedChartComponent implements OnInit {
  chartData: ChartConfiguration<'line'>['data'] = {
    labels: [],
    datasets: [
      {
        data: [],
        label: 'Speed (knots)',
        fill: false,
        tension: 0.3,
        borderColor: '#3e95cd',
        pointBackgroundColor: '#3e95cd',
      }
    ]
  };

  chartType: ChartType = 'line';

  constructor(
    private route: ActivatedRoute,
    private shipService: ShipService
  ) {}

  ngOnInit(): void {
    const id = Number(this.route.snapshot.paramMap.get('id'));
    this.shipService.getShipSpeedOverTime(id).subscribe((data) => {
      this.chartData.labels = data.map(entry => entry.timestamp);
      this.chartData.datasets[0].data = data.map(entry => entry.speed);
    });
  }
}
