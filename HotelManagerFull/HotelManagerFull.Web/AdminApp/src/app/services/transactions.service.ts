import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { URL_API } from '../constants/constants';

@Injectable({
  providedIn: 'root',
})
export class TransactionsService {
  constructor(private http: HttpClient) {}

  public save(params: any): Observable<any> {
    return this.http.post<any>(`${URL_API}Transactions/Save`, params);
  }
}
