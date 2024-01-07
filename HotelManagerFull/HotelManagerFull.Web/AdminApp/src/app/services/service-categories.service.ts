import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { URL_API } from '../constants/constants';

@Injectable({
  providedIn: 'root',
})
export class ServiceCategoriesService {
  constructor(private http: HttpClient) {}

  public get(request: {
    page: any;
    pageSize: any;
    searchText: any;
  }): Observable<any> {
    const params = `?Page=${request.page}&PageSize=${request.pageSize}&SearchText=${request.searchText}`;
    return this.http.get<any>(`${URL_API}ServiceCategories/GetAll${params}`);
  }

  public save(params: any): Observable<any> {
    return this.http.post<any>(`${URL_API}ServiceCategories/Save`, params);
  }

  public delete(id: number): Observable<any> {
    return this.http.delete<any>(`${URL_API}ServiceCategories/${id}`);
  }

  public suggestion(): Observable<any> {
    return this.http.get<any>(`${URL_API}ServiceCategories/Suggestion`);
  }

  public getAllSerrviceByServiceCategories(): Observable<any> {
    return this.http.get<any>(
      `${URL_API}ServiceCategories/GetAllSerrviceByServiceCategories`
    );
  }
}
