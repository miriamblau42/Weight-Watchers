import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-view-info',
  templateUrl: './view-info.component.html',
  styleUrls: ['./view-info.component.css'],
})
export class ViewInfoComponent implements OnInit {
  objectName: string = '';
  infoList: [] = [];
  constructor() {}

  ngOnInit(): void {}
}
