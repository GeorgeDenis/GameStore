import { Component, OnInit } from '@angular/core';
import { PageEvent } from '@angular/material/paginator';
import { INotification } from 'src/app/core/models/notification/notification';
import { NotificationService } from 'src/app/core/services/notification.service';

@Component({
  selector: 'app-notification-panel',
  templateUrl: './notification-panel.component.html',
  styleUrls: ['./notification-panel.component.scss']
})
export class NotificationPanelComponent implements OnInit {
  notifications: INotification[] = [];
  DEFAULT_PAGE_SIZE: number = 4;
  pageIndex: number = 0;
  totalItems: number = 0;
  showFirstLastButtons = true;


  constructor(private readonly notificationService: NotificationService) { }

  ngOnInit() {
    this.getAllNotificationsPaginated(this.pageIndex, this.DEFAULT_PAGE_SIZE);
  }

  handlePageEvent(e: PageEvent) {
    this.pageIndex = e.pageIndex;
    this.getAllNotificationsPaginated(this.pageIndex, this.DEFAULT_PAGE_SIZE);
  }

  getAllNotificationsPaginated(page: number, size: number) {
    this.notificationService.getAllNotificationsForUserPaginated(page + 1, size)
      .subscribe({
        next: (response) => {
          this.notifications = response.notifications.notifications;
          this.totalItems = response.notifications.totalItems;

          console.log('Current Page:', page + 1, 'Notifications:', this.notifications, 'Total Items:', this.totalItems);
        },
        error: (err) => {
          console.error(err.error.message);
        }
      });
  }

}
