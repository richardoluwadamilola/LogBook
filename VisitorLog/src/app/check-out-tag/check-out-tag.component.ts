import { DatePipe } from '@angular/common';
import { Component, HostListener, OnInit } from '@angular/core';
import { FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Visitor } from '../services/api/models/visitor';
import { AuthService } from '../services/api/Auth/auth.service';
import { VisitorService } from '../services/api/visitors/visitor.service';
import { TagService } from '../services/api/tags/tag.service';
import { FormBuilder } from '@angular/forms';
import { Employee } from '../services/api/models/employee.model';
import { Department } from '../services/api/models/department.model';

@Component({
  selector: 'app-check-out-tag',
  templateUrl: './check-out-tag.component.html',
  styleUrls: ['./check-out-tag.component.css']
})
export class CheckOutTagComponent  implements OnInit {
  searchForm!: FormGroup;
  errorMessage: string | null = null;
  successMessage: string | null = null;
  visitors: Visitor[] = [];
  employees: Employee[] = [];
  departments: Department[] = [];
  filteredVisitors: Visitor[] = [];
  checkedInVisitors: Visitor[] = [];

  private inactivityTimeout: any;
  private readonly inactivityPeriod = 300000; // 5 minutes
  //private readonly reloadPeriod = 30000; // 30 seconds

  constructor(private fb: FormBuilder, private tagService: TagService, private visitorService: VisitorService, private authService: AuthService, private router: Router, private datepipe: DatePipe) { }

  ngOnInit(): void {
    this.initForm();
    //this.loadVisitors();
    this.loadEmployees();
    this.loadDepartments();
    this.initCheckOutForm();
    this.createForm();
    this.initInactivityTimer();
    //this.initReloadTimer();
  }

  createForm(): void {
    this.searchForm = this.fb.group({
      tagNumber: ['', Validators.required]
    });
  
  }

  ngOnDestroy(): void {
    clearTimeout(this.inactivityTimeout);
  }

  initInactivityTimer(): void {
    this.inactivityTimeout = setTimeout(() => {
      console.log('User inactive for 5 minutes');
      this.logout();
    }, this.inactivityPeriod);
  }

  // reloadPage(): void {
  //   location.reload();
  // }

  // initReloadTimer(): void {
  //   setInterval(() => {
  //     this['reloadPage']();
  //   }, this.reloadPeriod);
  // }

  

  initForm() {}

  initCheckOutForm() {}

  loadDepartments() {
    this.visitorService.getDepartments().subscribe(
      (data: any) => {
        this.departments = data;
      },
      (error: any) => {
        console.error('Error fetching departments', error);
      }
    );
  }

  loadEmployees() {
    this.visitorService.getEmployees().subscribe(
      (data: any) => {
        this.employees = data;
      },
      (error: any) => {
        console.error('Error fetching employees', error);
      }
    );
  }

  getEmployeeName(employeeNumber: string): string {
    const employee = this.employees.find(emp => emp.employeeNumber === employeeNumber);
    return employee ? `${employee.lastName} ${employee.middleName} ${employee.firstName}` : '';
  }

  //filter visitor with tag number searched  for the current date
  filterVisitorsByTagNumber(tagNumber: string): void {
    this.visitors = this.visitors.filter(visitor => visitor.tagNumber === tagNumber);
    

  }
  
  searchVisitor(): void {
    const formData = this.searchForm.value;

    if (formData.tagNumber) {
      this.visitorService.getVisitorbyTagNumber(formData.tagNumber).subscribe(
        (data: Visitor) => { 
          if (Array.isArray(data)) {
            // Handle the case where the API returns an array for tagNumber search
            this.visitors = data;
          } else {
            // Handle the case where the API returns a single object for tagNumber search
            this.visitors = data ? [data] : [];
          }
        }
      );
    }
  }
  
  checkoutVisitor(visitorId: number, tagNumber: string) {

    const checkoutTagDto = { VisitorId: visitorId, TagNumber: tagNumber };
    this.tagService.checkOutVisitor(checkoutTagDto).subscribe(
      (response: any) => {
        if (!response.hasError) {
          console.log('Visitor checked out successfully:', response);
          alert('Visitor checked out successfully');
          this.errorMessage = null;
          this.successMessage = 'Visitor checked out successfully';
          this.searchForm.reset();
          // navigate to the assign page.
          this.router.navigateByUrl('/entry');
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

  // calculateDuration(arrivalTime: string, departureTime: string): string {
  //   const arrival = new Date(arrivalTime);
  //   const departure = departureTime ? new Date(departureTime) : new Date('0001-01-01T00:00:00'); // Default to a valid date

  //   if (departure.getFullYear() === 1 && departure.getMonth() === 0 && departure.getDate() === 1) {
  //     // If departure time is not set, return "Not Departed Yet"
  //     return "Not Departed Yet";
  //   } else {
  //     // If departure time is set, calculate duration
  //     const duration = Math.abs(departure.getTime() - arrival.getTime());
  //     const hours = Math.floor(duration / 3600000);
  //     const minutes = Math.floor((duration % 3600000) / 60000);
  //     return `${hours} hours, ${minutes} minutes`;
  //   }
  // }

  @HostListener('window:mousemove') refreshUserState() {
    console.log('Mousemove detected');
    this.resetInactivityTimer();
  }

  @HostListener('window:keypress') refreshUserStateOnKeypress() {
    console.log('Keypress detected');
    this.resetInactivityTimer();
  }

  resetInactivityTimer(): void {
    clearTimeout(this.inactivityTimeout);
    this.initInactivityTimer();
  }

  logout(): void {
    // Implement your logout logic here
    this.authService.clearAuthToken();
    this.router.navigateByUrl('/login');
    console.log('User logged out due to inactivity');
  }


}
