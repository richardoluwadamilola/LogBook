import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { VisitorService } from '../services/api/visitors/visitor.service';
import { Employee } from '../services/api/models/employee.model';
import { ReasonForVisit } from '../services/api/models/visitor';
import { Router } from '@angular/router';
import { CameraComponent } from '../camera/camera.component';

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
  //takenPicture: string | null = null;
  formSubmitted = false;
  // isTakingPicture = false;
  // isRetakeMode = false;


  constructor(private fb: FormBuilder, private visitorService: VisitorService, private router: Router) { }

  ngOnInit(): void {
    this.createForm();
    this.getEmployees();
  }

  ngAfterViewInit(): void {
    $('#employeeNumber').select();
  }

  createForm(): void {
    this.visitorForm = this.fb.group({
      fullName: ['', Validators.required],
      contactAddress: ['', Validators.required],
      phoneNumber: ['', Validators.required],
      employeeNumber: [null, Validators.required],
      //personHereToSee: ['', Validators.required],
      reasonForVisit: [null, Validators.required],
      reasonForVisitDescription: [''],
      photo: [null, Validators.required],
    });
  }

  reasons = [
    { label: 'Official', value: ReasonForVisit.Official },
    { label: 'Personal', value: ReasonForVisit.Personal }
  ];

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
  
  // Submit the form
  submitForm(): void {
    console.log('Form data:', this.visitorForm.value);
    if (this.visitorForm.valid) {
      const formData = this.visitorForm.value;
      
      // Log formData before modifying the employeeNumber
      console.log('Original FormData:', formData);
  
      // Log the employeeNumber separately
      console.log('Form Control Value (employeeNumber):', this.visitorForm.get('employeeNumber')?.value);
  
      // Ensure employeeNumber is not null
      if (formData.employeeNumber !== null) {
        // You don't need to convert it to a number
      } else {
        console.error('Invalid employeeNumber:', formData.employeeNumber);
        return;
      }
  
      // Log formData after modifying the employeeNumber
      console.log('Modified FormData:', formData);

      // Call a service to save visitor details
    this.visitorService.saveVisitorDetails(formData).subscribe(
      (response: any) => {
        console.log('Visitor details saved successfully', response);
        // Check the response for success or error
        if (response && response.hasError) {
          console.error('Error saving visitor details:', response.description);
        } else {
          console.log('Visitor details saved successfully');
          alert('Visitor details saved successfully, please proceed to get a tag.');
          // Set the formSubmitted flag to true
          this.formSubmitted = true;
          // Reset the form and the camera component after a short delay
          setTimeout(() => {
            this.formSubmitted = false;
            this.visitorForm.reset();
            if (this.cameraComponent) {
              this.cameraComponent.resetCamera();
            }
            // Navigate to the home page
            this.router.navigate(['/home']);
          }, 3000); 
        }
      },
      (error: any) => {
        console.error('Error saving visitor details', error);
      }
    );
  } else {
    console.error('Invalid form data');
  }
}
}

