import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, Observer } from 'rxjs';
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

  // Create new property
  createProperty(property: Property): Observable<Property> {
    return this.http.post<Property>(this.baseUrl, property);
  }

  // Update property
  updateProperty(propertyId: string, property: Property): Observable<Property> {
    return this.http.put<Property>(`${this.baseUrl}/${propertyId}`, property);
  }

  // Delete property by property ID
  deletePropertyByPropertyId(propertyId: string): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${propertyId}`);
  }
}
