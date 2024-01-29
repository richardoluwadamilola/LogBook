import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { VisitorService } from '../services/api/visitors/visitor.service';
import { DatePipe } from '@angular/common';
import { ReasonForVisit, Visitor } from '../services/api/models/visitor';

@Component({
  selector: 'app-visitor-management',
  templateUrl: './visitor-management.component.html',
  styleUrls: ['./visitor-management.component.css']
})
export class VisitorManagementComponent implements OnInit{
  searchForm!: FormGroup;
  visitors!: any[];

  constructor(private fb: FormBuilder, private visitorService: VisitorService, private datePipe: DatePipe) {}

  ngOnInit(): void {
    this.createForm();
  }

  createForm(): void {
    this.searchForm = this.fb.group({
      arrivalTime: ['', Validators.required],
      employeeNumber: ['', Validators.required],
      tagNumber: ['', Validators.required],
    });
  }

  submitForm(): void {
    const formData = this.searchForm.value;
    if (formData.arrivalTime) {
      this.visitorService.getVisitorsByCheckInDate(formData.arrivalTime).subscribe(
        (data: Visitor[]) => {
          this.visitors = data;
        },
        (error: any) => {
          console.error('Error fetching visitors', error);
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
  } else if (formData.tagNumber) {
    this.visitorService.getVisitorbyTagNumber(formData.tagNumber).subscribe(
      (data: Visitor) => {
        this.visitors = data ? [data]: [];
      },
      (error: any) => {
        console.error('Error fetching visitors by Tag Number', error);
      }
    );
  }
}
  
  getReasonForVisit(reasonForVisitEnum: ReasonForVisit): string {
    return ReasonForVisit[reasonForVisitEnum];
  }
}
