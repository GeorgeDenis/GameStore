import { Component, Input, OnChanges, SimpleChanges } from '@angular/core';
import { ToastService } from 'angular-toastify';
import { INotification } from 'src/app/core/models/notification/notification';
import { NotificationService } from 'src/app/core/services/notification.service';

@Component({
  selector: 'app-notification',
  templateUrl: './notification.component.html',
  styleUrls: ['./notification.component.scss']
})
export class NotificationComponent {
  @Input() readStatus: boolean = false;
  @Input() notificationType?: string;
  @Input() notificationData: INotification = {
    notificationId: '',
    userId: '',
    subjectName: '',
    dateSent: undefined,
    notificationType: '',
    readStatus: false
  };


  constructor(
    private readonly notificationService: NotificationService,
    private readonly toastService: ToastService
  ) { }


  getNotificationText() {
    return `${this.notificationData.subjectName} ${this.getNotificationType()}`
  }

  getNotificationType() {
    switch (this.notificationData.notificationType) {
      case 'game-added':
        return 'has been added!'
      case 'game-deleted':
        return 'has been deleted'
      case 'game-updated':
        return 'has been updated'
      default:
        return 'Error while retriving notification'
    }
  }

  handleReadNotification() {
    this.notificationService.handleReadNotification(this.notificationData.notificationId).subscribe({
      next: () => {
        this.toastService.success("Notification marked as read!");
        this.notificationData.readStatus = true;
        this.notificationService.handleRefetchNotifications();
      },
      error: () => {
        this.toastService.error("Error while trying to mark the notification as read!");
      }
    });
  }
}
