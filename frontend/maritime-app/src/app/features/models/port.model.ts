import { Ship } from './ship.model';

export interface Port {
  id: number;
  name: string;
  countryName?: string;  
}

export interface FullDetailPort extends Port {
  latitude?: number;
  longitude?: number;
  shipsVisited?: Ship[];
}
