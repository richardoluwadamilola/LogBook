import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { DepartmentService } from '../services/api/department/department.service';
import { Department } from '../services/api/models/department.model';

@Component({
  selector: 'app-department-form',
  templateUrl: './department-form.component.html',
  styleUrls: ['./department-form.component.css']
})
export class DepartmentFormComponent implements OnInit {
  @Output() departmentAdded = new EventEmitter<string>();

  departmentForm: FormGroup;
  departments: Department[] = [];
  departmentId!: number;
  departmentName!: string;


  constructor(private fb: FormBuilder, private departmentService: DepartmentService) {
    this.departmentForm = this.fb.group({
      departmentName: ['', Validators.required],
    });
  }

  ngOnInit(): void {
    this.getDepartments();
  }

  submitForm(): void {
    const department: Department = {
      departmentId: this.departmentId,
      departmentName: this.departmentForm.value.departmentName,
    }
    if (this.departmentId) {
      // If departmentId exists, update the department
      this.departmentService.updateDepartmentDetails(this.departmentId, department).subscribe(
        (data: any) => {
          console.log('Department details updated successfully', data);
          alert('Department updated successfully');
          this.departmentForm.reset();
          //this.departmentId = null; // Reset departmentId after updating
          this.getDepartments();
        },
        (error: any) => {
          console.error('Error updating department details', error);
        }
      );
    } else {
      // If departmentId does not exist, create a new department
      this.departmentService.saveDepartment(department).subscribe(
        (data: any) => {
          console.log('Department details saved successfully', data);
          alert('Department saved successfully');
          this.departmentForm.reset();
          this.getDepartments();
        },
        (error: any) => {
          console.error('Error saving department details', error);
        }
      );
    }
  }

  getDepartments(): void {
    this.departmentService.getDepartments().subscribe(
      (data: Department[]) => {
        this.departments = data;
        console.log('Departments:', this.departments);
      },
      (error: any) => {
        console.error('Error getting departments', error);
      }
    );
  }

  deleteDepartment(departmentId: number): void {
    this.departmentService.deleteDepartment(departmentId).subscribe(
      (data: any) => {
        console.log('Department deleted successfully', data);
        alert('Department deleted successfully');
        this.getDepartments();
      },
      (error: any) => {
        console.error('Error deleting department', error);
      }
    );
  }

  editDepartment(departmentId: number, departmentName: string): void {
    this.departmentId = departmentId;
    this.departmentName = departmentName;
    this.departmentForm.patchValue({
      departmentName: departmentName
    });
  }

}
