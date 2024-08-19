import { Injectable } from '@angular/core';
import { BaseService } from './base.service';
import { IUser } from '../models/user/user';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private baseService: BaseService) {
  }
  getUserById(id: string) {
    return this.baseService.get<{ user: IUser }>(`/User/${id}`);
  }
}
