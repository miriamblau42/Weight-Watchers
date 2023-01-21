import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TrackingTableComponent } from './tracking-table/tracking-table.component';
import { TrackDetailsComponent } from './track-details/track-details.component';
import { RouterModule, Routes } from '@angular/router';
import { MatTableModule } from '@angular/material/table';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatSortModule } from '@angular/material/sort';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { NgxSpinnerModule } from 'ngx-spinner';
import { FormsModule } from '@angular/forms';
import { TrackingContainerComponent } from './tracking-container/tracking-container.component';
// import { MatButtonModule } from '@angular/material';

const routes: Routes = [
  { path: 'tracking-table', component: TrackingTableComponent },
  { path: 'track-details', component: TrackDetailsComponent },
  { path: 'tracking-container/:id', component: TrackingContainerComponent },
];

@NgModule({
  declarations: [
    TrackingTableComponent,
    TrackDetailsComponent,
    TrackingContainerComponent,
  ],
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    MatPaginatorModule,
    MatTableModule,
    MatSortModule,
    MatFormFieldModule,
    MatInputModule,
    // MatButtonModule,
    NgxSpinnerModule,
    FormsModule,
    RouterModule,
  ],
})
export class TrackingModule {}
