// assign-tag.component.ts

import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { TagService } from '../services/api/tags/tag.service';
import { VisitorService } from '../services/api/visitors/visitor.service';
import { ReasonForVisit, Visitor } from '../services/api/models/visitor';

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

//assign tag
assignTagToVisitor(visitorId: number): void {
  const assignTagDto = { VisitorId: visitorId };

  this.tagService.assignTagToVisitor(assignTagDto).subscribe(
    (response: any) => {
      if (!response.hasError) {
        console.log('Tag assigned successfully:', response);
        alert(`Tag ${response.data} assigned successfully`);
        this.errorMessage = null;
        // You may want to reload the visitors after successful tag assignment
        this.loadVisitors();
      } else {
        if (response.description === 'No available tags found.') {
          // Alert when no tags are available
          alert('No available tags. Please try again later.');
        }

        console.error('Error assigning tag:', response.description);
        this.errorMessage = response.description || 'Error assigning tag';
        this.successMessage = null;
      }
    },
    (error: any) => {
      console.error('Error assigning tag:', error);
      this.errorMessage = 'Error assigning tag';
      this.successMessage = null;
    }
  );
}


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


  
  // assign-tag.component.ts
loadVisitors(): void {
  const currentDate = new Date();
  const currentDateString = currentDate.toISOString().slice(0, 10);

  this.visitorService.getVisitors().subscribe(
    (data: Visitor[]) => {
      this.visitors = data.filter(visitor => visitor.arrivalTime?.toString().startsWith(currentDateString));
    },
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
  // Check out visitor
  checkoutVisitor(visitorId: number): void {
    const checkoutTagDto = { VisitorId: visitorId };

    this.tagService.checkOutVisitor(checkoutTagDto).subscribe(
      (response: any) => {
        if (!response.hasError) {
          console.log('Visitor checked out successfully:', response);
          alert('Visitor checked out successfully');
          this.errorMessage = null;
          // You may want to reload the visitors after successful checkout
          this.loadVisitors();
        } else {
          console.error('Error checking out visitor:', response.description);
          alert(`Visitor check out failed: ${response.description}`);
          this.errorMessage = response.description || 'Error checking out visitor';
          this.successMessage = null;
        }
      },
      (error: any) => {
        console.error('Error checking out visitor:', error);
        this.errorMessage = error || 'Error checking out visitor';
        this.successMessage = null;
      }
    );
  }
}
