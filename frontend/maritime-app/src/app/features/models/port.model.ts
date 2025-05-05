import { Ship } from './ship.model';

export interface Port {
  id: number;
  name: string;
  countryName?: string;      // e.g. “Greece”
}

export interface FullDetailPort extends Port {
  latitude?: number;
  longitude?: number;
  // all ships that have visited this port (last year or ever – up to you)
  shipsVisited?: Ship[];
}
