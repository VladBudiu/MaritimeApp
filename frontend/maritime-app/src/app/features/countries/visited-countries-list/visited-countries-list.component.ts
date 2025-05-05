import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';

import { MatCardModule } from '@angular/material/card';
import { MatExpansionModule } from '@angular/material/expansion';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatListModule } from '@angular/material/list';

import { ShipService } from '../../services/ship.service';
import { VisitedCountriesService } from '../../services/visited-countries.service';
import { Ship } from '../../models/ship.model';
import { CountryVisit } from '../../models/countryvisit.model';


@Component({
  selector: 'app-visited-country-list',
  standalone: true,
  templateUrl: './visited-countries-list.component.html',
  styleUrls: ['./visited-countries-list.component.scss'],
  imports: [
    CommonModule,
    RouterModule,
    FormsModule,
    MatCardModule,
    MatExpansionModule,
    MatButtonModule,
    MatIconModule,
    MatInputModule,
    MatProgressSpinnerModule,
    MatListModule
  ]
})
export class VisitedCountryListComponent implements OnInit {
  ships: Ship[] = [];
  search = '';
  error: string | null = null;

  visitedCountries: Record<number, CountryVisit[]> = {};
  loading: Record<number, boolean> = {};

  constructor(
    private shipService: ShipService,
    private visitedCountryService: VisitedCountriesService
  ) {}

  ngOnInit(): void {
    this.shipService.getShips().subscribe({
      next: ships => (this.ships = ships),
      error: () => (this.error = 'Could not load ships.')
    });
  }

  get filteredShips(): Ship[] {
    return this.ships.filter(s =>
      s.name.toLowerCase().includes(this.search.toLowerCase())
    );
  }

  fetchVisitedCountries(shipId: number): void {
    if (this.visitedCountries[shipId] || this.loading[shipId]) return;

    this.loading[shipId] = true;
    this.visitedCountryService.getVisitedCountriesLastYear(shipId).subscribe({
      next: result => (this.visitedCountries[shipId] = result),
      error: () => (this.error = 'Could not load visited countries.'),
      complete: () => (this.loading[shipId] = false)
    });
  }
}
