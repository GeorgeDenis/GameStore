import { EventEmitter, Injectable, Output } from '@angular/core';
import { BaseService } from './base.service';
import { Observable } from 'rxjs';
import { INotification } from '../models/notification/notification';

@Injectable({
  providedIn: 'root'
})
export class NotificationService {
  @Output() readNotification = new EventEmitter<void>();
  constructor(private readonly baseService: BaseService) {
  }


  getAllNotificationsForUser(): Observable<{ notifications: INotification[] }> {
    return this.baseService.get(`/Notification/userId`);
  }

  handleReadNotification(notificationId: string) {
    return this.baseService.get(`/Notification/readStatus/${notificationId}`);
  }

  handleRefetchNotifications() {
    this.readNotification.emit();
  }

}
