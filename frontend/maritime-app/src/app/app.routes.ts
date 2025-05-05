import { Routes } from '@angular/router';
import { PORT_ROUTES } from './features/ports/ports.routes';

export const routes: Routes = [
  {
    path: 'ships',
    loadChildren: () =>
      import('./features/ships/ships.routes').then((m) => m.SHIP_ROUTES),
  },
  { path: '', redirectTo: 'ships', pathMatch: 'full' },
  { path: 'ports', children: PORT_ROUTES},
  {
    path: 'voyages',
    loadChildren: () =>
      import('./features/voyages/voyages.routes').then(m => m.VOYAGES_ROUTES)
  },
 {
  path: 'countries',
  loadChildren: () =>
    import('./features/countries/visited-countries.routes').then(m => m.COUNTRIES_ROUTES)
}
  
];
