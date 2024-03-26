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
import { DialogService } from '../services/dialog.service';

@Component({
  selector: 'app-check-out-tag',
  templateUrl: './check-out-tag.component.html',
  styleUrls: ['./check-out-tag.component.css']
})
export class CheckOutTagComponent implements OnInit {
  searchForm!: FormGroup;
  errorMessage: string | null = null;
  successMessage: string | null = null;
  visitors: Visitor[] = [];
  employees: Employee[] = [];
  departments: Department[] = [];
  filteredVisitors: Visitor[] = [];
  checkedInVisitors: Visitor[] = [];
  modalTitle: string = '';
  modalBody: string = '';

  private inactivityTimeout: any;
  private readonly inactivityPeriod = 300000; // 5 minutes

  constructor(private fb: FormBuilder, private tagService: TagService, private visitorService: VisitorService, private authService: AuthService, private router: Router, private datepipe: DatePipe, private dialogService: DialogService) { }

  ngOnInit(): void {
    this.initForm();
    this.loadEmployees();
    this.loadDepartments();
    this.initCheckOutForm();
    this.createForm();
    this.initInactivityTimer();
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

  initForm() { }

  initCheckOutForm() { }

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

  onModalConfirm(): void {
    $('#errorModal').modal('hide');
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
        async (data: Visitor | Visitor[]) => {
          if (Array.isArray(data)) {
            // Handle the case where the API returns an array for tagNumber search
            if (data.length === 0) {
              // Show alert indicating tag number is not assigned
              this.modalTitle = 'Error';
              this.modalBody = 'Tag number not assigned';
              await this.dialogService.showDialog(this.modalTitle, this.modalBody);
            } else {
              this.visitors = data;
            }
          } else {
            // Handle the case where the API returns a single object for tagNumber search
            this.visitors = data ? [data] : [];
          }
        },
        error => {
          // Handle error if any
          console.error('Error occurred:', error);
          // Show alert with error message
          alert('An error occurred while fetching data');
        }
      );
    }
  }


  checkoutVisitor(visitorId: number, tagNumber: string) {

    const checkoutTagDto = { VisitorId: visitorId, TagNumber: tagNumber };
    this.tagService.checkOutVisitor(checkoutTagDto).subscribe(
      async (response: any) => {
        if (!response.hasError) {
          console.log('Visitor checked out successfully:', response);
          this.modalTitle = 'Success';
          this.modalBody = 'Visitor checked out successfully';
          await this.dialogService.showDialog(this.modalTitle, this.modalBody);
          this.errorMessage = null;
          this.successMessage = 'Visitor checked out successfully';
          this.searchForm.reset();
          // navigate to the assign page.
          this.router.navigateByUrl('/entry');
        } else {
          console.error('Error checking out visitor:', response.description);
          this.modalTitle = 'Error';
          this.modalBody = 'Visitor check out failed: ${response.description}';
          await this.dialogService.showDialog(this.modalTitle, this.modalBody);
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
