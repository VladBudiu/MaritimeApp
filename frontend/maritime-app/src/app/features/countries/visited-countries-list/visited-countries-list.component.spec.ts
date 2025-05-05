import { ComponentFixture, TestBed } from '@angular/core/testing';

import { VisitedCountriesListComponent } from './visited-countries-list.component';

describe('VisitedCountriesListComponent', () => {
  let component: VisitedCountriesListComponent;
  let fixture: ComponentFixture<VisitedCountriesListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [VisitedCountriesListComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(VisitedCountriesListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
