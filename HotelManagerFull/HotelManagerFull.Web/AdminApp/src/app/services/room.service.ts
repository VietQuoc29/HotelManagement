import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { URL_API } from '../constants/constants';

@Injectable({
  providedIn: 'root',
})
export class RoomService {
  constructor(private http: HttpClient) {}

  public get(request: {
    page: any;
    pageSize: any;
    searchText: any;
    hotelId: any;
  }): Observable<any> {
    const params = `?Page=${request.page}&PageSize=${request.pageSize}&SearchText=${request.searchText}&HotelId=${request.hotelId}`;
    return this.http.get<any>(`${URL_API}Rooms/GetAll${params}`);
  }

  public save(params: any): Observable<any> {
    return this.http.post<any>(`${URL_API}Rooms/Save`, params);
  }

  public delete(id: number): Observable<any> {
    return this.http.delete<any>(`${URL_API}Rooms/${id}`);
  }

  public getInfoRoom(request: {
    page: any;
    pageSize: any;
    roomStatusId: any;
    hotelId: any;
  }): Observable<any> {
    const params = `?Page=${request.page}&PageSize=${request.pageSize}&RoomStatusId=${request.roomStatusId}&HotelId=${request.hotelId}`;
    return this.http.get<any>(`${URL_API}Rooms/GetInfoRoom${params}`);
  }

  public updateRoomStatus(params: any): Observable<any> {
    return this.http.post<any>(`${URL_API}Rooms/UpdateRoomStatus`, params);
  }

  public getInfoReturnRoom(
    id: number,
    isPayment: boolean,
    orderRoomId: number
  ): Observable<any> {
    const param = `?id=${id}&isPayment=${isPayment}&orderRoomId=${orderRoomId}`;
    return this.http.get<any>(`${URL_API}Rooms/GetInfoReturnRoom${param}`);
  }

  public paymentRoom(params: any): Observable<any> {
    return this.http.post<any>(`${URL_API}Rooms/PaymentRoom`, params);
  }
}
