import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ShipLocationChartComponent } from './ship-location-chart.component';

describe('ShipSpeedChartComponent', () => {
  let component: ShipLocationChartComponent;
  let fixture: ComponentFixture<ShipLocationChartComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ShipLocationChartComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ShipLocationChartComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
