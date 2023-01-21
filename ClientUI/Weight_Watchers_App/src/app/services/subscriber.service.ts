import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Subscriber } from '../models/subscriber';
import { Observable } from 'rxjs';
import { LoginSubscriber } from '../models/login-subscriber';
import { CardInfo } from '../models/card-info';

@Injectable({
  providedIn: 'root',
})
export class SubscriberService {
  private base = 'https://localhost:7235/api';
  constructor(private http: HttpClient) {}

  postSubscriber(newSubscriber: Subscriber): Observable<boolean> {
    return this.http.post<boolean>(`${this.base}/subscriber`, newSubscriber);
  }

  Signin(loginSubscriber: LoginSubscriber): Observable<number> {
    return this.http.post<number>(`${this.base}/Login`, loginSubscriber);
  }

  getCardById(id: string): Observable<CardInfo> {
    return this.http.get<CardInfo>(`${this.base}/card/` + id);
  }
}
