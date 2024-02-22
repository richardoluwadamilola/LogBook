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
  
    if (formData.startDate && formData.endDate) {
      const startDate = this.searchForm.value.startDate;
      const endDate = this.searchForm.value.endDate;
      console.log('Start Date:', startDate);
      console.log('End Date:', endDate);
      this.visitorService.getVisitorsByDateRange(startDate, endDate).subscribe(
        (data: Visitor[]) => {
          this.visitors = data;
          console.log('Visitors:', this.visitors);
        },
        (error: any) => {
          console.error('Error fetching visitors by Date Range', error);
          console.error('Error Details:', error.error);
        }
      );
    } else if (formData.employeeNumber) {
      this.visitorService.getVisitorsByEmployeeNumber(formData.employeeNumber).subscribe(
        (data: Visitor[]) => {
          this.visitors = data;
        },
        (error: any) => {
          console.error('Error fetching visitors by Employee Number', error);
        }
      );
    } else if (formData.fullName) {
      this.visitorService.getVisitorByFullName(formData.fullName).subscribe(
        (data: Visitor[]) => {
          this.visitors = data;
        },
        (error: any) => {
          console.error('Error fetching visitors by Full Name', error);
        }
      );
    } 
    else if (formData.tagNumber) {
      this.visitorService.getVisitorbyTagNumber(formData.tagNumber).subscribe(
        (data: Visitor) => { 
          if (Array.isArray(data)) {
            // Handle the case where the API returns an array for tagNumber search
            this.visitors = data;
          } else {
            // Handle the case where the API returns a single object for tagNumber search
            this.visitors = data ? [data] : [];
          }
        },
        (error: any) => {
          console.error('Error fetching visitor by Tag Number', error);
        }
      );
    }
  }
  
  
  // getReasonForVisit(reasonForVisitEnum: ReasonForVisit): string {
  //   return ReasonForVisit[reasonForVisitEnum];
  // }

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


