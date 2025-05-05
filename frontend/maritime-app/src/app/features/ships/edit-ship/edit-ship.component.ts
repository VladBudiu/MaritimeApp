import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ShipService } from '../../services/ship.service';
import { FullDetailShip, Ship } from '../../models/ship.model';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { MatCardModule } from '@angular/material/card';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { RouterModule } from '@angular/router';


@Component({
  selector: 'app-edit-ship',
  standalone: true,
  imports: [CommonModule, FormsModule,
    MatCardModule,
    MatTableModule,
    MatButtonModule,
    MatIconModule,
    MatInputModule,
  ],
  templateUrl: './edit-ship.component.html',
  styleUrls: ['./edit-ship.component.scss'],
})
export class EditShipComponent implements OnInit {
  ship?: FullDetailShip;
  name = '';
  imoNumber = '';
  shipType = '';
  flagCountry = '';
  owner = '';
  operator = '';
  yearBuilt?: number;
  maxSpeed!: number;
  grossTonnage?: number;
  deadweight?: number;
  lengthMeters?: number;
  beamMeters?: number;
  draftMeters?: number;
  lastKnownLat?: number;
  lastKnownLon?: number;
  lastSeenAt?: string;

  constructor(
    private route: ActivatedRoute,
    private shipService: ShipService,
    private router: Router
  ) {}

  isEdit = true;

  ngOnInit(): void {
    const id = Number(this.route.snapshot.paramMap.get('id'));
    this.shipService.getShipById(id).subscribe((ship) => {
      if (ship) {
        this.ship = ship;
        this.name = ship.name;
        this.imoNumber = ship.imoNumber ?? '';
        this.shipType = ship.shipType ?? '';
        this.flagCountry = ship.flagCountry ?? '';
        this.owner = ship.owner ?? '';
        this.operator = ship.operator ?? '';
        this.yearBuilt = ship.yearBuilt;
        this.maxSpeed = ship.maxSpeed ?? 0;
        this.grossTonnage = ship.grossTonnage;
        this.deadweight = ship.deadweight;
        this.lengthMeters = ship.lengthMeters;
        this.beamMeters = ship.beamMeters;
        this.draftMeters = ship.draftMeters;
        this.lastKnownLat = ship.lastKnownLat;
        this.lastKnownLon = ship.lastKnownLon;
        this.lastSeenAt = ship.lastSeenAt;
      }
    });
  }

  onSubmit(): void {
    if (!this.ship) return;

    const updated: FullDetailShip = {
      ...this.ship,
      name: this.name,
      imoNumber: this.imoNumber,
      shipType: this.shipType,
      flagCountry: this.flagCountry,
      owner: this.owner,
      operator: this.operator,
      yearBuilt: this.yearBuilt,
      maxSpeed: this.maxSpeed,
      grossTonnage: this.grossTonnage,
      deadweight: this.deadweight,
      lengthMeters: this.lengthMeters,
      beamMeters: this.beamMeters,
      draftMeters: this.draftMeters,
      lastKnownLat: this.lastKnownLat,
      lastKnownLon: this.lastKnownLon,
      lastSeenAt: this.lastSeenAt
    };

    this.shipService.updateShip(updated).subscribe(() => {
      this.router.navigate(['/ships']);
    });
  }
}

