import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { ActivatedRoute, NavigationExtras, Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { Track } from 'src/app/models/track';
import { TrackService } from 'src/app/services/tracking.service';

@Component({
  selector: 'app-tracking-table',
  templateUrl: './tracking-table.component.html',
  styleUrls: ['./tracking-table.component.css'],
})
export class TrackingTableComponent {
  cardId?: string = sessionStorage.getItem('cardId')?.toString();
  typeSelected = 'ball-fussion';
  clickedRow?: Track;
  state?: any;
  try2?: any;

  reportsData: Track[] = [];
  displayedColumns: string[] = [
    'position',
    'date',
    'weight',
    'trend',
    'comment',
    'BMI',
  ];

  dataSource!: MatTableDataSource<Track>;
  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  constructor(
    private trackingService: TrackService,
    private spinnerService: NgxSpinnerService,
    private route: ActivatedRoute,
    private router: Router
  ) {}
  ngAfterViewInit(): void {
    this.spinnerService.show();
    if (this.cardId)
      this.trackingService.getTracksByCardId(this.cardId).subscribe(
        (res) => {
          this.reportsData = res;
          this.dataSource = new MatTableDataSource(this.reportsData);
          this.dataSource.paginator = this.paginator;
          this.dataSource.sort = this.sort;
          this.spinnerService.hide();
        },
        (err) => {
          console.log(err);
          this.spinnerService.hide();
        }
      );
  }
  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();

    if (this.dataSource.paginator) {
      this.dataSource.paginator.firstPage();
    }
  }

  async showRowInfo(row: Track, index: number) {
    this.clickedRow = row;

    await this.router.navigate(['tracking/tracking-container/' + index]);
    this.try2 = this.route.snapshot.paramMap.get('id');
    //window.location.reload();
  }
}
