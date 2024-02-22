import { TestBed } from '@angular/core/testing';

import { ReasonforvisitService } from './reasonforvisit.service';

describe('ReasonforvisitService', () => {
  let service: ReasonforvisitService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ReasonforvisitService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
