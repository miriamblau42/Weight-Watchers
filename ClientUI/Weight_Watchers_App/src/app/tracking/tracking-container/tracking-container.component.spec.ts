import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TrackingContainerComponent } from './tracking-container.component';

describe('TrackingContainerComponent', () => {
  let component: TrackingContainerComponent;
  let fixture: ComponentFixture<TrackingContainerComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TrackingContainerComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(TrackingContainerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
