import { Component } from '@angular/core';
import { IGame } from 'src/app/core/models/games/game';
import { BaseService } from 'src/app/core/services/base.service';

@Component({
  selector: 'app-games',
  templateUrl: './games.component.html',
  styleUrls: ['./games.component.scss']
})
export class GamesComponent {
  baseUrl = "/Game"
  games: IGame[] = [];
  game?: IGame;
  errorMessage: string = '';
  gamesLoaded: boolean = false;

  constructor(private readonly baseService: BaseService) {

  }

  ngOnInit(): void {
    this.loadGames();
  }

  loadGames() {
    this.baseService.get<{ games: IGame[] }>(this.baseUrl).subscribe({
      next: (response) => { this.games = response.games, this.gamesLoaded = true },
      error: (err) => console.error("Error loading heroes:", err)
    });
  }
  // loadGameById(){
  //   this.baseService.get<{ game: Game }>(`${this.baseUrl}/4ba63397-6e40-481f-811f-24aebac5ded2`).subscribe({
  //     next: (response) => this.game = response.game,
  //     error: (err) => {
  //       this.errorMessage = 'Error loading games: ' + err.error.message;
  //     }
  //   });
  // }

}
