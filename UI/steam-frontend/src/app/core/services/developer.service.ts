import { Injectable } from '@angular/core';
import { BaseService } from './base.service';
import { IDeveloper } from '../models/developers/developer';
@Injectable({
  providedIn: 'root'
})
export class DeveloperService {

  constructor(private baseService: BaseService) { }

  getDevelopers() {
    return this.baseService.get<{ developers: IDeveloper[] }>("/Developer");
  }
}
