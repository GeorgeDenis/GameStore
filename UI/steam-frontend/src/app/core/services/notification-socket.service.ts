import { EventEmitter, Injectable } from '@angular/core';
import { HubConnection, HubConnectionBuilder } from '@microsoft/signalr';

@Injectable({
  providedIn: 'root'
})
export class NotificationSocketService {
  private hubConnection: HubConnection;
  baseSocketUrl = "http://localhost:5169/notificationHub";
  notificationUpdate = new EventEmitter<void>();

  constructor() {
    this.hubConnection = new HubConnectionBuilder()
      .withUrl(this.baseSocketUrl)
      .withAutomaticReconnect()
      .build();

    this.hubConnection.start()
      .then(() => {
        this.hubConnection.on('ReceiveNotification', (gameName: string) => {
          this.notificationUpdate.emit();
        });
      })
      .catch(err => console.error('Eroare la pornirea conexiunii SignalR: ', err));
  }


  async sendNotification(gameName: string, notificationType: string) {
    console.log(gameName)
    try {
      await this.hubConnection.invoke('SendGameNotification', gameName, notificationType);
    } catch (err) {
      console.error('Eroare la trimiterea notificării: ', err);
    }
  }
}
