import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms';

import { MatCardModule } from '@angular/material/card';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';

import { ShipService } from '../../services/ship.service';
import { Ship } from '../../models/ship.model';

@Component({
  selector: 'app-ship-list',
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
  ],
  templateUrl: './ship-list.component.html',
  styleUrls: ['./ship-list.component.scss'],
})
export class ShipListComponent implements OnInit {
  ships: Ship[] = [];
  error: string | null = null;
  searchTerm: string = '';
  displayedColumns: string[] = ['name', 'speed', 'actions'];

  constructor(private shipService: ShipService) {}

  ngOnInit(): void {
    this.loadShips();
  }

  loadShips(): void {
    this.shipService.getShips().subscribe({
      next: (data) => (this.ships = data),
      error: (err) => {
        console.error('Failed to fetch ships', err);
        this.error = 'Unable to load ship data.';
      }
    });
  }

  onDelete(id: number): void {
    if (!confirm('Are you sure you want to delete this ship?')) return;

    this.shipService.deleteShip(id).subscribe({
      next: () => {
        this.ships = this.ships.filter(ship => ship.id !== id);
      },
      error: (err) => {
        console.error('Failed to delete ship', err);
      }
    });
  }

  get filteredShips(): Ship[] {
    return this.ships.filter(ship =>
      ship.name.toLowerCase().includes(this.searchTerm.toLowerCase())
    );
  }
}
