import { Component } from '@angular/core';
import { NotificationService } from './core/services/notification.service';
import { NotificationSocketService } from './core/services/notification-socket.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'steam-frontend';
  constructor(private notificationSocket: NotificationSocketService) { }
}
