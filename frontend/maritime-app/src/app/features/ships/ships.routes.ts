import { Routes } from '@angular/router';
import { ShipListComponent } from './ship-list/ship-list.component';
import { ShipDetailComponent } from './ship-detail/ship-detail.component';
import { AddShipComponent } from './add-ship/add-ship.component';
import { EditShipComponent } from './edit-ship/edit-ship.component';
import { ShipLocationChartComponent } from './ship-location-chart/ship-location-chart.component';

export const SHIP_ROUTES: Routes = [
  { path: '', component: ShipListComponent },
  { path: 'add', component: AddShipComponent },
  { path: ':id/edit', component: EditShipComponent },
  { path: ':id/chart', component: ShipLocationChartComponent }, 
  { path: ':id', component: ShipDetailComponent }
]