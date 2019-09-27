import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { FilterscontainerComponent } from './filterscontainer.component';

describe('FilterscontainerComponent', () => {
  let component: FilterscontainerComponent;
  let fixture: ComponentFixture<FilterscontainerComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ FilterscontainerComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(FilterscontainerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
