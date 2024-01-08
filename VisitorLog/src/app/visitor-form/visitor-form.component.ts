import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { VisitorService } from '../services/api/visitors/visitor.service';
import { Employee } from '../services/api/models/employee.model';

@Component({
  selector: 'app-visitor-form',
  templateUrl: './visitor-form.component.html',
  styleUrls: ['./visitor-form.component.css']
})
export class VisitorFormComponent implements OnInit {
  visitorForm!: FormGroup;
  employees: Employee[] = [];

  constructor(private fb: FormBuilder, private visitorService: VisitorService) { }

  ngOnInit(): void {
    this.createForm();
    this.getEmployees();
  }

  createForm(): void {
    this.visitorForm = this.fb.group({
      firstName: ['', Validators.required],
      middlename: ['', Validators.required],
      lastName: ['', Validators.required],
      contactaddress: ['', Validators.required],
      phonenumber: ['', Validators.required],
      personheretosee: ['', Validators.required],
      reasonforvisit: ['', Validators.required],
      reasonforvisitdescription: [''],
      photo: [null, Validators.required],
    });
  }

  getEmployees(): void {
    this.visitorService.getEmployees().subscribe(
      (data: any[]) => {
        this.employees = data;
      },
      (error: any) => { // Explicitly specify the type of 'error' parameter as 'any'
        console.error('Error getting employees', error);
      }
    );
  }

  handlePhotoUpload(event: any): void {
    const file = (event.target as HTMLInputElement).files?.[0];
    if (file) {
      // Convert the file to a data URL for preview
      const reader = new FileReader();
      reader.readAsDataURL(file);
      reader.onload = () => {
        this.visitorForm.patchValue({
          photo: reader.result as string
        });
      };
    }
  }

  submitForm(): void {
    if (this.visitorForm.valid) {
      const formData = this.visitorForm.value;
      // Call a service to save visitor details (to be implemented in VisitorService)
      this.visitorService.saveVisitorDetails(formData).subscribe(
        (response: any) => { // Explicitly specify the type of 'response' parameter as 'any'
          console.log('Visitor details saved successfully', response);
          // Reset the form after successful submission
          this.visitorForm.reset();
        },
        (error: any) => { // Explicitly specify the type of 'error' parameter as 'any'
          console.error('Error saving visitor details', error);
        }
      );
    }
  }
}

