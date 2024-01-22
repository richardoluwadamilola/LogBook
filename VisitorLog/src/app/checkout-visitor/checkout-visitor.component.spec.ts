import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CheckoutVisitorComponent } from './checkout-visitor.component';

describe('CheckoutVisitorComponent', () => {
  let component: CheckoutVisitorComponent;
  let fixture: ComponentFixture<CheckoutVisitorComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [CheckoutVisitorComponent]
    });
    fixture = TestBed.createComponent(CheckoutVisitorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
