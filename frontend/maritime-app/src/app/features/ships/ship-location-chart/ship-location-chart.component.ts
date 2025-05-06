import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import {
  ChartType,
  ChartOptions,
  ChartData,
  ScatterDataPoint,
  TooltipItem
} from 'chart.js';
import { NgChartsModule } from 'ng2-charts';
import { ActivatedRoute } from '@angular/router';
import { ShipService } from '../../services/ship.service';
import 'chartjs-adapter-date-fns';
import { format } from 'date-fns';

@Component({
  selector: 'app-ship-location-chart',
  standalone: true,
  imports: [CommonModule, NgChartsModule],
  templateUrl: './ship-location-chart.component.html',
  styleUrls: ['./ship-location-chart.component.scss']
})
export class ShipLocationChartComponent implements OnInit {
  @ViewChild('scatterCanvas') scatterCanvas!: ElementRef;

  showLatitude = true;

  chartData: ChartData<'scatter'> = {
    datasets: [
      {
        label: 'Latitude (°)',
        data: [],
        pointRadius: 6,
        showLine: false,
        borderColor: '#1976d2',
        backgroundColor: '#1976d2'
      }
    ]
  };

  chartOptions: ChartOptions<'scatter'> = {
    responsive: true,
    scales: {
      x: {
        type: 'time',
        time: {
          unit: 'hour',
          tooltipFormat: 'dd/MM/yyyy HH:mm',
          displayFormats: { hour: 'dd/MM HH:mm' }
        },
        ticks: { source: 'data' },
        title: { display: true, text: 'Date & Hour' }
      },
      y: {
        title: { display: true, text: 'Latitude (°)' }
      }
    },
    plugins: {
      tooltip: {
        callbacks: {
          label: (ctx: TooltipItem<'scatter'>) => {
            const raw = ctx.raw as ScatterDataPoint & { lon: number };
            const time = format(new Date(raw.x), 'dd/MM/yyyy HH:mm');
            return `longitude: ${raw.lon}, latitude: ${raw.y} at ${time}`;
          }
        }
      }
    }
  };

  barChartData: ChartData<'bar'> = {
    labels: [],
    datasets: [
      {
        label: 'Max Speed (knots)',
        data: [],
        backgroundColor: []
      }
    ]
  };

  barChartOptions: ChartOptions<'bar'> = {
    responsive: true,
    scales: {
      x: { title: { display: true, text: 'Ships' } },
      y: { beginAtZero: true, title: { display: true, text: 'Max Speed (knots)' } }
    }
  };

  constructor(
    private route: ActivatedRoute,
    private shipService: ShipService
  ) {}

  ngOnInit(): void {
    const id = Number(this.route.snapshot.paramMap.get('id'));

    this.shipService.getShipLocations(id).subscribe(entries => {
      this.chartData.datasets[0].data = entries.map(e => ({
        x: new Date(e.timestamp).getTime(),
        y: e.latitude,
        y_original: e.latitude,
        lon: e.longitude
      }));
      this.chartData = { ...this.chartData };
    });

    this.shipService.getShips().subscribe(ships => {
      const thisShip = ships.find(s => s.id === id);
      const others = ships.filter(s => s.id !== id && s.maxSpeed != null);
      const sorted = [...others].sort((a, b) => (b.maxSpeed ?? 0) - (a.maxSpeed ?? 0));
      const fastest = sorted.slice(0, 2);
      const slowest = sorted.slice(-2);
      const avg = others.length > 0 ? others.reduce((sum, s) => sum + (s.maxSpeed ?? 0), 0) / others.length : 0;

      const labels = [
        'This Ship',
        ...fastest.map(s => `Fastest: ${s.name}`),
        ...slowest.map(s => `Slowest: ${s.name}`),
        'Fleet Avg'
      ];

      const data = [
        thisShip?.maxSpeed ?? 0,
        ...fastest.map(s => s.maxSpeed ?? 0),
        ...slowest.map(s => s.maxSpeed ?? 0),
        avg
      ];

      const colors = ['#1976d2', '#ef6c00', '#ef6c00', '#ef6c00', '#ef6c00', '#2e7d32'];

      this.barChartData = {
        labels,
        datasets: [
          {
            label: 'Max Speed (knots)',
            data,
            backgroundColor: colors
          }
        ]
      };
    });
  }

  toggleAxis() {
    this.showLatitude = !this.showLatitude;

    this.chartData.datasets[0].data = this.chartData.datasets[0].data.map((point: any) => ({
      ...point,
      y: this.showLatitude ? point.y_original : point.lon
    }));

    this.chartOptions = {
      ...this.chartOptions,
      scales: {
        ...this.chartOptions.scales,
        y: {
          title: {
            display: true,
            text: this.showLatitude ? 'Latitude (°)' : 'Longitude (°)'
          }
        }
      }
    };
  }

  exportChart() {
    const canvasEl: HTMLCanvasElement = this.scatterCanvas.nativeElement;
    const link = document.createElement('a');
    link.download = 'ship-location-chart.png';
    link.href = canvasEl.toDataURL('image/png');
    link.click();
  }
}
