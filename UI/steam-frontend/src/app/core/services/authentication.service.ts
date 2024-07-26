import { Injectable } from '@angular/core';
import { BaseService } from './base.service';
import { ISignInModel } from '../models/user/signIn';
import { IToken } from '../models/user/tokenDetails';
import { HttpClient } from '@angular/common/http';
import { ISignUpModel } from '../models/user/signup';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {
  baseUrl = "https://localhost:7156/api/v1/Auth"
  private tokenKey = 'token';

  constructor(private baseService: BaseService, private http: HttpClient, private router: Router) { }

  login(data: ISignInModel) {
    return this.baseService.post<IToken>('/auth/login', data);
  }

  register(data: ISignUpModel) {
    return this.http.post(`${this.baseUrl}/register`, data, { headers: this.buildHeaders() });
  }

  isAuthenticated(): boolean {
    const token = localStorage.getItem(this.tokenKey);
    if (token) {
      return true;
    } else {
      return false;
    }
  }
  logout() {
    return this.baseService.get(`/Auth/logout`);
  }
  
  private buildHeaders() {
    return {
      'Content-Type': 'application/json'
    };
  }
}
