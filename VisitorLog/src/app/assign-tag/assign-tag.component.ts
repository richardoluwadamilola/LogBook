import { Component } from '@angular/core';
import { TagService } from '../services/api/tags/tag.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-assign-tag',
  templateUrl: './assign-tag.component.html',
  styleUrls: ['./assign-tag.component.css']
})
export class AssignTagComponent {
  tagAssignmentForm!: FormGroup;
  //assignTagDto: any = {};

  constructor(private fb: FormBuilder, private tagService: TagService) { }

  ngOnInit(): void {
    this.initForm();
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
          this.tagAssignmentForm.reset();
        },
        (error: any) => {
          console.error('Error assigning tag', error);
        }
      );
    }
  }
}
