import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms';

import { MatCardModule } from '@angular/material/card';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatTooltipModule } from '@angular/material/tooltip';
import { MatSelectModule } from '@angular/material/select';

import { PortService } from '../../services/port.service';
import { Port } from '../../models/port.model';

@Component({
  selector: 'app-port-list',
  standalone: true,
  imports: [
    CommonModule,
    RouterModule,
    FormsModule,
    MatCardModule,
    MatTableModule,
    MatButtonModule,
    MatIconModule,
    MatInputModule,
    MatTooltipModule,
    MatSelectModule,
  ],
  templateUrl: './ports-list.component.html',
  styleUrls: ['./ports-list.component.scss'],
})
export class PortsListComponent implements OnInit {
  ports: Port[] = [];
  error: string | null = null;
  searchTerm = '';
  selectedCountry: string = '';
  uniqueCountries: string[] = [];

  displayedColumns = ['name', 'country', 'actions'];

  constructor(private portService: PortService) {}

  ngOnInit(): void {
    this.loadPorts();
  }

  loadPorts() {
    this.portService.getPorts().subscribe({
      next: (data) => {
        this.ports = data;
        this.portService.getPorts().subscribe({
          next: (data) => {
            this.ports = data;
            this.uniqueCountries = Array.from(
              new Set(data.map(p => p.countryName).filter(Boolean))
            ).sort() as string[];
          },
          error: () => (this.error = 'Unable to load ports.'),
        });
      },
      error: () => (this.error = 'Unable to load ports.'),
    });
  }

  get filteredPorts() {
    return this.ports.filter(p =>
      p.name.toLowerCase().includes(this.searchTerm.toLowerCase()) &&
      (!this.selectedCountry || p.countryName === this.selectedCountry)
    );
  }
}
