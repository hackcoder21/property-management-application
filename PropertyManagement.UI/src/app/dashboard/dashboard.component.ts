import { Component, OnInit } from '@angular/core';
import { User } from '../core/models/user.model';
import { UserService } from '../core/services/user.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css'],
})
export class DashboardComponent implements OnInit {
  users: User[] = [];
  isLoading: boolean = false;

  constructor(private userService: UserService, private router: Router) {}

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
    this.router.navigate(['/users/create']);
  }

  onViewUser(userId: string) {
    this.router.navigate([`/users/${userId}`]);
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
}
