import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { EmployeeService } from '../services/api/employees/employee.service';
import { Department } from '../services/api/models/department.model';
import { DepartmentService } from '../services/api/department/department.service';

@Component({
  selector: 'app-employee-management',
  templateUrl: './employee-management.component.html',
  styleUrls: ['./employee-management.component.css']
})
export class EmployeeManagementComponent implements OnInit{
  employeeForm!: FormGroup;
  departments: Department[] = [];
  
  
  
    constructor(private fb: FormBuilder, private employeeService: EmployeeService, private departmentService: DepartmentService) { 
      this.employeeForm = this.fb.group({
        employeeNumber: ['', Validators.required],
        firstName: ['', Validators.required],
        middleName: [''],
        lastName: ['', Validators.required],
        department: ['', Validators.required],
      });
    }

    ngOnInit(): void {
      this.getDepartments();
    }

    getDepartments(): void {
      this.employeeService.getDepartments().subscribe(
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

      submitForm(): void {
        if (this.employeeForm.valid) {
          console.log('Employee details:', this.employeeForm.value);

          this.employeeService.saveEmployeeDetails(this.employeeForm.value).subscribe(
            (data: any) => {
              console.log('Employee details saved successfully', data);
              alert('Employee saved successfully');
              this.employeeForm.reset();
            },
            (error: any) => {
              console.error('Error saving employee details', error);
            }
          );

        
      }
    }

}
