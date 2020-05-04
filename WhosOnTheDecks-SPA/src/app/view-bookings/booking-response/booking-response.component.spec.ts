import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { BookingResponseComponent } from './booking-response.component';

describe('BookingResponseComponent', () => {
  let component: BookingResponseComponent;
  let fixture: ComponentFixture<BookingResponseComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ BookingResponseComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BookingResponseComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
