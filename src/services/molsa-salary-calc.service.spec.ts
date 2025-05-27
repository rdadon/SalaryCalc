import { TestBed } from '@angular/core/testing';

import { MolsaSalaryCalcService } from './molsa-salary-calc.service';

describe('MolsaSalaryCalcService', () => {
  let service: MolsaSalaryCalcService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(MolsaSalaryCalcService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
