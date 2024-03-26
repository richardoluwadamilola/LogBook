import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MessagesmodalComponent } from './messagesmodal.component';

describe('MessagesmodalComponent', () => {
  let component: MessagesmodalComponent;
  let fixture: ComponentFixture<MessagesmodalComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [MessagesmodalComponent]
    });
    fixture = TestBed.createComponent(MessagesmodalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
