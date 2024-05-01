import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CarCreateComponent } from './car-create.component';

describe('CarCreateComponent', () => {
  let component: CarCreateComponent;
  let fixture: ComponentFixture<CarCreateComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CarCreateComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(CarCreateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
