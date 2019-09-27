import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CateringtoolsComponent } from './cateringtools.component';

describe('CateringtoolsComponent', () => {
  let component: CateringtoolsComponent;
  let fixture: ComponentFixture<CateringtoolsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CateringtoolsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CateringtoolsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
