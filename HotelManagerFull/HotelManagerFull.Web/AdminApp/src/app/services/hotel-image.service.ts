import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { URL_API } from '../constants/constants';

@Injectable({
    providedIn: 'root',
})
export class HotelImageService {
    constructor(private http: HttpClient) { }

    public getAllHotelImage(roomId?: number): Observable<any> {
        const params = `?roomId=${roomId}`;
        return this.http.get<any>(`${URL_API}HotelImages/GetAllHotelImage/${params}`);
    }

    public delete(id: number): Observable<any> {
        return this.http.delete<any>(`${URL_API}HotelImages/${id}`);
    }

    public uploadImage(params: any): Observable<any> {
      return this.http.post<any>(`${URL_API}HotelImages/UploadImage`, params);
    }
}
