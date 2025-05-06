import { ComponentFixture, TestBed } from '@angular/core/testing';

import { VisitedCountryListComponent } from './visited-countries-list.component';

describe('VisitedCountriesListComponent', () => {
  let component: VisitedCountryListComponent;
  let fixture: ComponentFixture<VisitedCountryListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [VisitedCountryListComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(VisitedCountryListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
