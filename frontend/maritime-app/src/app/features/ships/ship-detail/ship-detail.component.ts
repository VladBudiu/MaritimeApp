import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ShipService } from '../../services/ship.service';
import { Ship } from '../../models/ship.model';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { MatCardModule } from '@angular/material/card';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { Router } from '@angular/router';
import { FullDetailShip } from '../../models/ship.model';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-ship-detail',
  standalone: true,
  imports: [CommonModule, FormsModule,
    MatCardModule,
    MatTableModule,
    MatButtonModule,
    MatIconModule,
    MatInputModule,
    RouterModule
  ],
  templateUrl: './ship-detail.component.html',
  styleUrls: ['./ship-detail.component.scss'],
})
export class ShipDetailComponent implements OnInit {
  ship?: FullDetailShip

  constructor(private route: ActivatedRoute, private shipService: ShipService) {}

  ngOnInit(): void {
    const id = Number(this.route.snapshot.paramMap.get('id'));
    this.shipService.getShipById(id).subscribe((ship) => {
      this.ship = ship;
    });
  }
}
