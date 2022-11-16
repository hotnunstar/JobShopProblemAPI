import { TestBed } from '@angular/core/testing';

import { MaquinaEfetuaPlanoService } from './maquina-efetua-plano.service';

describe('MaquinaEfetuaPlanoService', () => {
  let service: MaquinaEfetuaPlanoService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(MaquinaEfetuaPlanoService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
