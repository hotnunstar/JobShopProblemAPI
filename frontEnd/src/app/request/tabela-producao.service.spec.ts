import { TestBed } from '@angular/core/testing';

import { TabelaProducaoService } from './tabela-producao.service';

describe('TabelaProducaoService', () => {
  let service: TabelaProducaoService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(TabelaProducaoService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
