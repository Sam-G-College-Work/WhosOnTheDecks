import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ViewBookedDjComponent } from './view-booked-dj.component';

describe('ViewBookedDjComponent', () => {
  let component: ViewBookedDjComponent;
  let fixture: ComponentFixture<ViewBookedDjComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ViewBookedDjComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ViewBookedDjComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
