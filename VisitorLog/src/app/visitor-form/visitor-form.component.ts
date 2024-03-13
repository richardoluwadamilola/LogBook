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

declare var $: any;

@Component({
  selector: 'app-visitor-form',
  templateUrl: './visitor-form.component.html',
  styleUrls: ['./visitor-form.component.css']
})
export class VisitorFormComponent implements OnInit, AfterViewInit {
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

  constructor(private fb: FormBuilder, private visitorService: VisitorService, private router: Router, private reasonForVisitService: ReasonforvisitService, private departmentService: DepartmentService) { }

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
  filterEmployees(): void {
    const input = this.visitorForm.get('personHereToSee')?.value.trim().toLowerCase();
  
    // Check if the input contains a space
    const spaceIndex = input.indexOf(' ');
  
    // Check if there is a space and the term before it is not empty
    if (spaceIndex > 0) {
      const lastName = input.substring(0, spaceIndex);
      const firstNamePrefix = input.substring(spaceIndex + 1); // Get the entered first name prefix

      // Check if at least four characters are entered after the space
      if (firstNamePrefix.length >= 4) {
        // Filter employees based on last name and first name
        const matchingEmployees = this.employees.filter(employee =>
          this.doesEmployeeMatchSearchTerm(employee, lastName, firstNamePrefix.charAt(0))
        );
    
        // Display error message if no matching employees found
        if (matchingEmployees.length === 0) {
          alert('Employee not found. Please confirm who you are here to see.');
        } else {
          // If matching employees found, assign them to filteredEmployees
          this.filteredEmployees = matchingEmployees;
        }
      }
    } else {
      // If no space found, clear filteredEmployees
      this.filteredEmployees = [];
    }
}

doesEmployeeMatchSearchTerm(employee: Employee, lastName: string, firstNameFirstLetter: string): boolean {
    // Check if the last name matches
    if (employee.lastName.toLowerCase() !== lastName) {
        return false;
    }

    // Check if the first letter of the first name matches
    return employee.firstName.toLowerCase().startsWith(firstNameFirstLetter);
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

  submitForm(): void {
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
          alert('The person you are here to see is not in the department specified. Please confirm the department inputted.');
          return; // Abort form submission
        }
      }
  
      this.saveVisitorDetails(formData);
    } else {
      console.error('Invalid form data');
      alert('Please correct the following errors:');
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
  
  getErrorMessage(control: AbstractControl): string {
    if (control.errors && control.errors['required']) {
      return 'This field is required.';
    } else if (control.errors && control.errors['pattern']) {
      return 'Please enter a valid value.';
    } // Add more error messages as needed

    return ''; // Default error message
  }
  


  saveVisitorDetails(formData: any): void {
    this.visitorService.saveVisitorDetails(formData).subscribe(
      (response: any) => {
        console.log('Visitor details saved successfully', response);
        if (response && response.hasError) {
          console.error('Error saving visitor details:', response.description);
          alert('Error saving visitor details. Please fill the right details');
        } else {
          console.log('Visitor details saved successfully');
          alert('Visitor details saved successfully, please proceed to get a tag.');
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

