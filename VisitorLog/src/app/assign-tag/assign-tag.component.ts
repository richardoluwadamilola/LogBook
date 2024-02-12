import { Component, HostListener, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { TagService } from '../services/api/tags/tag.service';
import { VisitorService } from '../services/api/visitors/visitor.service';
import { ReasonForVisit, Visitor } from '../services/api/models/visitor';
import { AuthService } from '../services/api/Auth/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-assign-tag',
  templateUrl: './assign-tag.component.html',
  styleUrls: ['./assign-tag.component.css']
})
export class AssignTagComponent implements OnInit, OnDestroy {
  
  errorMessage: string | null = null;
  successMessage: string | null = null;
  visitors: any[] = [];
  employees: any[] = [];
  filteredVisitors: Visitor[] = [];
  searchForm!: FormGroup;
  tagAssignmentForm!: FormGroup;
  availableTags: any[] = [];
  currentVisitorsCount: number = 0;

  private inactivityTimeout: any;
  private readonly inactivityPeriod = 300000; // 5 minutes

  constructor(
    private fb: FormBuilder,
    private tagService: TagService,
    private visitorService: VisitorService,
    private  authService: AuthService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.initForm();
    this.loadVisitors();
    this.loadEmployees();
    this.initTagAssignmentForm();
    this.loadAvailableTags();
    this.searchForm = this.fb.group({
      searchTerm: ['', Validators.required]
    });
    this.initInactivityTimer();
    console.log('Auto logout timer started')
  }

  initTagAssignmentForm(): void {
    this.tagAssignmentForm = this.fb.group({
      selectedTag: ['', Validators.required]
    });
  }

  ngOnDestroy(): void {
    console.log('Auto logout timer stopped');
  }

  initForm(): void {}

  reasons = [
    { label: 'Official', value: ReasonForVisit.Official },
    { label: 'Personal', value: ReasonForVisit.Personal }
  ];

  assignTagToVisitor(visitorId: number, tagNumber: string): void {
    if (!tagNumber) {
      alert('Please enter a tag number.');
      return;
    }
  
    const assignTagDto = { VisitorId: visitorId, TagNumber: tagNumber };
  
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

  getEmployeeName(employeeNumber: string): string {
    const employee = this.employees.find(emp => emp.employeeNumber === employeeNumber);
    return employee ? `${employee.firstName} ${employee.middleName} ${employee.lastName}` : '';
  }

  loadAvailableTags(): void {
    this.tagService.getTags().subscribe(
      (data: any[]) => {
        this.availableTags = data;
      },
      (error: any) => {
        console.error('Error fetching available tags', error);
      }
    );
  }
  
  loadVisitors(): void {
    const currentDate = new Date();
    const currentDateString = currentDate.toISOString().slice(0, 10);

    this.visitorService.getVisitors().subscribe(
      (data: Visitor[]) => {
        this.visitors = data.filter(visitor => visitor.arrivalTime?.toString().startsWith(currentDateString));
        this.filteredVisitors = this.visitors;
        this.updateCurrentVisitorsCount(currentDate);
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

  searchVisitors(): void {
    const searchTerm = this.searchForm.get('searchTerm')?.value;
    if (searchTerm) {
      this.filteredVisitors = this.visitors.filter(visitor => visitor.fullName.toLowerCase().includes(searchTerm.toLowerCase()));
    } else {
      this.filteredVisitors = this.visitors;
    }
    this.updateCurrentVisitorsCount(new Date());
  }

  @HostListener('window:mousemove') refreshUserState() {
    console.log('Mousemove detected');
    this.resetInactivityTimer();
  }

  @HostListener('window:keypress') refreshUserStateOnKeypress() {
    console.log('Keypress detected');
    this.resetInactivityTimer();
  }

  initInactivityTimer(): void {
    this.inactivityTimeout = setTimeout(() => {
      console.log('User inactive for 5 minutes');
      // Perform logout action here
      this.logout();
    }, this.inactivityPeriod);
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

  calculateDuration(arrivalTime: string, departureTime: string): string {
    const arrival = new Date(arrivalTime);
    const departure = departureTime ? new Date(departureTime) : new Date('0001-01-01T00:00:00'); // Default to a valid date
  
    if (departure.getFullYear() === 1 && departure.getMonth() === 0 && departure.getDate() === 1) {
      // If departure time is not set, return "Not Departed Yet"
      return "Not Departed Yet";
    } else {
      // If departure time is set, calculate duration
      const duration = Math.abs(departure.getTime() - arrival.getTime());
      const hours = Math.floor(duration / 3600000);
      const minutes = Math.floor((duration % 3600000) / 60000);
      return `${hours} hours, ${minutes} minutes`;
    }
  }
  

  updateCurrentVisitorsCount(checkTime: Date): void {
    this.currentVisitorsCount = this.filteredVisitors.filter(visitor => {
      return !visitor.departureTime || new Date(visitor.departureTime).getTime() === new Date('0001-01-01T00:00:00').getTime();
    }).length;
  }
  
}
