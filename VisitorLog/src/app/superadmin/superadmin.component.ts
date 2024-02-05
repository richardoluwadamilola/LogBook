import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AuthService } from '../services/api/Auth/auth.service';
import { DepartmentService } from '../services/api/department/department.service';
import { Department } from '../services/api/models/department.model';

declare var $: any;


@Component({
  selector: 'app-superadmin',
  templateUrl: './superadmin.component.html',
  styleUrls: ['./superadmin.component.css']
})
export class SuperadminComponent  implements OnInit {
  userForm!: FormGroup
  departments: Department[] = [];

  constructor(private fb: FormBuilder, private authService: AuthService, private departmentService: DepartmentService) {
    this.userForm = this.fb.group({
      username: ['', Validators.required],
      password: ['', Validators.required],
      department: ['', Validators.required]
    });
   }

  ngOnInit(): void {
    this.getDepartments();
  }

  ngAfterViewInit(): void {
    $('#department').select();
  }

  getDepartments(): void {
    this.departmentService.getDepartments().subscribe(
      (data: Department[]) => {
        //Sort departments alphabetically
        this.departments = data.sort((a, b) => a.departmentName.localeCompare(b.departmentName));
        console.log('Departments:', this.departments);
      },
      (error: any) => {
        console.error('Error getting departments', error);
      }
    );
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
