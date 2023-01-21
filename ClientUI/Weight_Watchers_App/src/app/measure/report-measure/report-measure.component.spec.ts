import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ReportMeasureComponent } from './report-measure.component';

describe('ReportMeasureComponent', () => {
  let component: ReportMeasureComponent;
  let fixture: ComponentFixture<ReportMeasureComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ReportMeasureComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ReportMeasureComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
