import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PendingCheckinsComponent } from './pending-checkins.component';

describe('PendingCheckinsComponent', () => {
  let component: PendingCheckinsComponent;
  let fixture: ComponentFixture<PendingCheckinsComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [PendingCheckinsComponent]
    });
    fixture = TestBed.createComponent(PendingCheckinsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
