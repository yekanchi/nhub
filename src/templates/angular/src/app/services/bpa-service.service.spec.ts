import { TestBed } from '@angular/core/testing';

import { bpaInstanceService } from './bpaService/bpa-service.service';

describe('BpaServiceService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: bpaInstanceService = TestBed.get(bpaInstanceService);
    expect(service).toBeTruthy();
  });
});
