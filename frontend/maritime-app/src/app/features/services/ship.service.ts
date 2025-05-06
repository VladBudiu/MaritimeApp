import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { FullDetailShip, Ship } from '../models/ship.model';

@Injectable({
  providedIn: 'root'
})
export class ShipService {
  private apiUrl = 'http://localhost:5210/api/ships';

  constructor(private http: HttpClient) {}

  getShips(): Observable<Ship[]> {
    return this.http.get<Ship[]>(this.apiUrl);
  }

  getShipById(id: number): Observable<FullDetailShip> {
    return this.http.get<Ship>(`${this.apiUrl}/${id}`);
  }

  addShip(ship: Omit<Ship, 'id'>): Observable<FullDetailShip> {
    return this.http.post<Ship>(this.apiUrl, ship);
  }

  updateShip(updatedShip: FullDetailShip): Observable<FullDetailShip> {
    return this.http.put<Ship>(`${this.apiUrl}/${updatedShip.id}`, updatedShip);
  }

  deleteShip(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }

  getShipSpeedOverTime(id: number): Observable<{ timestamp: string; speed: number }[]> {
    return this.http.get<{ timestamp: string; speed: number }[]>(`${this.apiUrl}/${id}/speed-history`);
  }

  getShipLocations(id: number): Observable<{ latitude: number; longitude: number; timestamp: string }[]> {
    return this.http.get<{ latitude: number; longitude: number; timestamp: string }[]>(`${this.apiUrl}/${id}/locations`);
  }
  

}
