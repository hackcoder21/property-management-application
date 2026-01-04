import { HttpClient, HttpResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root',
})
export class ReportService {
  private baseUrl = `${environment.apiBaseUrl}/api/Report`;

  constructor(private http: HttpClient) {}

  generateReportExcel(userId: string): Observable<HttpResponse<Blob>> {
    return this.http.post(
      `${this.baseUrl}/${userId}/excel`,
      {},
      {
        responseType: 'blob',
        observe: 'response',
      }
    );
  }

  generateReportPdf(userId: string): Observable<HttpResponse<Blob>> {
    return this.http.post(
      `${this.baseUrl}/${userId}/pdf`,
      {},
      {
        responseType: 'blob',
        observe: 'response',
      }
    );
  }
}
