import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CheckOutTagComponent } from './check-out-tag.component';

describe('CheckOutTagComponent', () => {
  let component: CheckOutTagComponent;
  let fixture: ComponentFixture<CheckOutTagComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [CheckOutTagComponent]
    });
    fixture = TestBed.createComponent(CheckOutTagComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
