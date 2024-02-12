import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AuthService, ChangePasswordDto, ServiceResponse } from '../services/api/Auth/auth.service';
import { error } from 'jquery';
import { Router } from '@angular/router';

@Component({
  selector: 'app-change-password',
  templateUrl: './change-password.component.html',
  styleUrls: ['./change-password.component.css']
})
export class ChangePasswordComponent implements OnInit{
  changePasswordForm!: FormGroup;

  constructor(private fb: FormBuilder, private authService: AuthService, private router: Router) {}

  ngOnInit(): void {
    this.changePasswordForm = this.fb.group({
      username: ['', Validators.required],
      oldPassword: ['', Validators.required],
      newPassword: ['', Validators.required],
      confirmNewPassword: ['', Validators.required]
  });
  }

  changePassword(): void {
    if (this.changePasswordForm.valid) {
      const changePasswordDto: ChangePasswordDto = this.changePasswordForm.value;

      this.authService.changePassword(changePasswordDto).subscribe(
        (response:ServiceResponse<string>) => {
          if (!response.hasError) {
            console.log('Password changed successfully', response.data);
            alert('Password changed successfully');
            this.resetFormAndNavigate();
          } else {
            console.error('Error changing password', response.description);
            alert('Error changing password: ' + response.description);
          }
        },
        (error: any) => {
          console.error('Errorchanging password', error);
          alert('Error changing password');
        }
      )
    }
  }

  resetFormAndNavigate(): void {
    this.changePasswordForm.reset();
    this.router.navigate(['/login']);
  }

}
