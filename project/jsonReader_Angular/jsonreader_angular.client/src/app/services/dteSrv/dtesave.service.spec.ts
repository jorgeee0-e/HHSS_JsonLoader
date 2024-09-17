import { TestBed } from '@angular/core/testing';

import { DTESaveService } from './dtesave.service';

describe('DTESaveService', () => {
  let service: DTESaveService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(DTESaveService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
