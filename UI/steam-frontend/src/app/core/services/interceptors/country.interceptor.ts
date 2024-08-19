import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable()
export class CountryInterceptor implements HttpInterceptor {

  constructor() {}

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    const currency = localStorage.getItem("currency") || "EUR";

    request = request.clone({
      setHeaders: {
        'X-Currency':currency
      }
    });
    return next.handle(request);
  }
}
