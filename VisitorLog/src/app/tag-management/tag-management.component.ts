


import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Tag } from '../services/api/models/tag';
import { TagService } from '../services/api/tags/tag.service';

@Component({
  selector: 'app-tag-management',
  templateUrl: './tag-management.component.html',
  styleUrls: ['./tag-management.component.css']
})
export class TagManagementComponent implements OnInit {
  tagForm: FormGroup;
  tags: Tag[] = [];
  tagNumber!: string;

  constructor(private fb: FormBuilder, private tagService: TagService) {
    this.tagForm = this.fb.group({
      tagNumber: ['', Validators.required],
      isAvailable: [true, Validators.required],
      isDisabled: [false, Validators.required]
    });
  }

  ngOnInit(): void {
    this.getTags();
  }

  createTag(): void {
    const tag: Tag = {
      tagNumber: this.tagForm.value.tagNumber,
      isAvailable: this.tagForm.value.isAvailable,
      isDisabled: this.tagForm.value.isDisabled
    };

    this.tagService.createTag(tag).subscribe(
      (data: any) => {
        console.log('Tag created successfully', data);
        this.tagForm.reset();
        this.getTags();
      },
      (error: any) => {
        console.error('Error creating tag', error);
      }
    );
  }

  disableTag(tagNumber: string): void {
    this.tagService.disableTag(tagNumber).subscribe(
      (data: any) => {
        console.log('Tag disabled successfully', data);
        alert('Tag disabled successfully');
        this.getTags();
      },
      (error: any) => {
        console.error('Error disabling tag', error);
      }
    );
  }

  getTags(): void {
    this.tagService.getTags().subscribe(
      (data: Tag[]) => {
        this.tags = data;
        console.log('Tags:', data);
      },
      (error: any) => {
        console.error('Error getting tags', error);
      }
    );
  }

  isDisabled(tag: Tag): boolean {
    return tag.isDisabled;
  }

  

}
