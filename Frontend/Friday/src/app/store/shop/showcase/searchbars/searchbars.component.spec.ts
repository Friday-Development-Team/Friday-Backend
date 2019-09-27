import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SearchbarsComponent } from './searchbars.component';

describe('SearchbarsComponent', () => {
  let component: SearchbarsComponent;
  let fixture: ComponentFixture<SearchbarsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SearchbarsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SearchbarsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
