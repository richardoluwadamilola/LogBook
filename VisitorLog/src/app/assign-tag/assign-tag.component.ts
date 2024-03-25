import { Component, HostListener, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { TagService } from '../services/api/tags/tag.service';
import { VisitorService } from '../services/api/visitors/visitor.service';
import { Visitor } from '../services/api/models/visitor';
import { AuthService } from '../services/api/Auth/auth.service';
import { Router } from '@angular/router';
import * as bootstrap from 'bootstrap';
import { DatePipe } from '@angular/common';


@Component({
  selector: 'app-assign-tag',
  templateUrl: './assign-tag.component.html',
  styleUrls: ['./assign-tag.component.css']
})
export class AssignTagComponent implements OnInit, OnDestroy {
  [x: string]: any;

  errorMessage: string | null = null;
  successMessage: string | null = null;
  visitors: any[] = [];
  employees: any[] = [];
  departments: any[] = [];
  filteredVisitors: Visitor[] = [];
  searchForm!: FormGroup;
  tagAssignmentForm!: FormGroup;
  availableTags: any[] = [];
  currentVisitorsCount: number = 0;
  


  private inactivityTimeout: any;
  private readonly inactivityPeriod = 300000; // 5 minutes
  private readonly reloadPeriod = 15000; // 15 seconds

  constructor( private fb: FormBuilder, private tagService: TagService, private visitorService: VisitorService, private authService: AuthService, private router: Router, private datepipe: DatePipe) { }

  ngOnInit(): void {
    this.initForm();
    this.loadVisitors();
    this.loadEmployees();
    this.loadDepartments();
    this.initTagAssignmentForm();
    this.loadAvailableTags();
    this.searchForm = this.fb.group({
      searchTerm: ['', Validators.required]
    });
    this.initInactivityTimer();
    this.initReloadTimer();
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

  

  initReloadTimer(): void {
    setInterval(() => {
     this.loadVisitors();
    }, this.reloadPeriod);
  }

  initTagAssignmentForm(): void {
    this.tagAssignmentForm = this.fb.group({
      selectedTag: ['', Validators.required]
    });
  }

  initForm(): void { }

  assignTagToVisitor(visitorId: number, tagNumber: string): void {
    // Check if tag number is provided
    if (!tagNumber) {
      alert('Please enter a tag number.');
      return;
    }
  
    // Create DTO for assigning tag
    const assignTagDto = { VisitorId: visitorId, TagNumber: tagNumber };
  
    // Call tag service to assign tag to visitor
    this.tagService.assignTagToVisitor(assignTagDto).subscribe(
      (response: any) => {
        if (!response.hasError) { // If no error
          console.log('Tag assigned successfully:', response);
          alert(`Tag ${response.data} assigned successfully`);

          
  
          // Remove the assigned visitor from the list
          this.visitors = this.visitors.filter(visitor => visitor.id !== visitorId);

         
          // (Update tagAssignedDateTime and tagNumber for the visitor)
          const assignedVisitor = this.visitors.find(visitor => visitor.id === visitorId);
          if (assignedVisitor) {
            assignedVisitor.tagAssignedDateTime = new Date().toISOString();
            assignedVisitor.tagNumber = tagNumber;
          }
          
          // Reset form and update messages
          this.tagAssignmentForm.reset();
          this.loadVisitors();
          this.searchForm.reset();
          this.errorMessage = null;
          this.successMessage = `Tag ${response.data} assigned successfully`;
          
        } else { // If error occurred
          alert(response.description || 'Error assigning tag');
          console.error('Error assigning tag:', response.description);
        }
      },
      (error: any) => { // If HTTP request fails
        console.error('Error assigning tag:', error);
        alert('Error assigning tag. Please try again later.');
      }
    );
  }

  getEmployeeName(employeeNumber: string): string {
    const employee = this.employees.find(emp => emp.employeeNumber === employeeNumber);
    return employee ? `${employee.lastName} ${employee.middleName} ${employee.firstName}` : '';
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
        // Filter visitors for today
        const filteredVisitors = data.filter(visitor => visitor.arrivalTime?.toString().startsWith(currentDateString));
  
        // Sort visitors in chronological order
        filteredVisitors.sort((a, b) => {
          if (a.arrivalTime && b.arrivalTime) {
            return new Date(a.arrivalTime).getTime() - new Date(b.arrivalTime).getTime();
          }
          return 0;
        });

        // Store all visitors for count purposes
        this['allVisitors'] = filteredVisitors;

        // Filter visitors without assigned tags
        this.visitors = this['allVisitors'].filter((visitor: { tagAssignedDateTime: { toString: () => string; }; }) => {
          // Check if tagAssignedDateTime is null or equal to the default date string
          return !visitor.tagAssignedDateTime || visitor.tagAssignedDateTime.toString() === '0001-01-01T00:00:00';
        });
        
  
        console.log('All Visitors:', this['allVisitors']);
        console.log('Visitors without tags:', this.visitors);
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

  loadDepartments(): void {
    // Call your service to get department data
    this.visitorService.getDepartments().subscribe(
      (data: any[]) => {
        this.departments = data;
      },
      (error: any) => {
        console.error('Error fetching departments', error);
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

  resetInactivityTimer(): void {
    clearTimeout(this.inactivityTimeout);
    this.initInactivityTimer();
  }


  logout(): void {
    // Implement your logout logic here
    this.authService.clearAuthToken();
    this.router.navigateByUrl('/');
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

  updateCurrentVisitorsCount(currentDate: Date): void {
    const currentVisitors = this['allVisitors'].filter((visitor: { tagAssignedDateTime: string; departureTime: string | number | Date; }) => {
      // Check if visitor has been assigned a tag and has not departed yet
      return (
        (visitor.tagAssignedDateTime && visitor.tagAssignedDateTime !== '0001-01-01T00:00:00') &&
        (visitor.departureTime === '0001-01-01T00:00:00' || new Date(visitor.departureTime) > currentDate)
      );
    });
    console.log("Current Visitors:", currentVisitors);
    this.currentVisitorsCount = currentVisitors.length;
  }
  

  openPhotoModal(photoUrl: string): void {
    const modalPhoto = document.getElementById('modalPhoto') as HTMLImageElement;
    modalPhoto.src = photoUrl;
    const photoModal = new bootstrap.Modal(document.getElementById('photoModal') as HTMLElement);
    photoModal.show();
  }

}
