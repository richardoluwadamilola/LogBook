import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AuthService } from '../services/api/Auth/auth.service';

@Component({
  selector: 'app-superadmin',
  templateUrl: './superadmin.component.html',
  styleUrls: ['./superadmin.component.css']
})
export class SuperadminComponent  implements OnInit {
  userForm!: FormGroup

  constructor(private fb: FormBuilder, private authService: AuthService) { }

  ngOnInit(): void {
    this.initForm();
  }

  initForm(): void {
    this.userForm = this.fb.group({
      username: ['', Validators.required],
      password: ['', Validators.required],
      role: ['', Validators.required]
    });
  }

  createUser(): void {
    if (this.userForm.valid) {
      console.log('User details:', this.userForm.value);

      this.authService.createUser(this.userForm.value).subscribe(
        (data: any) => {
          console.log('User created successfully', data);
          alert('User created successfully');
          this.userForm.reset();
        },
        (error: any) => {
          console.error('Error creating user', error);
          alert('Error creating user');
        }
      );
    }
  }

}
