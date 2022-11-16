import { TestBed } from '@angular/core/testing';

import { InserirDadosService } from './inserir-dados.service';

describe('InserirDadosService', () => {
  let service: InserirDadosService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(InserirDadosService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
