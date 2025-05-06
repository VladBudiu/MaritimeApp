import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { MatCardModule } from '@angular/material/card';
import { MatListModule } from '@angular/material/list';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatInputModule } from '@angular/material/input';
import { MatIconModule } from '@angular/material/icon';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';

import { PortService } from '../../services/port.service';
import { FullDetailPort } from '../../models/port.model';
import { Ship } from '../../models/ship.model';

interface VisitedShip extends Ship {
  lastDeparture?: string;
}

@Component({
  selector: 'app-port-detail',
  standalone: true,
  imports: [
    CommonModule,
    RouterModule,
    FormsModule,
    MatCardModule,
    MatListModule,
    MatTableModule,
    MatButtonModule,
    MatInputModule,
    MatIconModule,
    MatDatepickerModule,
    MatNativeDateModule,
  ],
  templateUrl: './port-detail.component.html',
  styleUrls: ['./port-detail.component.scss'],
})



export class PortDetailComponent implements OnInit {
  port: FullDetailPort | null = null;
  displayedColumns: string[] = ['imo', 'name', 'lastDeparture', 'actions'];
  newImo = '';
  saving = false;
  error: string | null = null;

  filterStartDate: Date | null = null;
  filterEndDate: Date | null = null;

  constructor(
    private route: ActivatedRoute,
    private portService: PortService
  ) {}

  ngOnInit(): void {
    const id = Number(this.route.snapshot.paramMap.get('id'));
    this.loadPort(id);
  }

  loadPort(id: number) {
    this.portService.getPortById(id).subscribe({
      next: (p) => (this.port = p),
      error: () => (this.error = 'Unable to load port.'),
    });
  }

  addShipVisit() {
    if (!this.port || !this.newImo.trim()) return;
    this.saving = true;

    this.portService
      .addShipVisit(this.port.id, this.newImo.trim())
      .subscribe({
        next: () => {
          this.newImo = '';
          this.loadPort(this.port!.id); 
        },
        error: () => (this.error = 'Failed to add ship visit.'),
      })
      .add(() => (this.saving = false));
  }

  get filteredShips(): VisitedShip[] {
    if (!this.port?.shipsVisited) return [];
  
    return (this.port.shipsVisited as VisitedShip[]).filter(ship => {
      const departure = ship.lastDeparture ? new Date(ship.lastDeparture) : null;
      if (!departure) return false;
  
      return (!this.filterStartDate || departure >= this.filterStartDate) &&
             (!this.filterEndDate || departure <= this.filterEndDate);
    });
  }
}
