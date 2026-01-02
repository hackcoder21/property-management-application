import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/core/services/auth.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  
  registerForm: FormGroup = new FormGroup({});
  
  constructor(private formBuilder: FormBuilder, 
    private router: Router, 
    private authService: AuthService) { }

  ngOnInit(): void {
    this.registerForm = this.formBuilder.group({
      username: ['', [Validators.required, Validators.email]],
      password: [
        '', 
        [
          Validators.required, 
          Validators.minLength(6), 
          Validators.pattern(/^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\W_]).+$/)
        ]
      ],
    });
  }

  onRegister() {
    if (this.registerForm.invalid) return; 

    this.authService.register(this.registerForm.value).subscribe({
      next: (response) => {
        alert(response);
        this.router.navigate(['/login']);
      },
      error: (error) => {
        alert('Registration failed. Please try again.');
        console.error(error);
      }
    });
  }
}
