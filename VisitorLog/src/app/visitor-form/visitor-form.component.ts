import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { FormGroup, FormBuilder, Validators, AbstractControl } from '@angular/forms';
import { VisitorService } from '../services/api/visitors/visitor.service';
import { Employee } from '../services/api/models/employee.model';
//import { ReasonForVisit } from '../services/api/models/visitor';
import { Router } from '@angular/router';
import { CameraComponent } from '../camera/camera.component';
import { ReasonforvisitService } from '../services/api/Reason/reasonforvisit.service';
import { ReasonForVisit } from '../services/api/models/reason-for-visit';
import { Department } from '../services/api/models/department.model';
import { Observable } from 'rxjs';
import { DepartmentService } from '../services/api/department/department.service';
import { error } from 'jquery';
import { DialogService } from '../services/dialog.service';

declare var $: any;

@Component({
  selector: 'app-visitor-form',
  templateUrl: './visitor-form.component.html',
  styleUrls: ['./visitor-form.component.css']
})
export class VisitorFormComponent implements OnInit, AfterViewInit {
  [x: string]: any;
  @ViewChild(CameraComponent) cameraComponent!: CameraComponent;
  visitorForm!: FormGroup;
  currentStep = 1;
  employees: Employee[] = [];
  departments: Department[] = [];
  filteredEmployees: Employee[] = [];
  reasonForVisit: ReasonForVisit[] = [];
  formSubmitted = false;
  departmentId = '';
  reasonForVisitId = '';
  modalTitle: string = '';
  modalBody: string = '';

  constructor(private fb: FormBuilder, private visitorService: VisitorService, private router: Router, private reasonForVisitService: ReasonforvisitService, private departmentService: DepartmentService, private dialogService: DialogService) { }

  ngOnInit(): void {
    this.createForm();
    this.getEmployees();
    this.getDepartments();
    this.getReasonsForVisit();
  }

  ngAfterViewInit(): void {
    $('#employeeNumber').select();
    $('#departmentId').select();
  }

  createForm(): void {
    this.visitorForm = this.fb.group({
      fullName: ['', Validators.required],
      contactAddress: ['', Validators.required],
      phoneNumber: ['', Validators.required],
      emailAddress: [''],
      personHereToSee: ['', Validators.required],
      departmentId: [null, Validators.required],
      employeeNumber: [null, Validators.required],
      reasonForVisit: [null, Validators.required],
      reasonForVisitDescription: [''],
      photo: [null, Validators.required],
    });
  }

  nextStep(): void {
    this.currentStep++;
  }

  prevStep(): void {
    this.currentStep--;
  }


  getReasonsForVisit(): void {
    this.reasonForVisitService.getReasonsForVisit().subscribe(
      (data: any) => {
        this.reasonForVisit = data;
        console.log('Reasons for visit:', this.reasonForVisit);
      },
      (error: any) => {
        console.error('Error getting reasons for visit', error);
      }
    );
  }

  getEmployees(): void {
    this.visitorService.getEmployees().subscribe(
      (data: Employee[]) => {
        //Sort employees alphabetically
        this.employees = data.sort((a, b) => a.firstName.localeCompare(b.firstName));
        console.log('Employees:', this.employees);
      },
      (error: any) => {
        console.error('Error getting employees', error);
      }
    );
  }

  //Get departments
  getDepartments(): void {
    this.departmentService.getDepartments().subscribe(
      (data: Department[]) => {
        this.departments = data.sort((a, b) => a.departmentName.localeCompare(b.departmentName));
        console.log('Departments:', this.departments);
      },
      (error: any) => {
        console.error('Error getting departments', error);
      }
    );
  }


  // Filter Employees
  async filterEmployees(): Promise<void> {
    const input = this.visitorForm.get('personHereToSee')?.value.trim().toLowerCase();

    // Check if the input contains a space
    const spaceIndex = input.indexOf(' ');

    // Check if there is a space and the term before it is not empty
    if (spaceIndex > 0) {
      const firstPart = input.substring(0, spaceIndex);
      const secondPart = input.substring(spaceIndex + 1); // Get the entered second part

      // Check if second part has at least three characters
      if (secondPart.length >= 3) {
        // Check if the second part matches either first name or last name
        const matchingEmployees = this.employees.filter(employee =>
          this.doesEmployeeMatchSearchTerm(employee, firstPart, secondPart)
        );

        if (matchingEmployees.length === 0) {
          this.modalTitle = 'Error';
          this.modalBody = 'Employee not found. Please confirm who you are here to see.';
          await this.dialogService.showDialog(this.modalTitle, this.modalBody);
        } else {
          this.filteredEmployees = matchingEmployees;
        }
      }
    } else {
      this.filteredEmployees = [];
    }
  }

