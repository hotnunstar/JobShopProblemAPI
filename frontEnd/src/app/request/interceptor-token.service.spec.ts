import { TestBed } from '@angular/core/testing';

import { InterceptorTokenService } from './interceptor-token.service';

describe('InterceptorTokenService', () => {
  let service: InterceptorTokenService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(InterceptorTokenService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
