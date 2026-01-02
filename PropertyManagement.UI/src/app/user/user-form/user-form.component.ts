import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { UserService } from 'src/app/core/services/user.service';

@Component({
  selector: 'app-user-form',
  templateUrl: './user-form.component.html',
  styleUrls: ['./user-form.component.css'],
})
export class UserFormComponent implements OnInit {
  userForm: FormGroup = new FormGroup({});
  isLoading: boolean = false;

  constructor(
    private formBuilder: FormBuilder,
    private router: Router,
    private userService: UserService
  ) {}

  ngOnInit(): void {
    this.userForm = this.formBuilder.group({
      fullName: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
    });
  }

  onSubmit() {
    if (this.userForm.invalid) return;

    this.isLoading = true;

    this.userService.createUser(this.userForm.value).subscribe({
      next: () => {
        this.isLoading = false;
        alert('User created successfully');
        this.router.navigate(['/dashboard']);
      },
      error: () => {
        this.isLoading = false;
        alert('Failed to create user');
      },
    });
  }
}
