import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { VisitorService } from '../services/api/visitors/visitor.service';
import { DatePipe } from '@angular/common';
import { Visitor } from '../services/api/models/visitor';
import { Employee } from '../services/api/models/employee.model';
import * as bootstrap from 'bootstrap';

@Component({
  selector: 'app-visitor-management',
  templateUrl: './visitor-management.component.html',
  styleUrls: ['./visitor-management.component.css']
})
export class VisitorManagementComponent implements OnInit{
  searchForm!: FormGroup;
  visitors!: any[];
  employees: Employee[] = [];

  constructor(private fb: FormBuilder, private visitorService: VisitorService, private datePipe: DatePipe) {}

  ngOnInit(): void {
    this.createForm();
    this.loadEmployees();
  }

  createForm(): void {
    this.searchForm = this.fb.group({
      startDate: ['', Validators.required],
      endDate: ['', Validators.required],
      employeeNumber: ['', Validators.required],
      tagNumber: ['', Validators.required],
      fullName: ['', Validators.required]
    });
  }

  loadEmployees(): void {
    this.visitorService.getEmployees().subscribe(
      (data: Employee[]) => {
        this.employees = data;
      },
      (error: any) => {
        console.error('Error fetching employees', error);
      }
    );
  }

  submitForm(): void {
    console.log('Form Submitted:', this.searchForm.value);
    const formData = this.searchForm.value;
    const searchParams: any = {};
  
    // Check each form field and add it to the searchParams object if it has a value
    for (const key in formData) {
      if (formData.hasOwnProperty(key) && formData[key] !== '') {
        searchParams[key] = formData[key];
      }
    }
  
    this.visitorService.searchVisitors(searchParams).subscribe(
      (data: Visitor[]) => {
        this.visitors = data;
      },
      (error: any) => {
        console.error('Error fetching visitors', error);
      }
    );
  }
  

  getEmployeeName(employeeNumber: string): string {
    const employee = this.employees.find(emp => emp.employeeNumber === employeeNumber);
    return employee ? `${employee.firstName} ${employee.lastName}` : '';
  } 

  openPhotoModal(photoUrl: string): void {
    const modalPhoto = document.getElementById('modalPhoto') as HTMLImageElement;
    modalPhoto.src = photoUrl;
    const photoModal = new bootstrap.Modal(document.getElementById('photoModal') as HTMLElement);
    photoModal.show();
  }

}


