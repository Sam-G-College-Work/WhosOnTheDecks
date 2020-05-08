/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { CreateEventService } from './create-event.service';

describe('Service: CreateEvent', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [CreateEventService]
    });
  });

  it('should ...', inject([CreateEventService], (service: CreateEventService) => {
    expect(service).toBeTruthy();
  }));
});
