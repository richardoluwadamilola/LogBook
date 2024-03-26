import { AfterViewInit, Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { EmployeeService } from '../services/api/employees/employee.service';
import { Department } from '../services/api/models/department.model';
import { DepartmentService } from '../services/api/department/department.service';
import { Employee } from '../services/api/models/employee.model';
import { DialogService } from '../services/dialog.service';

declare var $: any;

@Component({
  selector: 'app-employee-management',
  templateUrl: './employee-management.component.html',
  styleUrls: ['./employee-management.component.css']
})
export class EmployeeManagementComponent implements OnInit, AfterViewInit {
  employeeForm!: FormGroup;
  departments: Department[] = [];
  employees: Employee[] = [];
  employeeNumber!: string;
  modalTitle: string = '';
  modalBody: string = '';


  constructor(private fb: FormBuilder, private employeeService: EmployeeService, private departmentService: DepartmentService, private dialogService: DialogService) {
    this.employeeForm = this.fb.group({
      employeeNumber: ['', Validators.required],
      firstName: ['', Validators.required],
      middleName: [''],
      lastName: ['', Validators.required],
      department: ['', Validators.required],
    });
  }

  ngOnInit(): void {
    this.getDepartments();
    this.getEmployees();
  }

  ngAfterViewInit(): void {
    $('#department').select();
  }

  getDepartments(): void {
    this.employeeService.getDepartments().subscribe(
      (data: Department[]) => {
        // Sort departments alphabetically
        this.departments = data.sort((a, b) => a.departmentName.localeCompare(b.departmentName));
        console.log('Departments:', this.departments);
      },
      (error: any) => {
        console.error('Error getting departments', error);
      }
    );
  }

  async submitForm(): Promise<void> {
    if (this.employeeForm.valid) {
      console.log('Employee details:', this.employeeForm.value);

      const employeeData = this.employeeForm.value;

      // Check if the employee number exists
      const existingEmployee = this.employees.find(e => e.employeeNumber === employeeData.employeeNumber);

      if (!existingEmployee) {
        // If employeeNumber doesn't exist, it's a new employee, call the save API
        this.employeeService.saveEmployeeDetails(employeeData).subscribe(
          async (data: any) => {
            console.log('Employee details saved successfully', data);
            this.modalTitle = 'Success';
            this.modalBody = 'Employee details saved successfully.';
            await this.dialogService.showDialog(this.modalTitle, this.modalBody);
            this.employeeForm.reset();
            this.getEmployees(); // Assuming this method retrieves the updated employee list
          },
          (error: any) => {
            console.error('Error saving employee details', error);
          }
        );
      } else {
        // If employeeNumber exists, it's an update operation
        this.employeeService.updateEmployeeDetails(employeeData).subscribe(
          async (data: any) => {
            console.log('Employee details updated successfully', data);
            this.modalTitle = 'Success';
            this.modalBody = 'Employee details updated successfully.';
            await this.dialogService.showDialog(this.modalTitle, this.modalBody);
            this.employeeForm.reset();
            this.getEmployees(); // Assuming this method retrieves the updated employee list
          },
          (error: any) => {
            console.error('Error updating employee details', error);
          }
        );
      }
    }
  }

  onModalConfirm(): void {
    $('#errorModal').modal('hide');
  }

  getEmployees(): void {
    this.employeeService.getEmployees().subscribe(
      (data: Employee[]) => {
        this.employees = data;
        console.log('Employees:', this.employees);
      },
      (error: any) => {
        console.error('Error getting employees', error);
      }
    );
  }

  deleteEmployee(employeeNumber: string): void {
    this.employeeService.deleteEmployee(employeeNumber).subscribe(
      async (data: any) => {
        console.log('Employee deleted successfully', data);
        this.modalTitle = 'Error';
      this.modalBody = 'Employee deleted successfully.';
      await this.dialogService.showDialog(this.modalTitle, this.modalBody);
        this.getEmployees();
      },
      (error: any) => {
        console.error('Error deleting employee', error);
      }
    );
  }

  editEmployee(employee: Employee): void {
    this.employeeForm.patchValue({
      employeeNumber: employee.employeeNumber,
      firstName: employee.firstName,
      middleName: employee.middleName,
      lastName: employee.lastName,
      department: employee.department,
      departmentId: employee.departmentId,
    });
  }
}
