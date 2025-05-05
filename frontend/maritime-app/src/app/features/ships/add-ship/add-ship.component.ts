import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { ShipService } from '../../services/ship.service';
import { MatCardModule } from '@angular/material/card';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';

@Component({
  selector: 'app-add-ship',
  standalone: true,
  imports: [CommonModule, FormsModule,
    MatCardModule,
    MatTableModule,
    MatButtonModule,
    MatIconModule,
    MatInputModule,
  ],
  templateUrl: './add-ship.component.html',
  styleUrls: ['./add-ship.component.scss'],
})
export class AddShipComponent {
  name = '';
  imoNumber = '';
  maxSpeed!: number;

  constructor(private shipService: ShipService, private router: Router) {}

  onSubmit(): void {
    if (!this.name || !this.maxSpeed) return;

    this.shipService.addShip({ name: this.name, maxSpeed: this.maxSpeed, imoNumber: this.imoNumber }).subscribe(() => {
      this.router.navigate(['/ships']);
    });
  }
}
