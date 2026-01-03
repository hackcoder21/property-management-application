import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Property } from '../models/property.model';

@Injectable({
  providedIn: 'root',
})
export class PropertyService {
  private baseUrl: string = `${environment.apiBaseUrl}/api/Property`;

  constructor(private http: HttpClient) {}

  // Get property by user ID
  getPropertyByUserId(userId: string): Observable<Property[]> {
    return this.http.get<Property[]>(`${this.baseUrl}?userId=${userId}`);
  }

  // Get property by property ID
  getPropertyByPropertyId(propertyId: string): Observable<Property> {
    return this.http.get<Property>(`${this.baseUrl}/${propertyId}`);
  }

  // Delete property by property ID
  deletePropertyByPropertyId(propertyId: string): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${propertyId}`);
  }
}
