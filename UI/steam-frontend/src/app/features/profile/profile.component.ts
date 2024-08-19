import { Component } from '@angular/core';
import { map, Observable } from 'rxjs';
import { IGame } from 'src/app/core/models/games/game';
import { IUser } from 'src/app/core/models/user/user';
import { AuthenticationService } from 'src/app/core/services/authentication.service';
import { GameService } from 'src/app/core/services/game.service';
import { UserService } from 'src/app/core/services/user.service';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.scss']
})
export class ProfileComponent {
  user?: IUser;
  userId?: string;
  games: IGame[] = [];
  constructor(private authService: AuthenticationService, private userService: UserService, private gameService: GameService) { }

  async ngOnInit() {
    this.userId = await this.authService.getUserIdAsync();
    this.getUserDataById(this.userId);
    this.getGamesByUserId();

  }

  getUserDataById(id: string) {
    return this.userService.getUserById(id).subscribe({
      next: (response) => { this.user = response.user, console.log(response) },
      error: (err) => { console.error(err.error.message) }
    })
  }

  getGamesByUserId() {
    if (this.userId) {
      this.gameService.getGamesByUserId(this.userId).subscribe({
        next: (response) => {
          this.games = response.games;
          console.log(response.games);
        },
        error: (err) => {
          console.error(err.error.message);
        }
      })
    }
  }

}
