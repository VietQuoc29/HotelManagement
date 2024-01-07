import { Injectable } from '@angular/core';
import {
    HttpRequest,
    HttpHandler,
    HttpEvent,
    HttpInterceptor,
} from '@angular/common/http';
import { Observable } from 'rxjs';
import { URL_API } from '../constants/constants';
import { AuthenticationService } from '../services/authentication.service';

@Injectable()
export class JwtInterceptor implements HttpInterceptor {
    constructor(private accountService: AuthenticationService) { }

    intercept(
        request: HttpRequest<any>,
        next: HttpHandler
    ): Observable<HttpEvent<any>> {
        // add auth header with jwt if user is logged in and request is to the api url
        const user = this.accountService.userValue;
        if (user != null) {
            const isLoggedIn = user && user.token;
            const isApiUrl = request.url.startsWith(URL_API);
            if (isLoggedIn && isApiUrl) {
                request = request.clone({
                    setHeaders: {
                        Authorization: `Bearer ${user.token}`,
                    },
                });
            }
            return next.handle(request);
        }
        return next.handle(request);
    }
}
