/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { SelectADjComponent } from './select-a-dj.component';

describe('SelectADjComponent', () => {
  let component: SelectADjComponent;
  let fixture: ComponentFixture<SelectADjComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SelectADjComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SelectADjComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
