import { Injectable } from '@angular/core';
import { BaseService } from './base.service';
import { ISignInModel } from '../models/user/signIn';
import { IToken } from '../models/user/tokenDetails';
import { HttpClient } from '@angular/common/http';
import { ISignUpModel } from '../models/user/signup';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { map } from 'rxjs';
import { IValidToken } from '../models/token/validateToken';
import { firstValueFrom } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {
  baseUrl = "http://localhost:5169/api/v1/Auth"
  private tokenKey = 'token';

  constructor(private baseService: BaseService, private http: HttpClient, private router: Router) { }

  login(data: ISignInModel) {
    return this.baseService.post<IToken>('/Auth/login', data);
  }

  register(data: ISignUpModel) {
    return this.baseService.post(`/Auth/register`, data);

  }

  isAuthenticated(): boolean {
    const token = localStorage.getItem(this.tokenKey);
    if (token) {
      return true;
    } else {
      return false;
    }
  }

  isAdmin(): boolean {
    const role = localStorage.getItem("role");
    if (role == "Admin") {
      return true;
    } else {
      return false;
    }
  }

  logout() {
    return this.baseService.get(`/Auth/logout`);
  }
  validateToken(): Observable<IValidToken> {
    return this.baseService.get<IValidToken>(`/Auth/validate`);
  }
  getUserId(): Observable<string> {
    return this.validateToken().pipe(map(response => response.userId));
  }

  async getUserIdAsync(): Promise<string> {
    return await firstValueFrom(this.getUserId());
  }
}
