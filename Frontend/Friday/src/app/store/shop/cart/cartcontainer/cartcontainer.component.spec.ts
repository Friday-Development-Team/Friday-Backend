import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CartcontainerComponent } from './cartcontainer.component';

describe('CartcontainerComponent', () => {
  let component: CartcontainerComponent;
  let fixture: ComponentFixture<CartcontainerComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CartcontainerComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CartcontainerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
