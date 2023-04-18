import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AutomationContainerComponent } from './automation-container.component';

describe('AutomationContainerComponent', () => {
  let component: AutomationContainerComponent;
  let fixture: ComponentFixture<AutomationContainerComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AutomationContainerComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AutomationContainerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
