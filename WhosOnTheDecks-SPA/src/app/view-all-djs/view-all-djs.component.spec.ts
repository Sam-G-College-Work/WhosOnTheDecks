import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ViewAllDjsComponent } from './view-all-djs.component';

describe('ViewAllDjsComponent', () => {
  let component: ViewAllDjsComponent;
  let fixture: ComponentFixture<ViewAllDjsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ViewAllDjsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ViewAllDjsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
