import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './app.components/home/home.component';
import { PageNotFoundComponent } from './app.components/page-not-found/page-not-found.component';

const routes: Routes = [
  {
    path: 'subscriber',
    loadChildren: () =>
      import('./subscriber/subscriber.module').then((m) => m.SubscriberModule),
  },
  {
    path: 'tracking',
    loadChildren: () =>
      import('./tracking/tracking.module').then((m) => m.TrackingModule),
  },
  {
    path: 'measure',
    loadChildren: () =>
      import('./measure/measure.module').then((m) => m.MeasureModule),
  },
  { path: '', component: HomeComponent },
  { path: '**', component: PageNotFoundComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
