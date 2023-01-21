import { Component, OnInit } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
})
export class AppComponent implements OnInit {
  title = 'Weight_Watchers_App';
  authorized: boolean = sessionStorage.getItem('cardId') ? true : false;
  register: boolean = false;
  constructor(private modalService: NgbModal) {}

  ngOnInit(): void {
    //this.authorized = sessionStorage.getItem('cardId') ? true : false;
  }

  public open(modal: any): void {
    this.modalService.open(modal);
  }
}
