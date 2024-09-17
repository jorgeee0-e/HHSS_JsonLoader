import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DTEComponent } from './dte.component';

describe('DTEComponent', () => {
  let component: DTEComponent;
  let fixture: ComponentFixture<DTEComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [DTEComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DTEComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
