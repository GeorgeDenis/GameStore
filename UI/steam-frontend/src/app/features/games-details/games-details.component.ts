import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { IGame } from 'src/app/core/models/games/game';
import { IReview } from 'src/app/core/models/reviews/review';
import { BaseService } from 'src/app/core/services/base.service';

@Component({
  selector: 'app-games-details',
  templateUrl: './games-details.component.html',
  styleUrls: ['./games-details.component.scss']
})
export class GamesDetailsComponent {
  baseUrl = "/Game"
  gameLoaded: boolean = false;
  game?: IGame;
  reviews: IReview[] = [];
  constructor(private route: ActivatedRoute, private readonly baseService: BaseService) {

  }
  ngOnInit() {
    this.route.paramMap.subscribe(params => {
      const id = params.get('id');
      if (id) {
        this.loadGameById(id);
        this.loadReviewsByGameId(id);
      }
    })
  }
  loadGameById(id: string) {
    this.baseService.get<{ game: IGame }>(`${this.baseUrl}/${id}`).subscribe({
      next: (response) => { this.game = response.game; this.gameLoaded = true; },
      error: (err) => {
        console.log('Error loading games: ' + err.error.message);
      }
    });
  }
  loadReviewsByGameId(id: string){
    this.baseService.get<{reviews : IReview[]}>(`/Review/games/${id}`).subscribe({
      next: (response) => {this.reviews = response.reviews, console.log(this.reviews)},
      error: (err) => {
        console.log('Error loading games: ' + err.error.message);
      }
    })
  }
}
