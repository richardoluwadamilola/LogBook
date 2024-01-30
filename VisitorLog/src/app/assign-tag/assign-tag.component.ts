// assign-tag.component.ts

import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { TagService } from '../services/api/tags/tag.service';
import { VisitorService } from '../services/api/visitors/visitor.service';
import { ReasonForVisit } from '../services/api/models/visitor';

@Component({
  selector: 'app-assign-tag',
  templateUrl: './assign-tag.component.html',
  styleUrls: ['./assign-tag.component.css']
})
export class AssignTagComponent implements OnInit {
  
  errorMessage: string | null = null;
  successMessage: string | null = null;
  visitors: any[] = [];
  employees: any[] = [];

  constructor(
    private fb: FormBuilder,
    private tagService: TagService,
    private visitorService: VisitorService
  ) {}

  ngOnInit(): void {
    this.initForm();
    this.loadVisitors();
    this.loadEmployees();
  }

  initForm(): void {
  }

  reasons = [
    { label: 'Official', value: ReasonForVisit.Official },
    { label: 'Personal', value: ReasonForVisit.Personal }
  ];

assignTagToVisitor(): void {}

  // Method to get the reason for visit label based on the enum value
  getReasonLabel(value: number): string {
    // Implement the logic to map the enum value to the label
    return value === 0 ? 'Official' : 'Personal'; // Adjust based on your actual enum values
  }

  // assign-tag.component.ts

getEmployeeName(employeeNumber: string): string {
  const employee = this.employees.find(emp => emp.employeeNumber === employeeNumber);
  return employee ? `${employee.firstName} ${employee.middleName} ${employee.lastName}` : '';
}


  
  loadVisitors(): void {
    this.visitorService.getVisitors().subscribe(
      (data: any[]) => this.visitors = data,
      (error: any) => console.error('Error fetching visitors', error)
    );
  }

  loadEmployees(): void {
    // Call your service to get employee data
    this.visitorService.getEmployees().subscribe(
      (data: any[]) => {
        this.employees = data;
      },
      (error: any) => {
        console.error('Error fetching employees', error);
      }
    );
  }
}
