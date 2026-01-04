import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root',
})
export class ReportService {
  private baseUrl = `${environment.apiBaseUrl}/api/Report`;

  constructor(private http: HttpClient) {}

  generateReportExcel(userId: string): Observable<Blob> {
    return this.http.post(
      `${this.baseUrl}/${userId}/excel`,
      {},
      {
        responseType: 'blob',
      }
    );
  }

  generateReportPdf(userId: string): Observable<Blob> {
    return this.http.post(
      `${this.baseUrl}/${userId}/pdf`,
      {},
      {
        responseType: 'blob',
      }
    );
  }
}
