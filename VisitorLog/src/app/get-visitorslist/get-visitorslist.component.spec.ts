import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GetVisitorslistComponent } from './get-visitorslist.component';

describe('GetVisitorslistComponent', () => {
  let component: GetVisitorslistComponent;
  let fixture: ComponentFixture<GetVisitorslistComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [GetVisitorslistComponent]
    });
    fixture = TestBed.createComponent(GetVisitorslistComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
