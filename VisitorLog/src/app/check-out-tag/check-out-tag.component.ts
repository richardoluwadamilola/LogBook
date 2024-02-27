import { DatePipe } from '@angular/common';
import { Visitor } from '@angular/compiler';
import { Component, OnInit } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../services/api/Auth/auth.service';
import { VisitorService } from '../services/api/visitors/visitor.service';
import { TagService } from '../services/api/tags/tag.service';
import { FormBuilder } from '@angular/forms';

@Component({
  selector: 'app-check-out-tag',
  templateUrl: './check-out-tag.component.html',
  styleUrls: ['./check-out-tag.component.css']
})
export class CheckOutTagComponent  implements OnInit {
  // ...

  constructor(private fb: FormBuilder, private tagService: TagService, private visitorService: VisitorService, private authService: AuthService, private router: Router, private datepipe: DatePipe) { }
  
  
  
  ngOnInit(): void {
    throw new Error('Method not implemented.');
  }

}
