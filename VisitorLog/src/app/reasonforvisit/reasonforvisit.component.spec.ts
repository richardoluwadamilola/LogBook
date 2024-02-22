import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ReasonforvisitComponent } from './reasonforvisit.component';

describe('ReasonforvisitComponent', () => {
  let component: ReasonforvisitComponent;
  let fixture: ComponentFixture<ReasonforvisitComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ReasonforvisitComponent]
    });
    fixture = TestBed.createComponent(ReasonforvisitComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
