import { Component } from '@angular/core';
import { ToastService } from 'angular-toastify';
import { IGame } from 'src/app/core/models/games/game';
import { GameService } from 'src/app/core/services/game.service';
import { NotificationSocketService } from 'src/app/core/services/notification-socket.service';

@Component({
  selector: 'app-delete-game',
  templateUrl: './delete-game.component.html',
  styleUrls: ['./delete-game.component.scss']
})
export class DeleteGameComponent {
  gameId?: string;
  games: IGame[] = [];
  constructor(private gameService: GameService, private toastService: ToastService, private notificationSocket: NotificationSocketService) {

  }

  ngOnInit() {
    this.getGames();
    console.log(this.games)
  }
  handleClear() {
    this.gameId = '';
  }
  getGames() {
    return this.gameService.getAllGames().subscribe({
      next: (response) => {
        this.games = response.games;
      },
      error: (err) => {
        console.log(err.error.message);
      }
    })
  }
  deleteGame() {
    if (!this.gameId) {
      this.toastService.error("You must chose a game!");
      return;
    }
    return this.gameService.deleteGameById(this.gameId).subscribe({
      next: () => {
        this.toastService.success("Game deleted succesfully!");
        const gameName = this.games.find(x => x.gameId == this.gameId)?.name;
        if (gameName)
          this.notificationSocket.sendNotification(gameName, 'game-deleted');
        this.handleClear();
        this.getGames();
      },
      error: (err) => {
        console.log(err.error.message);
      }
    })
  }
}
