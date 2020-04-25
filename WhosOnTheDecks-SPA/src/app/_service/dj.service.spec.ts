/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { DjService } from './dj.service';

describe('Service: Dj', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [DjService]
    });
  });

  it('should ...', inject([DjService], (service: DjService) => {
    expect(service).toBeTruthy();
  }));
});
