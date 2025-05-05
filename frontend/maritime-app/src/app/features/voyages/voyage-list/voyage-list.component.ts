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
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { MatFormFieldModule } from '@angular/material/form-field';

import { ShipService } from '../../services/ship.service';
import { VoyageService } from '../../services/voyage.service';
import { Ship } from '../../models/ship.model';
import { Voyage } from '../../models/voyage.model';

import { ActivatedRoute } from '@angular/router';
import { Router } from '@angular/router';

@Component({
  selector: 'app-voyage-list',
  standalone: true,
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
    MatListModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatFormFieldModule
  ],
  templateUrl: './voyage-list.component.html',
  styleUrls: ['./voyage-list.component.scss'],
  
})
export class VoyageListComponent implements OnInit {
  ships: Ship[] = [];
  search = '';
  error: string | null = null;
  autoExpandShipId: number | null = null;

  /** cache voyages per ship‑id */
  voyages: Record<number, Voyage[]> = {};
  loadingVoyages: Record<number, boolean> = {};

  /** single active form */
  activeFormShipId: number | null = null;

  newVoyage = {
    departurePort: '',
    arrivalPort: '',
    startTime: '',
    endTime: ''
  };

  constructor(
    private shipService: ShipService,
    private voyageService: VoyageService,
    private route: ActivatedRoute,
  ) {}

  ngOnInit(): void {
    this.route.queryParams.subscribe(params => {
      const id = +params['shipId'];
      this.autoExpandShipId = isNaN(id) ? null : id;
    });
  
    this.shipService.getShips().subscribe({
      next: ships => {
        this.ships = ships;
      },
      error: () => (this.error = 'Could not load ships.')
    });
  }
  
  

  /** fetch voyages, unless already cached */
  fetchVoyages(ship: Ship): void {
    if (this.voyages[ship.id] || this.loadingVoyages[ship.id]) return;

    this.loadingVoyages[ship.id] = true;
    this.voyageService.getVoyagesByShip(ship.id).subscribe({
      next: v => {
        this.voyages[ship.id] = v;
        console.log(`Voyages for ${ship.name}:`, v);
      },
      error: () => (this.error = `Failed to load voyages for ${ship.name}`),
      complete: () => (this.loadingVoyages[ship.id] = false)
    });
  }

  /** format date */
  format(dt?: string): string {
    return dt ? new Date(dt).toLocaleDateString() : '';
  }

  /** filter ships by search term */
  get filteredShips(): Ship[] {
    return this.ships.filter(s =>
      s.name.toLowerCase().includes(this.search.toLowerCase())
    );
  }

  /** toggle which form is open */
  toggleForm(shipId: number): void {
    this.activeFormShipId = this.activeFormShipId === shipId ? null : shipId;
  }

  /** check if form is currently open */
  isFormOpen(shipId: number): boolean {
    return this.activeFormShipId === shipId;
  }

  /** submit new voyage and reload that ship’s data */
  submitVoyage(shipId: number): void {
    const voyagePayload = {
      ...this.newVoyage,
      shipId
    };

    this.voyageService.createVoyage(voyagePayload).subscribe({
      next: () => {
        // Clear form and close
        this.newVoyage = { departurePort: '', arrivalPort: '', startTime: '', endTime: '' };
        this.activeFormShipId = null;

        // Force reload
        delete this.voyages[shipId];
        this.fetchVoyages({ id: shipId } as Ship);
      },
      error: () => alert('Failed to create voyage.')
    });
  }

  deleteVoyage(shipId: number, voyageId: number): void {
    if (!confirm('Are you sure you want to delete this voyage?')) return;
  
    this.voyageService.deleteVoyage(voyageId).subscribe({
      next: () => {
        // Refresh voyages for this ship
        delete this.voyages[shipId];
        this.fetchVoyages({ id: shipId } as Ship);
      },
      error: () => alert('Failed to delete voyage.')
    });
  }
  
}
