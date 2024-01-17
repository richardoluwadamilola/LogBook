import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { EmployeeService } from '../services/api/employees/employee.service';

@Component({
  selector: 'app-employee-management',
  templateUrl: './employee-management.component.html',
  styleUrls: ['./employee-management.component.css']
})
export class EmployeeManagementComponent{
  employeeForm!: FormGroup;
  
    constructor(private fb: FormBuilder, private employeeService: EmployeeService) { 
      this.employeeForm = this.fb.group({
        employeeNumber: ['', Validators.required],
        firstName: ['', Validators.required],
        middleName: [''],
        lastName: ['', Validators.required],
        department: ['', Validators.required],
      });
    }

      submitForm(): void {
        if (this.employeeForm.valid) {
          console.log('Employee details:', this.employeeForm.value);

          this.employeeService.saveEmployeeDetails(this.employeeForm.value).subscribe(
            (data: any) => {
              console.log('Employee details saved successfully', data);
              this.employeeForm.reset();
            },
            (error: any) => {
              console.error('Error saving employee details', error);
            }
          );

        
      }
    }

}
