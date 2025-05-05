import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ShipSpeedChartComponent } from './ship-speed-chart.component';

describe('ShipSpeedChartComponent', () => {
  let component: ShipSpeedChartComponent;
  let fixture: ComponentFixture<ShipSpeedChartComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ShipSpeedChartComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ShipSpeedChartComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
