/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { AlertlyService } from './alertly.service';

describe('Service: Alertly', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [AlertlyService]
    });
  });

  it('should ...', inject([AlertlyService], (service: AlertlyService) => {
    expect(service).toBeTruthy();
  }));
});
