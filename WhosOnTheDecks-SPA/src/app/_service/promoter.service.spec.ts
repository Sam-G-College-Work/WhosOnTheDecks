/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { PromoterService } from './promoter.service';

describe('Service: Promoter', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [PromoterService]
    });
  });

  it('should ...', inject([PromoterService], (service: PromoterService) => {
    expect(service).toBeTruthy();
  }));
});
