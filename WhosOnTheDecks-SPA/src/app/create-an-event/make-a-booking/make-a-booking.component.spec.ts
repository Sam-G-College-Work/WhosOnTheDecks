/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { MakeABookingComponent } from './make-a-booking.component';

describe('MakeABookingComponent', () => {
  let component: MakeABookingComponent;
  let fixture: ComponentFixture<MakeABookingComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MakeABookingComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MakeABookingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
