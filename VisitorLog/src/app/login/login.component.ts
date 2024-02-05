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
          alert('Login successful');

          // Store the token in local storage
          this.authService.setAuthToken(data.token);

          //Set the department in local storage
          this.authService.setDepartment(data.department);

           // Check the department and navigate to the appropriate page
          if (data.department == 'Reception') {
            //this.router.navigate(['/entry']);
            window.location.assign('/entry');
          }
          else if (data.department == 'Administration') {
            //this.router.navigate(['/admin']);
            window.location.assign('/admin');
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
