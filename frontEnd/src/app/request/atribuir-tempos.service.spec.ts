import { TestBed } from '@angular/core/testing';

import { AtribuirTemposService } from './atribuir-tempos.service';

describe('AtribuirTemposService', () => {
  let service: AtribuirTemposService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(AtribuirTemposService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
