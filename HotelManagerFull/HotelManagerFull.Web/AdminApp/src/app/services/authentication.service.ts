import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { URL_API } from '../constants/constants';
import { User } from '../models/user';
import { Router } from '@angular/router';
import { map } from 'rxjs/operators';

@Injectable({
    providedIn: 'root',
})
export class AuthenticationService {
    private userSubject: BehaviorSubject<User | null>;
    public user: Observable<User | null>;
    private clearTimeout: any;

    constructor(private http: HttpClient, private router: Router) {
        this.userSubject = new BehaviorSubject<User | null>(
            JSON.parse(localStorage.getItem('user') || '{}')
        );
        this.user = this.userSubject.asObservable();
    }

    public get userValue(): User | null {
        return this.userSubject.value;
    }

    public signIn(username: string, passWord: string): any {
        const userName = username.trim();
        const password = passWord.trim();

        return this.http
            .post<any>(`${URL_API}Authentication/Login`, { userName, password })
            .pipe(
                map((user) => {
                    // store user details and basic auth credentials in local storage to keep user logged in between page refreshes
                    if (user.data) {
                        const date = new Date().getTime();
                        const { expireTime } = user.data;
                        const exp = date + expireTime * 1000;
                        const data = { ...user.data, expireTime: exp };
                        localStorage.setItem('user', JSON.stringify(data));
                        this.userSubject.next(user.data);
                        this.autoLogout(user.data.expireTime * 1000);
                        return user;
                    }
                    return user;
                })
            );
    }

    public logout(): void {
        localStorage.removeItem('user');
        this.userSubject.next(null);
        this.router.navigate(['/sign-in']);
        if (this.clearTimeout) {
            clearTimeout(this.clearTimeout);
        }
    }

    autoLogout(expirationDate: number): any {
        this.clearTimeout = setTimeout(() => {
            this.logout();
        }, expirationDate);
    }

    autoLogin(): void {
        const userData: {
            username: string;
            token: string;
            expireTime: number;
        } = JSON.parse(localStorage.getItem('user') || '{}');
        const date = new Date().getTime();
        const expirationDate = userData.expireTime;
        this.autoLogout(expirationDate - date);
    }
}
