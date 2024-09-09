import { EventEmitter, Injectable, Output } from '@angular/core';
import { BaseService } from './base.service';
import { Observable } from 'rxjs';
import { INotification, INotificationPaginated } from '../models/notification/notification';

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

  getAllNotificationsForUserPaginated(page: number, pageSize: number): Observable<{ notifications: INotificationPaginated }> {
    return this.baseService.get(`/Notification/paginated?page=${page}&pageSize=${pageSize}`);
  }

  handleReadNotification(notificationId: string) {
    return this.baseService.get(`/Notification/readStatus/${notificationId}`);
  }

  handleRefetchNotifications() {
    this.readNotification.emit();
  }

}
