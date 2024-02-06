import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { TagService } from '../services/api/tags/tag.service';
import { VisitorService } from '../services/api/visitors/visitor.service';
import { ReasonForVisit, Visitor } from '../services/api/models/visitor';

@Component({
  selector: 'app-checkout-visitor',
  templateUrl: './checkout-visitor.component.html',
  styleUrls: ['./checkout-visitor.component.css']
})
export class CheckoutVisitorComponent implements OnInit{
  errorMessage: string | null = null;
  successMessage: string | null = null;
  visitors: any[] = [];
  employees: any[] = [];

  constructor(private fb: FormBuilder, private tagService: TagService, private visitorService: VisitorService) { }

  ngOnInit(): void {
    this.loadVisitors();
    this.loadEmployees();
  }

  initform(): void {}
  reasons = [
    { label: 'Official', value: ReasonForVisit.Official },
    { label: 'Personal', value: ReasonForVisit.Personal }
  ];

  // Method to get the reason for visit label based on the enum value
  getReasonLabel(value: number): string {
    // Implement the logic to map the enum value to the label
    return value === 0 ? 'Official' : 'Personal'; // Adjust based on your actual enum values
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

getEmployeeName(employeeNumber: string): string {
  const employee = this.employees.find(emp => emp.employeeNumber === employeeNumber);
  return employee ? `${employee.firstName} ${employee.middleName} ${employee.lastName}` : '';
}


  
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

}
