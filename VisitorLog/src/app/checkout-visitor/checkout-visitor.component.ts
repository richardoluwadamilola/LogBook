import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { TagService } from '../services/api/tags/tag.service';

@Component({
  selector: 'app-checkout-visitor',
  templateUrl: './checkout-visitor.component.html',
  styleUrls: ['./checkout-visitor.component.css']
})
export class CheckoutVisitorComponent implements OnInit{
  checkoutForm!: FormGroup;
  errorMessage: string | null = null;
  successMessage: string | null = null;

  constructor(private fb: FormBuilder, private tagService: TagService) { }

  ngOnInit(): void {
    this.initform();
  }

  initform(): void {
    this.checkoutForm = this.fb.group({
      tagNumber: ['', Validators.required],
      visitorId: [0, Validators.required]
    });
  }

  checkoutVisitor(): void {
    if (this.checkoutForm.valid) {
      const checkoutTagDto = this.checkoutForm.value;
      this.tagService.checkOutVisitor(checkoutTagDto).subscribe(
        (data: any) => {
          console.log('Visitor checked out successfully', data);

          if (data.error) {
            this.errorMessage = data.description;
            this.successMessage = null;
            return;
          } else {
            this.successMessage = data.description;
            this.errorMessage = null;
            this.checkoutForm.reset();
          }
        },
        (error: any) => {
          console.error('Error checking out Visitor', error);
          this.errorMessage = 'An unexpected error occurred';
          this.successMessage = null;
        }
      );
    }
  }

}
