import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { FullDetailPort, Port } from '../models/port.model';

@Injectable({
  providedIn: 'root',
})
export class PortService {
  private apiUrl = 'http://localhost:5210/api/ports';

  constructor(private http: HttpClient) {}


  getPorts(): Observable<Port[]> {
    return this.http.get<Port[]>(this.apiUrl);
  }

  getPortById(id: number): Observable<FullDetailPort> {
    return this.http.get<FullDetailPort>(`${this.apiUrl}/${id}`);
  }

  addPort(port: Omit<Port, 'id'>): Observable<FullDetailPort> {
    return this.http.post<FullDetailPort>(this.apiUrl, port);
  }


  getShipsAtPort(id: number) {
    return this.http.get(`${this.apiUrl}/${id}/ships`);
  
  }

  addShipVisit(portId: number, imoNumber: string) {
    return this.http.post(
      `${this.apiUrl}/${portId}/ships`,
      { imoNumber }
    );
  }
}
