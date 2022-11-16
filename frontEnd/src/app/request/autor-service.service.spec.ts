import { TestBed } from '@angular/core/testing';

import { AutorServiceService } from './autor-service.service';

describe('AutorServiceService', () => {
  let service: AutorServiceService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(AutorServiceService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
