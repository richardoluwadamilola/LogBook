import { Component, OnInit } from '@angular/core';
import { ReasonforvisitService } from '../services/api/Reason/reasonforvisit.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ReasonForVisit } from '../services/api/models/reason-for-visit';
import { DialogService } from '../services/dialog.service';

@Component({
  selector: 'app-reasonforvisit',
  templateUrl: './reasonforvisit.component.html',
  styleUrls: ['./reasonforvisit.component.css']
})
export class ReasonforvisitComponent implements OnInit {
  reasonForm!: FormGroup;
  reasons: ReasonForVisit[] = [];
  modalTitle: string = '';
  modalBody: string = '';


  constructor(private formBuilder: FormBuilder, private reasonForVisitService: ReasonforvisitService, private dialogService: DialogService) { }

  ngOnInit(): void {
    this.reasonForm = this.formBuilder.group({
      reason: ['', Validators.required],
      description: ['', Validators.required]
    });

    // Call the method to fetch reasons for visit when the component initializes
    this.getReasonsForVisit();
  }

  submitForm(): void {
    console.log('Form Submitted:', this.reasonForm.value);
    const formData = this.reasonForm.value;
    this.reasonForVisitService.createReasonForVisit(formData).subscribe(
      async (data: any) => {
        console.log('Reason for visit created successfully', data);
        this.modalTitle = 'Success';
      this.modalBody = 'Reason for visit created successfully.';
      await this.dialogService.showDialog(this.modalTitle, this.modalBody);
        this.reasonForm.reset();
        // After successfully creating the reason, refresh the list of reasons
        this.getReasonsForVisit();
      },
      (error: any) => {
        console.error('Error creating reason for visit', error);
      }
    );
  }

  getReasonsForVisit(): void {
    this.reasonForVisitService.getReasonsForVisit().subscribe(
      (data: any) => {
        this.reasons = data;
        console.log('Reasons for visit:', this.reasons);
      },
      (error: any) => {
        console.error('Error getting reasons for visit', error);
      }
    );
  }

  onModalConfirm(): void {
    $('#errorModal').modal('hide');
  }
}


