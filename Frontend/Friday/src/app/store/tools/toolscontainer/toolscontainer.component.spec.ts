import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ToolscontainerComponent } from './toolscontainer.component';

describe('ToolscontainerComponent', () => {
  let component: ToolscontainerComponent;
  let fixture: ComponentFixture<ToolscontainerComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ToolscontainerComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ToolscontainerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