  doesEmployeeMatchSearchTerm(employee: Employee, firstPart: string, secondPart: string): boolean {
    const firstNameLower = employee.firstName.toLowerCase();
    const lastNameLower = employee.lastName.toLowerCase();

    // Check if first part matches either first name or last name
    if (!(firstNameLower === firstPart || lastNameLower === firstPart)) {
      return false;
    }

    // Check if the second part matches the remaining part of the name
    return (firstNameLower === firstPart && lastNameLower.startsWith(secondPart)) ||
      (lastNameLower === firstPart && firstNameLower.startsWith(secondPart));
  }

  selectEmployee(employee: Employee): void {
    this.visitorForm.patchValue({
      employeeNumber: employee.employeeNumber,
      personHereToSee: `${employee.lastName} ${employee.middleName ? employee.middleName + ' ' : ''} ${employee.firstName}`,
    });
    this.filteredEmployees = [];
  }

  // Quit the form
  quitForm(): void {
    // Navigate to the home page
    this.router.navigate(['/home']);
  }

  handlePhotoCapture(photoData: string): void {
    this.visitorForm.patchValue({
      photo: photoData,
    });
  }

  async submitForm(): Promise<void> {
    console.log('Form data:', this.visitorForm.value);
    //debugger;
    if (this.visitorForm.valid) {
      const formData = this.visitorForm.value;
      const selectedDepartmentId = parseInt(formData.departmentId, 10);
      const employee = this.employees.find(emp => emp.employeeNumber === formData.employeeNumber);

      if (employee) {
        const employeeDepartmentId = employee.departmentId;

        if (selectedDepartmentId !== employeeDepartmentId) {
          console.error('Selected department does not match the employee\'s department');
          this.modalTitle = 'Error';
          this.modalBody = 'Selected department does not match the employee\'s department';
          await this.dialogService.showDialog(this.modalTitle, this.modalBody);
          return; // Abort form submission
        }
      }

      this.saveVisitorDetails(formData);
    } else {
      console.error('Invalid form data');
      this.modalTitle = 'Error';
      this.modalBody = 'Please correct the following errors:';
      await this.dialogService.showDialog(this.modalTitle, this.modalBody);
      Object.keys(this.visitorForm.controls).forEach(field => {
        const control = this.visitorForm.get(field);
        if (control && control.invalid) {
          alert(`- ${field.toUpperCase()}: ${this.getErrorMessage(control)}`);
        }
      });
      console.log('Form data:', this.visitorForm.value);
      console.log('Form errors:', this.visitorForm.errors);
      return; // Abort form submission
    }
  }

  onModalConfirm(): void {
    $('#errorModal').modal('hide');
  }

  getErrorMessage(control: AbstractControl): string {
    if (control.errors && control.errors['required']) {
      return 'This field is required.';
    } else if (control.errors && control.errors['pattern']) {
      return 'Please enter a valid value.';
    } // Add more error messages as needed

    return ''; // Default error message
  }



  async saveVisitorDetails(formData: any): Promise<void> {
    this.visitorService.saveVisitorDetails(formData).subscribe(
      async (response: any) => {
        console.log('Visitor details saved successfully', response);
        if (response && response.hasError) {
          console.error('Error saving visitor details:', response.description);
          this.modalTitle = 'Error';
          this.modalBody = 'Error', response.description;
          await this.dialogService.showDialog(this.modalTitle, this.modalBody);
        } else {
          console.log('Visitor details saved successfully');
          this.modalTitle = 'Success';
          this.modalBody = 'Visitor details saved successfully, please proceed to the security personnel to get a tag.';
          await this.dialogService.showDialog(this.modalTitle, this.modalBody);
          this.formSubmitted = true;
          setTimeout(() => {
            this.formSubmitted = false;
            this.visitorForm.reset();
            if (this.cameraComponent) {
              this.cameraComponent.resetCamera();
            }
            this.router.navigate(['/home']);
          }, 3000);
        }
      },
      (error: any) => {
        console.error('Error saving visitor details', error);
      }
    );
  }



}

