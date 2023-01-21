import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Track } from '../models/track';

@Injectable({
  providedIn: 'root',
})
export class TrackService {
  private base = 'https://localhost:7061/api';
  constructor(private http: HttpClient) {}

  getTracksByCardId(id: string): Observable<Track[]> {
    return this.http.get<Track[]>(`${this.base}/Tracking/` + id);
  }
}
