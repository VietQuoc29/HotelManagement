import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { URL_API } from '../constants/constants';

@Injectable({
  providedIn: 'root',
})
export class RoomStatusService {
  constructor(private http: HttpClient) {}

  public suggestion(): Observable<any> {
    return this.http.get<any>(`${URL_API}RoomStatus/Suggestion`);
  }
}
