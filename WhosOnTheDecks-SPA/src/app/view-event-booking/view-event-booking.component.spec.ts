import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ViewEventBookingComponent } from './view-event-booking.component';

describe('ViewEventBookingComponent', () => {
  let component: ViewEventBookingComponent;
  let fixture: ComponentFixture<ViewEventBookingComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ViewEventBookingComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ViewEventBookingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
