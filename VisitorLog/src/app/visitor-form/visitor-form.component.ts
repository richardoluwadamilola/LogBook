import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { VisitorService } from '../services/api/visitors/visitor.service';
import { Employee } from '../services/api/models/employee.model';
//import { ReasonForVisit } from '../services/api/models/visitor';
import { Router } from '@angular/router';
import { CameraComponent } from '../camera/camera.component';
import { ReasonforvisitService } from '../services/api/Reason/reasonforvisit.service';
import { ReasonForVisit } from '../services/api/models/reason-for-visit';
import { Department } from '../services/api/models/department.model';
import { Observable } from 'rxjs';

declare var $: any;

@Component({
  selector: 'app-visitor-form',
  templateUrl: './visitor-form.component.html',
  styleUrls: ['./visitor-form.component.css']
})
export class VisitorFormComponent implements OnInit, AfterViewInit {
  @ViewChild(CameraComponent) cameraComponent!: CameraComponent;
  visitorForm!: FormGroup;
  employees: Employee[] = [];
  departments: Department[] = [];
  filteredEmployees: Employee[] = [];
  filteredDepartments: Department[] = [];
  reasonForVisit: ReasonForVisit[] = [];
  formSubmitted = false;



  constructor(private fb: FormBuilder, private visitorService: VisitorService, private router: Router, private reasonForVisitService: ReasonforvisitService) { }

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
      department: ['', Validators.required],
      departmentId: [null, Validators.required],
      //departmentName: ['', Validators.required],
      employeeNumber: [null, Validators.required],
      reasonForVisit: [null, Validators.required],
      reasonForVisitDescription: [''],
      photo: [null, Validators.required],
    });
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
    this.visitorService.getDepartments().subscribe(
      (data: Department[]) => {
        this.departments = data;
        console.log('Departments:', this.departments);
      },
      (error: any) => {
        console.error('Error getting departments', error);
      }
    );
  }

  // Filter employees
  filterEmployees(): void {
    const searchTerm = this.visitorForm.get('personHereToSee')?.value.trim().toLowerCase();
    
    if (searchTerm.length >= 2) { // At least 2 characters for search
      this.filteredEmployees = this.employees.filter(employee =>
        this.doesEmployeeMatchSearchTerm(employee, searchTerm)
      );
    } else {
      this.filteredEmployees = [];
    }
  }
  
  doesEmployeeMatchSearchTerm(employee: Employee, searchTerm: string): boolean {
    const lastName = employee.lastName.toLowerCase();
    return lastName.includes(searchTerm);
  }

  
  selectEmployee(employee: Employee): void {
    this.visitorForm.patchValue({
      employeeNumber: employee.employeeNumber,
      personHereToSee: `${employee.firstName} ${employee.middleName ? employee.middleName + ' ' : ''}${employee.lastName}`,
    });
    this.filteredEmployees = []; // Clear suggestions
  }

  // Filter departments
  filterDepartments(): void {
    const searchTerm = this.visitorForm.get('department')?.value.toLowerCase();
    if (searchTerm.length >= 3) { // Check if at least 3 characters are entered
      this.filteredDepartments = this.departments.filter(department =>
        department.departmentName.toLowerCase().includes(searchTerm)
      );
    } else {
      this.filteredDepartments = []; 
    }
  }
  selectDepartment(department: Department): void {
    this.visitorForm.patchValue({
      departmentId: department.departmentId,
      department: department.departmentName,
    });
    this.filteredDepartments = []; // Clear suggestions
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
      const selectedDepartmentId = formData.departmentId;
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
      return; // Abort form submission
    }
  }
  

  saveVisitorDetails(formData: any): void {
    this.visitorService.saveVisitorDetails(formData).subscribe(
      (response: any) => {
        console.log('Visitor details saved successfully', response);
        if (response && response.hasError) {
          console.error('Error saving visitor details:', response.description);
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
  





  // // Get the employee name initials
  // getInitials(name: string): string {
  //   if (!name) {
  //     return '';
  //   }
  //   return name.charAt(0) + '.';
  // }


}

