import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PortsListComponent } from './ports-list.component';

describe('PortsListComponent', () => {
  let component: PortsListComponent;
  let fixture: ComponentFixture<PortsListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [PortsListComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PortsListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
