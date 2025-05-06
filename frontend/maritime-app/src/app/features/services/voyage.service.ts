import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Voyage } from '../models/voyage.model';

@Injectable({ providedIn: 'root' })
export class VoyageService {
  private readonly baseUrl = 'http://localhost:5210/api';   

  constructor(private http: HttpClient) {}

  getVoyagesByShip(shipId: number): Observable<Voyage[]> {
    return this.http.get<Voyage[]>(`${this.baseUrl}/ships/${shipId}/voyages`);
  }

  createVoyage(voyage: any): Observable<any> {
    return this.http.post(`${this.baseUrl}/voyages`, voyage);
  }
  
  deleteVoyage(voyageId: number): Observable<any> {
    return this.http.delete(`${this.baseUrl}/voyages/${voyageId}`);
  }
}
