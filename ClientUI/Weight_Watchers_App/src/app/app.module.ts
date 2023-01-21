import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { SubscriberService } from './services/subscriber.service';
import { NavBarComponent } from './app.components/nav-bar/nav-bar.component';
import { PageNotFoundComponent } from './app.components/page-not-found/page-not-found.component';
import { HomeComponent } from './app.components/home/home.component';
import { SubscriberModule } from './subscriber/subscriber.module';
import { MeasureService } from './services/measure.service';
import { TrackService } from './services/tracking.service';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgxSpinnerModule } from 'ngx-spinner';
import { LoadingComponent } from './app.components/loading/loading.component';
import { FormsModule } from '@angular/forms';
import { ViewInfoComponent } from './app.components/view-info/view-info.component';

@NgModule({
  declarations: [
    AppComponent,
    NavBarComponent,
    PageNotFoundComponent,
    HomeComponent,
    ViewInfoComponent,
  ],
  imports: [
    BrowserModule,
    NgbModule,
    AppRoutingModule,
    HttpClientModule,
    SubscriberModule,
    BrowserAnimationsModule,
    NgxSpinnerModule,
    FormsModule,
  ],
  providers: [SubscriberService, MeasureService, TrackService],
  bootstrap: [AppComponent],
})
export class AppModule {}
