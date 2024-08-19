import { EventEmitter, Injectable, Output } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class NotificationRefreshService {
  @Output() loadNotificationsOnLogin = new EventEmitter<void>();
  constructor() { }
  handleLoadNotificationsOnLogin(){
    this.loadNotificationsOnLogin.emit();
  }
}
