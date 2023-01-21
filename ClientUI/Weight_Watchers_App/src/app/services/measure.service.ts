import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Measure } from '../models/measure';

@Injectable({
  providedIn: 'root',
})
export class MeasureService {
  private base = 'https://localhost:7026/api';
  constructor(private http: HttpClient) {}

  postMeasure(newMeasure: Measure) {
    return new Promise(async (resolve, reject) => {
      await this.http
        .post<string>(`${this.base}/Measures`, newMeasure)
        .toPromise()
        .then((res) => {
          // Success
          console.log(res);
          resolve(res);
        })
        .catch((err) => reject(err));
    });
  }
}
