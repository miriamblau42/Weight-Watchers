import { Component, OnInit } from '@angular/core';
import { CardInfo } from 'src/app/models/card-info';
import { SubscriberService } from 'src/app/services/subscriber.service';

@Component({
  selector: 'app-view-card',
  templateUrl: './view-card.component.html',
  styleUrls: ['./view-card.component.css'],
})
export class ViewCardComponent implements OnInit {
  id = sessionStorage.getItem('cardId')?.toString();
  // put in and get from session storage
  infoList: string[][] = [];
  constructor(private subscriberServise: SubscriberService) {}

  ngOnInit(): void {
    let card: CardInfo;
    this.subscriberServise.getCardById(this.id ?? '-1').subscribe(
      (res: CardInfo) => {
        card = res;
        for (const [key, value] of Object.entries(card)) {
          this.infoList.push([key, value]);
        }
      },
      (err) => {
        console.log(err);
      }
    );
  }
}
