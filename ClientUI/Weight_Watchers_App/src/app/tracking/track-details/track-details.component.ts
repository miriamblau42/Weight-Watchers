import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Track } from 'src/app/models/track';

@Component({
  selector: 'app-track-details',
  templateUrl: './track-details.component.html',
  styleUrls: ['./track-details.component.css'],
})
export class TrackDetailsComponent implements OnInit {
  tracksList: Track[] = [];
  trackId: number = Number(this.route.snapshot.paramMap.get('id'));
  params = this.route.snapshot.paramMap;
  track?: Track = {
    id: 0,
    weight: Number(this.params.get('weight')),
    bmi: Number(this.params.get('BMI')),
    comment: this.params.get('comment')?.toString(),
    date: this.params.get('date')?.toString(),
    trend: this.params.get('trend')?.toString(),
  };
  infoList: string[][] = [];

  constructor(private route: ActivatedRoute) {}

  ngOnInit(): void {
    alert('trackId:' + this.trackId);
    this.params = this.route.snapshot.paramMap;
    if (this.params)
      this.track = {
        id: 0,
        weight: Number(this.params.get('weight')),
        bmi: Number(this.params.get('BMI')),
        comment: this.params.get('comment')?.toString(),
        date: this.params.get('date')?.toString(),
        trend: this.params.get('trend')?.toString(),
      };

    if (this.track)
      for (const [key, value] of Object.entries(this.track)) {
        this.infoList.push([key, value]);
      }
 
  }
}
