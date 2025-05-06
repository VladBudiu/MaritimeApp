import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { CountryVisit } from '../models/countryvisit.model';

@Injectable({ providedIn: 'root' })
export class VisitedCountriesService {
  private api = 'http://localhost:5210/api/visitedcountries'

  constructor(private http: HttpClient) {}

  getVisitedCountriesLastYear(shipId: number): Observable<CountryVisit[]> {
    return this.http.get<CountryVisit[]>(`${this.api}/lastyear/${shipId}`);
  }
}
