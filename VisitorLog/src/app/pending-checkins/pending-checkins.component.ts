import { Component, OnInit } from '@angular/core';
import { VisitorService } from '../services/api/visitors/visitor.service';
import { EmployeeService } from '../services/api/employees/employee.service';
import { DepartmentService } from '../services/api/department/department.service';
import { Visitor } from '../services/api/models/visitor';
import * as bootstrap from 'bootstrap';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-pending-checkins',
  templateUrl: './pending-checkins.component.html',
  styleUrls: ['./pending-checkins.component.css']
})
export class PendingCheckinsComponent implements OnInit {

  errorMessage: string | null = null;
  successMessage: string | null = null;
  visitors: any[] = [];
  employees: any[] = [];
  departments: any[] = [];
  searchForm!: FormGroup;
  assignedVisitorsCount: number = 0;
  filteredVisitors: Visitor[] = [];

  constructor( private fb: FormBuilder, private visitorService: VisitorService, private employeeService: EmployeeService, private departmentService: DepartmentService) { }

  ngOnInit(): void {
    this.loadVisitors();
    this.loadEmployees();
    this.loadDepartments();
    this.searchForm = this.fb.group({
      searchTerm: ['', Validators.required]
    });
  }

  getEmployeeName(employeeNumber: string): string {
    const employee = this.employees.find(emp => emp.employeeNumber === employeeNumber);
    return employee ? `${employee.lastName} ${employee.middleName} ${employee.firstName}` : '';
  }

  loadVisitors(): void {
    const currentDate = new Date();
    const currentDateString = currentDate.toISOString().slice(0, 10);
  
    this.visitorService.getVisitors().subscribe(
      (data: Visitor[]) => {
        // Filter visitors for today
        const filteredVisitors = data.filter(visitor => visitor.arrivalTime?.toString().startsWith(currentDateString));
  
        // Filter visitors with assigned tags
        this.visitors = filteredVisitors.filter(visitor => new Date(visitor.tagAssignedDateTime).getTime() !== new Date('0001-01-01T00:00:00').getTime());
  
        console.log('Visitors:', this.visitors);
  
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

  openPhotoModal(photoUrl: string): void {
    const modalPhoto = document.getElementById('modalPhoto') as HTMLImageElement;
    modalPhoto.src = photoUrl;
    const photoModal = new bootstrap.Modal(document.getElementById('photoModal') as HTMLElement);
    photoModal.show();
  }
}
