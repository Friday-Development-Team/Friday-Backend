import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ShopcontainerComponent } from './shopcontainer.component';

describe('ShopcontainerComponent', () => {
  let component: ShopcontainerComponent;
  let fixture: ComponentFixture<ShopcontainerComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ShopcontainerComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ShopcontainerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
