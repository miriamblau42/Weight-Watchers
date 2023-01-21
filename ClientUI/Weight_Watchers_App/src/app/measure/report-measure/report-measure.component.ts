import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { Measure } from 'src/app/models/measure';
import { MeasureService } from 'src/app/services/measure.service';

@Component({
  selector: 'app-report-measure',
  templateUrl: './report-measure.component.html',
  styleUrls: ['./report-measure.component.css'],
})
export class ReportMeasureComponent {
  weight?: number;
  comment?: string;
  cardId?: string = sessionStorage.getItem('cardId')?.toString();
  errors: boolean = false;
  errorMessage: string = '';
  loading: boolean = false;
  typeSelected = 'ball-fussion';

  constructor(
    private measureServise: MeasureService,
    private route: Router,
    private spinnerService: NgxSpinnerService
  ) {}
  ReportMeasure() {
    if (!this.weight) {
      this.errors = true;
      this.errorMessage = 'Weight has to be between 5 - 300 KG.';
      return;
    }

    this.spinnerService.show();
    this.loading = true;

    const newMeasure: Measure = {
      CardID: Number(this.cardId),
      weight: this.weight,
      comment: this.comment,
    };

    this.measureServise
      .postMeasure(newMeasure)
      .then((res) => {
        this.errors = false;
        alert('Measure added successfuly.');
        console.log(res);
        this.cleanPostLeftovers();
      })
      .catch((err) => {
        this.errors = true;
        this.errorMessage = 'Server error. Please try again later.';
        console.log(err);
        this.cleanPostLeftovers();
      });
  }

  goToTrackingTable() {
    this.route.navigate(['tracking/tracking-table']);
  }

  cleanInputs() {
    this.weight = undefined;
    this.comment = undefined;
  }

  hideLoading() {
    this.spinnerService.hide();
    this.loading = false;
  }

  cleanPostLeftovers() {
    this.hideLoading();
    this.cleanInputs();
  }
}
