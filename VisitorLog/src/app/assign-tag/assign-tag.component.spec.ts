import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AssignTagComponent } from './assign-tag.component';

describe('AssignTagComponent', () => {
  let component: AssignTagComponent;
  let fixture: ComponentFixture<AssignTagComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [AssignTagComponent]
    });
    fixture = TestBed.createComponent(AssignTagComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
