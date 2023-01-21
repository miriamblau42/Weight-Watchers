import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReportMeasureComponent } from './report-measure/report-measure.component';
import { RouterModule, Routes } from '@angular/router';
import { LoadingComponent } from '../app.components/loading/loading.component';
import { NgxSpinnerModule } from 'ngx-spinner';
import { FormsModule } from '@angular/forms';

const routes: Routes = [
  { path: 'report-measure', component: ReportMeasureComponent },
];

@NgModule({
  declarations: [ReportMeasureComponent, LoadingComponent],
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    NgxSpinnerModule,
    FormsModule,
  ],
})
export class MeasureModule {}
