import { Component, EventEmitter, Output } from '@angular/core';
import { INotification } from 'src/app/core/models/notification/notification';
import { NotificationService } from 'src/app/core/services/notification.service';

@Component({
  selector: 'app-notification-panel',
  templateUrl: './notification-panel.component.html',
  styleUrls: ['./notification-panel.component.scss']
})
export class NotificationPanelComponent {
  notifications: INotification[] = [];
  constructor(private readonly notificationService: NotificationService) { }
  ngOnInit() {
    this.getAllNotifications();
  }
  getAllNotifications() {
    return this.notificationService.getAllNotificationsForUser().subscribe({
      next: (response) => {
        this.notifications = response.notifications;
      },
      error: (err) => { console.error(err.error.message) }
    })
  }

  getNumberOfUnreadNotifications(): number {
    return this.notifications.reduce((total, notification) => {
      return total + (notification.readStatus ? 0 : 1);
    }, 0);
  }
}
