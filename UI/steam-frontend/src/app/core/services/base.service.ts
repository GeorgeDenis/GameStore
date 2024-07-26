import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
@Injectable({
  providedIn: 'root'
})
export class BaseService {
  baseUrl = "https://localhost:7156/api/v1";

  constructor(protected http: HttpClient) {
  
   }
   get<T>(url: string): Observable<T>{
    const completeUrl: string = this.baseUrl + url;
    console.log(completeUrl)
    return this.http.get<T>(completeUrl, {headers: this.buildHeaders()});
  }

  post<T>(url: string, data: any): Observable<any>{
    const body: string = JSON.stringify(data);
    const completeUrl: string = this.baseUrl + url;
    return this.http.post<T>(completeUrl,body,{headers: this.buildHeaders()});
  }

  put<T>(url: string, data: T): Observable<T> {
    const body: string = JSON.stringify(data);
    const completeUrl: string = this.baseUrl + url;

    return this.http.put<T>(completeUrl, body, { headers: this.buildHeaders() });
  }

  delete<T>(url: string): Observable<T> {
    const completeUrl: string = this.baseUrl + url;

    return this.http.delete<T>(completeUrl, { headers: this.buildHeaders() });
  }

  protected buildHeaders(): HttpHeaders {
    let headers: HttpHeaders;
    headers = new HttpHeaders();
    headers = headers.set('Accept', 'application/json');
    headers = headers.set('Content-Type', 'application/json; ; charset=UTF-8');
    // headers = headers.set('Cache-Control', 'no-cache');
    return headers;
  }
}
