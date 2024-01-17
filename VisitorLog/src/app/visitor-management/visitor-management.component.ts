import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { VisitorService } from '../services/api/visitors/visitor.service';
import { DatePipe } from '@angular/common';
import { ReasonForVisit } from '../services/api/models/visitor';

@Component({
  selector: 'app-visitor-management',
  templateUrl: './visitor-management.component.html',
  styleUrls: ['./visitor-management.component.css']
})
export class VisitorManagementComponent implements OnInit{
  dateForm!: FormGroup;
  visitors!: any[];

  constructor(private fb: FormBuilder, private visitorService: VisitorService, private datePipe: DatePipe) {
    this.dateForm = this.fb.group({
      arrivalTime: [Date, Validators.required]
    });
   }

  ngOnInit(): void {}

  submitForm(): void {
    if (this.dateForm.valid) {
      const date = this.dateForm.value.arrivalTime;
      console.log('Sending date to backend:', date);
  
      this.visitorService.getVisitorsByCheckInDate(date).subscribe(
        (data: any) => {
          console.log('Raw API Response:', data);  // Log the entire response
          console.log('Visitors fetched successfully', data);
          this.visitors = data;
        },
        (error: any) => {
          console.error('Error fetching visitors', error);
        }
      );
    }
  }
  
  getReasonForVisit(reasonForVisitEnum: ReasonForVisit): string {
    return ReasonForVisit[reasonForVisitEnum];
  }
}
