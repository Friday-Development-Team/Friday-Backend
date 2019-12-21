import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AdjustUserComponent } from './adjust-user.component';

describe('AdjustUserComponent', () => {
  let component: AdjustUserComponent;
  let fixture: ComponentFixture<AdjustUserComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AdjustUserComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AdjustUserComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
