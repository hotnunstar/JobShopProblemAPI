import { TestBed } from '@angular/core/testing';

import { GestaoUtilizadoresService } from './gestao-utilizadores.service';

describe('GestaoUtilizadoresService', () => {
  let service: GestaoUtilizadoresService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(GestaoUtilizadoresService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
