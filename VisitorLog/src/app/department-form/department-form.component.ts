import { Component, EventEmitter, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { DepartmentService } from '../services/api/department/department.service';

@Component({
  selector: 'app-department-form',
  templateUrl: './department-form.component.html',
  styleUrls: ['./department-form.component.css']
})
export class DepartmentFormComponent {
  @Output() departmentAdded = new EventEmitter<string>();

  departmentForm: FormGroup;

  constructor(private fb: FormBuilder, private departmentService: DepartmentService) { 
    this.departmentForm = this.fb.group({
      departmentName: ['', Validators.required],
    });
  }

  submitForm(): void {
    if (this.departmentForm.valid) {
      console.log('Department details:', this.departmentForm.value);
  
      this.departmentService.saveDepartmentDetails(this.departmentForm.value).subscribe(
        (data: any) => {
          console.log('Department details saved successfully', data);
          alert('Department details saved successfully');
          this.departmentForm.reset();
        },
        (error: any) => {
          console.error('Error saving department details', error);
          console.error('Error Details:', error);
        }
      );
    }
  }
  
}
