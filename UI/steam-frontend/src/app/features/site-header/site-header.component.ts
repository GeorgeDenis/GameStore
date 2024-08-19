import { Component, inject } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { ToastService } from 'angular-toastify';
import { AuthenticationService } from 'src/app/core/services/authentication.service';
import { LogoutDialogComponent } from '../utils/logout-dialog/logout-dialog/logout-dialog.component';
import { NotificationService } from 'src/app/core/services/notification.service';
import { NotificationPanelComponent } from '../notification-panel/notification-panel.component';
import { NotificationSocketService } from 'src/app/core/services/notification-socket.service';
import { INotification } from 'src/app/core/models/notification/notification';
import { trigger, state, style, transition, animate } from '@angular/animations';
import { NotificationRefreshService } from 'src/app/core/services/notification-refresh.service';

@Component({
  selector: 'app-site-header',
  templateUrl: './site-header.component.html',
  styleUrls: ['./site-header.component.scss'],
  animations: [
    trigger('buzzShine', [
      state('default', style({ transform: 'scale(1)', opacity: 1 })),
      state('active', style({ transform: 'scale(1.2)', opacity: 1.5 })),
      transition('default => active', [
        animate('0.5s ease-in-out')
      ]),
      transition('active => default', [
        animate('0.5s ease-in-out')
      ]),
    ])
  ]
})
export class SiteHeaderComponent {
  readonly dialog = inject(MatDialog);
  notificationsCount: number = 0;
  notifications: INotification[] = [];
  animationState = 'default';

  constructor(
    private authService: AuthenticationService,
    private router: Router,
    private toast: ToastService,
    private notificationService: NotificationService,
    private notificationSocket: NotificationSocketService,
    private notificationRefreshService: NotificationRefreshService
  ) { }

  ngOnInit() {
    this.loadNotifications();
    this.notificationSocket.notificationUpdate.subscribe(() => {
      this.handleNotificationUpdate();
      this.triggerAnimation();
    });
    this.notificationService.readNotification.subscribe(() => {
      this.loadNotifications();
    });
    this.notificationRefreshService.loadNotificationsOnLogin.subscribe(() => {
      this.loadNotifications();
    })
  }

  loadNotifications() {
    this.getAllNotifications();
    this.getNumberOfUnreadNotifications();
  }

  triggerAnimation() {
    this.animationState = 'active';
    setTimeout(() => {
      this.animationState = 'default';
    }, 1000);
  }
  isAuthenticated(): boolean {
    return this.authService.isAuthenticated();
  }
  isAdmin(): boolean {
    return this.authService.isAdmin();
  }

  handleNotificationUpdate() {
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

  logout(): void {
    this.authService.logout().subscribe(
      {
        next: (response) => {
          console.log('Logout successful', response);
          localStorage.removeItem('token');
          this.toast.success("Logged out successfully!");
          localStorage.removeItem("role");
          this.router.navigate(['/sign-in']);
        },
        error: err => {
          console.error('Logout failed', err);
        }
      }
    );
  }

  openLogoutDialog(): void {
    const dialogRef = this.dialog.open(LogoutDialogComponent);

    dialogRef.afterClosed().subscribe(result => {
      console.log('The dialog was closed');
    });
  }

  openNotificationDialog(enterAnimationDuration: string, exitAnimationDuration: string): void {
    this.dialog.open(NotificationPanelComponent, {
      width: '500px',
      height: '450px',
      enterAnimationDuration,
      exitAnimationDuration,
    });
  }

}
