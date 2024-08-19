import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { IGame, IUpdateGame } from 'src/app/core/models/games/game';
import { IDeveloper } from 'src/app/core/models/developers/developer';
import { DeveloperService } from 'src/app/core/services/developer.service';
import { GameService } from 'src/app/core/services/game.service';
import { NotificationSocketService } from 'src/app/core/services/notification-socket.service';
@Component({
  selector: 'app-update-game',
  templateUrl: './update-game.component.html',
  styleUrls: ['./update-game.component.scss']
})
export class UpdateGameComponent {
  availableGenres: string[] = ['Action', 'Adventure', 'Puzzle', 'Strategy'];
  developersValue: IDeveloper[] = [];
  gamesValues: IGame[] = [];
  game: IUpdateGame = {
    gameId: '',
    name: '',
    description: '',
    releaseDate: new Date(),
    price: 0,
    developerId: '',
    image: null as any,
    genres: []
  }
  constructor(private gameService: GameService, private developerService: DeveloperService, private router: Router, private notificationSocket: NotificationSocketService) { }
  ngOnInit() {
    this.getDevelopers();
    this.getGames();
  }

  handlePostGame() {
    this.gameService.postGame(this.game).subscribe({
      next: (response) => { this.handleClear(); this.router.navigate(['/games']) },
      error: (err) => { console.log(err.error.message) }
    });
  }

  onFileSelected(event: any) {
    const file: File = event.target.files[0];
    if (file) {
      this.game.image = file;
    }
  }

  toggleGenreSelection(genre: string) {
    const index = this.game.genres.indexOf(genre);
    if (index < 0) {

      this.game.genres.push(genre);
    }
  }

  deleteGenreSelection(genre: string) {
    const index = this.game.genres.indexOf(genre);
    if (index >= 0) {
      this.game.genres.splice(index, 1);
    } else {
      this.game.genres.push(genre);
    }
  }


  isSelected(genre: string): boolean {
    return this.game.genres.includes(genre);
  }

  handleClear() {
    this.game = {
      gameId: '',
      name: '',
      description: '',
      releaseDate: new Date(),
      price: 0,
      developerId: '',
      image: null as any,
      genres: []
    };
  }

  getDevelopers() {
    return this.developerService.getDevelopers().subscribe({
      next: (response) => {
        this.developersValue = response.developers;
      },
      error: (err) => {
        console.log(err.error.message);
      }
    })
  }

  getGames() {
    return this.gameService.getAllGames().subscribe({
      next: (response) => {
        this.gamesValues = response.games;
      },
      error: (err) => {
        console.log(err.error.message);
      }
    })
  }
  getGameById(id: string) {
    return this.gameService.getGameById(id).subscribe({
      next: (response) => {
        this.game = {
          gameId: response.game.gameId,
          name: response.game.name,
          description: response.game.description,
          releaseDate: response.game.releaseDate,
          price: response.game.price,
          developerId: response.game.developerId,
          image: null as any,
          genres: response.game.genre
        }
      },
      error: (err) => {
        console.log(err.error.message);
      }
    })
  }

  handleUpdateGame() {
    this.gameService.putGame(this.game).subscribe({
      next: (response) => {
        this.notificationSocket.sendNotification(this.game.name, 'game-updated');
        this.handleClear();
        this.router.navigate(['/games'])
      },
      error: (err) => { console.log(err.error.message) }
    });
  }

}
