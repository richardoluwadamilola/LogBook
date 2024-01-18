import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { VisitorService } from '../services/api/visitors/visitor.service';
import { Employee } from '../services/api/models/employee.model';
import { ReasonForVisit } from '../services/api/models/visitor';

@Component({
  selector: 'app-visitor-form',
  templateUrl: './visitor-form.component.html',
  styleUrls: ['./visitor-form.component.css']
})
export class VisitorFormComponent implements OnInit {
  visitorForm!: FormGroup;
  employees: Employee[] = [];
  takenPicture: string | null = null;
  formSubmitted = false;

  constructor(private fb: FormBuilder, private visitorService: VisitorService) { }

  ngOnInit(): void {
    this.createForm();
    this.getEmployees();
  }

  createForm(): void {
    this.visitorForm = this.fb.group({
      firstName: ['', Validators.required],
      middleName: [''],
      lastName: ['', Validators.required],
      contactAddress: ['', Validators.required],
      phoneNumber: ['', Validators.required],
      employeeNumber: [null, Validators.required],
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
        this.employees = data;
        console.log('Employees:', this.employees);
      },
      (error: any) => {
        console.error('Error getting employees', error);
      }
    );
  }  

  takePicture(): void {
    // Access the device camera and trigger the picture taking process
    const videoOptions = { facingMode: 'user', width: 320, height: 240 };
    navigator.mediaDevices.getUserMedia({ video: videoOptions })
      .then((stream) => {
        const video = document.createElement('video');
        document.body.appendChild(video);
        video.srcObject = stream;
        video.play();

        // Capture a frame as an image
        setTimeout(() => {
          const canvas = document.createElement('canvas');
          canvas.width = video.videoWidth;
          canvas.height = video.videoHeight;
          const context = canvas.getContext('2d');
          context?.drawImage(video, 0, 0, canvas.width, canvas.height);

          // Convert the canvas image to a data URL
          this.takenPicture = canvas.toDataURL('image/png');

          // Set the captured photo in the form control
          this.visitorForm.get('photo')?.setValue(this.takenPicture);

          // Stop the camera stream and remove the video element
          stream.getTracks().forEach(track => track.stop());
          document.body.removeChild(video);
        }, 1000); // Adjust the timeout based on your needs
      })
      .catch((error) => {
        console.error('Error accessing the camera:', error);
      });
  }

  retakePicture(): void {
    // Reset the takenPicture variable to allow retaking a new picture
    this.takenPicture = null;
    // Call the takePicture method to start the picture-taking process again
    this.takePicture();
  }
  

  submitForm(): void {
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
          // Set the formSubmitted flag to true
          this.formSubmitted = true;
          
          // Delay the form reset for 5 seconds (adjust the time based on your preference)
          setTimeout(() => {
            this.formSubmitted = false;
            this.visitorForm.reset();
            this.takenPicture = null;
          }, 5000);
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

