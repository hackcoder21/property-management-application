import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { AuthResponse, LoginRequest, RegisterRequest } from '../models/auth.model';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  // Define the base URL for authentication
  private baseUrl = `${environment.apiBaseUrl}/api/Auth`;

  constructor(private http: HttpClient) { }

  // Login method
  login(data: LoginRequest): Observable<AuthResponse> {
    return this.http.post<AuthResponse>(`${this.baseUrl}/Login`, data);
  }

  // Register method
  register(data: RegisterRequest): Observable<string> {
    return this.http.post(`${this.baseUrl}/Register`, data, { responseType: 'text' });
  }

  // Save token to local storage
  saveToken(token: string): void {
    localStorage.setItem('token', token);
  }

  // Get token from local storage
  getToken(): string | null {
    return localStorage.getItem('token');
  }

  // Logout method
  logout(): void {
    localStorage.removeItem('token');
  }

  // Check if user is authenticated
  isAuthenticated(): boolean {
    return this.getToken() !== null;
  }
}
