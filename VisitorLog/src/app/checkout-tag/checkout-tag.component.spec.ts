import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CheckoutTagComponent } from './checkout-tag.component';

describe('CheckoutTagComponent', () => {
  let component: CheckoutTagComponent;
  let fixture: ComponentFixture<CheckoutTagComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [CheckoutTagComponent]
    });
    fixture = TestBed.createComponent(CheckoutTagComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
