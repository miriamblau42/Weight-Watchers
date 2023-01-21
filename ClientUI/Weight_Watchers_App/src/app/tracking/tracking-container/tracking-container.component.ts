import { Component, OnInit } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';
import { Track } from 'src/app/models/track';
import { TrackService } from 'src/app/services/tracking.service';

@Component({
  selector: 'app-tracking-container',
  templateUrl: './tracking-container.component.html',
  styleUrls: ['./tracking-container.component.css'],
})
export class TrackingContainerComponent implements OnInit {
  cardId?: string = sessionStorage.getItem('cardId')?.toString();
  tracksList: Track[] = [];
  constructor(
    private trackingService: TrackService,
    private spinnerService: NgxSpinnerService
  ) {}

  ngOnInit(): void {
    // this.spinnerService.show();
    // if (this.cardId)
    //   this.trackingService.getTracksByCardId(this.cardId).subscribe(
    //     (res) => {
    //       this.tracksList = res;
    //       this.spinnerService.hide();
    //     },
    //     (err) => {
    //       console.log(err);
    //       this.spinnerService.hide();
    //     }
    //   );
  }
}
