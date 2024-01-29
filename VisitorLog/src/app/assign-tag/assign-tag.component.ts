import { Component } from '@angular/core';
import { TagService } from '../services/api/tags/tag.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { VisitorService } from '../services/api/visitors/visitor.service';
import { ReactiveFormsModule } from '@angular/forms';

@Component({
  selector: 'app-assign-tag',
  templateUrl: './assign-tag.component.html',
  styleUrls: ['./assign-tag.component.css']
})
export class AssignTagComponent {
  tagAssignmentForm!: FormGroup;
  errorMessage : string | null = null;
  successMessage : string | null = null;
  visitors: any[] = [];

  constructor(private fb: FormBuilder, private tagService: TagService, private visitorService: VisitorService) { }

  ngOnInit(): void {
    this.initForm();
    this.loadVisitors();
  }

  initForm(): void {
    this.tagAssignmentForm = this.fb.group({
      tagNumber: ['', Validators.required],
      visitorId: [0, Validators.required]
    });
  }

  assignTagToVisitor(): void {
    if (this.tagAssignmentForm.valid) {
      const assignTagDto = this.tagAssignmentForm.value;
      this.tagService.assignTagToVisitor(assignTagDto).subscribe(
        (data: any) => {
          console.log('Tag assigned successfully', data);

          if (data.error) {
            this.errorMessage = data.description;
            this.successMessage = null;
            return;
          } else {
            this.successMessage = data.description;
            this.errorMessage = null;
            this.tagAssignmentForm.reset();
          }
        },
        (error: any) => {
          console.error('Error assigning tag', error);
          this.errorMessage = 'An unexpected error occurred';
          this.successMessage = null;
        }
      );
    }
  }

  loadVisitors(): void {
    this.visitorService.getVisitors().subscribe(
      (data: any[]) => {
        this.visitors = data;
      },
      (error: any) => {
        console.error('Error fetching visitors', error);
      }
    );
  }
}
