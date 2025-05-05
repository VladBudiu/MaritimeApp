import { Routes } from '@angular/router';
import { PortsListComponent } from './ports-list/ports-list.component';
import { AddPortComponent } from './add-port/add-port.component';
import { PortDetailComponent } from './port-detail/port-detail.component';

export const PORT_ROUTES: Routes = [
  { path: '', component: PortsListComponent },
  { path: 'add', component: AddPortComponent },
  { path: ':id', component: PortDetailComponent },
];
