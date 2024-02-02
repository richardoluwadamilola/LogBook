import { Component } from '@angular/core';
import { AuthService } from '../services/api/Auth/auth.service';
import { Form, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  loginForm!: FormGroup;

  constructor(private fb: FormBuilder, private authService: AuthService, private router: Router) { 
    this.loginForm = this.fb.group({
      username: ['', Validators.required],
      password: ['', Validators.required]
    });
  }

  login(): void {
    if (this.loginForm.valid) {
      console.log('Login details:', this.loginForm.value);

      const { username, password } = this.loginForm.value;

      this.authService.login(username, password).subscribe(
        (data: any) => {
          console.log('Login successful', data);
          //alert('Login successful');

          // Check the role from the received token or user data
          const role = ''; 

           // Check the role from the received token or user data
          if (role !== '' && role === 'admin' && this.router.url === '/admin') {
            this.router.navigateByUrl('/admin');
          } else if (role !== '' && role === 'entry' && this.router.url === '/entry') {
            this.router.navigateByUrl('/entry');
          } else {
            console.error('Invalid role or route', data);
            alert('Error logging in');
          }
         },
        (error: any) => {
          console.error('Error logging in', error);
          alert('Error logging in');
        }
      );
    }
    
  }

}
