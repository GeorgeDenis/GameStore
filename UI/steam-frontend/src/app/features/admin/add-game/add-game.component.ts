import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { IPostGame } from 'src/app/core/models/games/game';
import { DeveloperService } from 'src/app/core/services/developer.service';
import { GameService } from 'src/app/core/services/game.service';
import { IDeveloper } from 'src/app/core/models/developers/developer';
import { NotificationService } from 'src/app/core/services/notification.service';
import { NotificationSocketService } from 'src/app/core/services/notification-socket.service';
@Component({
  selector: 'app-add-game',
  templateUrl: './add-game.component.html',
  styleUrls: ['./add-game.component.scss']
})
export class AddGameComponent {
  availableGenres: string[] = ['Action', 'Adventure', 'Puzzle', 'Strategy'];
  developersValue: IDeveloper[] = [];
  game: IPostGame = {
    name: '',
    description: '',
    releaseDate: new Date(),
    price: 0,
    developerId: '',
    image: null as any,
    genres: []
  }


  constructor(private gameService: GameService, private developerService: DeveloperService, private router: Router, private notificationService: NotificationService, private notificationSocket: NotificationSocketService) {

  }

  ngOnInit() {
    this.getDevelopers();
  }


  handlePostGame() {
    this.gameService.postGame(this.game).subscribe({
      next: (response) => {
        console.log(response);
        this.notificationSocket.sendNotification(this.game.name, 'game-added');
        this.handleClear(); this.router.navigate(['/games'])
      },
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
}
