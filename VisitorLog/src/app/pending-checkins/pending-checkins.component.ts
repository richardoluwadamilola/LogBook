import { Component, OnInit } from '@angular/core';
import { VisitorService } from '../services/api/visitors/visitor.service';
import { EmployeeService } from '../services/api/employees/employee.service';
import { DepartmentService } from '../services/api/department/department.service';
import { Visitor } from '../services/api/models/visitor';
import * as bootstrap from 'bootstrap';

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

  constructor(private visitorService: VisitorService, private employeeService: EmployeeService, private departmentService: DepartmentService) { }

  ngOnInit(): void {
    this.loadVisitors();
    this.loadEmployees();
    this.loadDepartments();
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
        this.visitors = filteredVisitors.filter(visitor => new Date(visitor.tagAssignedDateTime) !== new Date('0001-01-01T00:00:00'));
  
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

  openPhotoModal(photoUrl: string): void {
    const modalPhoto = document.getElementById('modalPhoto') as HTMLImageElement;
    modalPhoto.src = photoUrl;
    const photoModal = new bootstrap.Modal(document.getElementById('photoModal') as HTMLElement);
    photoModal.show();
  }

}
