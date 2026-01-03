import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { CloudUploadResponse } from '../models/cloud-upload.model';

@Injectable({
  providedIn: 'root',
})
export class CloudService {
  private baseUrl = `${environment.apiBaseUrl}/api/CloudStorage`;

  constructor(private http: HttpClient) {}

  // Upload file to cloud storage
  uploadFile(file: File): Observable<CloudUploadResponse> {
    const formData = new FormData();
    formData.append('file', file);

    return this.http.post<CloudUploadResponse>(
      `${this.baseUrl}/upload`,
      formData
    );
  }

  // Upload image to cloud storage
  uploadImage(file: File): Observable<CloudUploadResponse> {
    const formData = new FormData();
    formData.append('file', file);

    return this.http.post<CloudUploadResponse>(
      `${this.baseUrl}/upload-image`,
      formData
    );
  }

  // Download file from cloud storage
  downloadFile(publicId: string): Observable<Blob> {
    return this.http.get(`${this.baseUrl}/download/${publicId}`, {
      responseType: 'blob',
    });
  }

  // Delete file from cloud storage
  deleteFile(publicId: string): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/delete/${publicId}`);
  }
}
