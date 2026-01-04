import { Component, OnInit } from '@angular/core';
import { User } from '../core/models/user.model';
import { UserService } from '../core/services/user.service';
import { Router } from '@angular/router';
import { ReportService } from '../core/services/report.service';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css'],
})
export class DashboardComponent implements OnInit {
  users: User[] = [];
  isLoading: boolean = false;
  downloadingReportPerUserId: string | null = null;
  activeDropdownUserId: string | null = null;

  constructor(
    private userService: UserService,
    private router: Router,
    private reportService: ReportService
  ) {}

  ngOnInit(): void {
    this.loadUsers();
  }

  loadUsers() {
    this.isLoading = true;

    this.userService.getAllUsers().subscribe({
      next: (data) => {
        this.users = data;
        this.isLoading = false;
      },
      error: (error) => {
        this.isLoading = false;
        alert('Error loading users');
      },
    });
  }

  onCreateUser() {
    this.router.navigate(['dashboard/users/create']);
  }

  onViewProperties(userId: string) {
    this.router.navigate([`dashboard/users/${userId}/properties`]);
  }

  onDeleteUser(userId: string) {
    if (!confirm('Are you sure you want to delete this user?')) {
      return;
    }

    this.userService.deleteUser(userId).subscribe({
      next: () => {
        this.users = this.users.filter((u) => u.id !== userId);
      },
      error: () => {
        alert('Error deleting user');
      },
    });
  }

  downloadReport(userId: string, type: 'excel' | 'pdf') {
    if (this.downloadingReportPerUserId) return;

    this.activeDropdownUserId = null;
    this.downloadingReportPerUserId = userId;

    const request$ =
      type === 'excel'
        ? this.reportService.generateReportExcel(userId)
        : this.reportService.generateReportPdf(userId);

    request$.subscribe({
      next: (response) => {
        const blob = response.body!;
        const filename = this.getFilenameFromHeader(
          response.headers.get('content-disposition'),
          type
        );
        this.triggerDownload(blob, filename);
        this.downloadingReportPerUserId = null;
      },
      error: () => {
        this.downloadingReportPerUserId = null;
        alert('Error generating report');
      },
    });
  }

  getFilenameFromHeader(
    contentDisposition: string | null,
    type: 'excel' | 'pdf'
  ): string {
    if (!contentDisposition) {
      return `report.${type === 'excel' ? 'xlsx' : 'pdf'}`;
    }

    const match = /filename="?([^"]+)"?/.exec(contentDisposition);
    return match?.[1] ?? `report.${type === 'excel' ? 'xlsx' : 'pdf'}`;
  }

  private triggerDownload(blob: Blob, filename: string) {
    const url = window.URL.createObjectURL(blob);
    const a = document.createElement('a');
    a.href = url;
    a.download = filename;
    a.click();
    window.URL.revokeObjectURL(url);
  }

  toggleDropdown(userId: string) {
    this.activeDropdownUserId =
      this.activeDropdownUserId === userId ? null : userId;
  }
}
