import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router, RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms';

import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';

import { PortService } from '../../services/port.service';

@Component({
  selector: 'app-add-port',
  standalone: true,
  imports: [
    CommonModule,
    RouterModule,
    FormsModule,
    MatCardModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
  ],
  templateUrl: './add-port.component.html',
  styleUrls: ['./add-port.component.scss'],
})
export class AddPortComponent {
  name = '';
  countryName = '';

  constructor(private portService: PortService, private router: Router) {}

  onSubmit() {
    if (!this.name.trim()) return;

    this.portService
      .addPort({ name: this.name, countryName: this.countryName })
      .subscribe(() => this.router.navigate(['/ports']));
  }
}
