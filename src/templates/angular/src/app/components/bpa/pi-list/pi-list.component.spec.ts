import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PiListComponent } from './pi-list.component';

describe('PiListComponent', () => {
  let component: PiListComponent;
  let fixture: ComponentFixture<PiListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PiListComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PiListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
