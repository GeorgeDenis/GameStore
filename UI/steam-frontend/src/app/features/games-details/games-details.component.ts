import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ToastService } from 'angular-toastify';
import { IBuyGame } from 'src/app/core/models/games/buyGame';
import { IGame } from 'src/app/core/models/games/game';
import { IReview } from 'src/app/core/models/reviews/review';
import { AuthenticationService } from 'src/app/core/services/authentication.service';
import { BaseService } from 'src/app/core/services/base.service';
import { CurrencyService } from 'src/app/core/services/currency.service';

interface OrderTypes {
  value: string;
  viewValue: string;
}

@Component({
  selector: 'app-games-details',
  templateUrl: './games-details.component.html',
  styleUrls: ['./games-details.component.scss']
})
export class GamesDetailsComponent {
  baseUrl = "/Game"
  gameLoaded: boolean = false;
  reviewsLoaded: boolean = false;
  game?: IGame;
  reviews: IReview[] = [];
  purchaseStatus: boolean = false;
  filterType: string = '0';
  currency: string = localStorage.getItem('currency') || 'EUR';
  orderTypes: OrderTypes[] = [
    { value: '0', viewValue: 'Date' },
    { value: '1', viewValue: 'Rating' },
  ];
  currentId: string = '';
  constructor(private route: ActivatedRoute, private readonly baseService: BaseService, private authService: AuthenticationService, private toastService: ToastService, private currencyService: CurrencyService) {

  }
  ngOnInit() {
    this.route.paramMap.subscribe(params => {
      const id = params.get('id');
      if (id) {
        this.currentId = id;
        this.loadGameById(id);
        this.loadReviewsByGameId(id);
      }
    })
    this.currencyService.currentCurrency.subscribe(currentCurrency => {
      if (this.currentId) {
        this.currency = currentCurrency;

        this.loadGameById(this.currentId);
        this.loadReviewsByGameId(this.currentId);
      }
    })
  }
  loadGameById(id: string) {
    this.baseService.get<{ game: IGame }>(`${this.baseUrl}/${id}`).subscribe({
      next: (response) => { this.game = response.game; console.log(this.game); this.gameLoaded = true; this.getPurchaseStatus(id); },
      error: (err) => {
        console.log('Error loading games: ' + err.error.message);
      }
    });
  }
  getPurchaseStatus(id: string) {
    this.baseService.get(`/GameUser/purchase/${this.game?.gameId}`).subscribe({
      next: (response) => {
        console.log(response);
        this.purchaseStatus = true;
      }
    })
  }
  loadReviewsByGameId(id: string) {
    this.baseService.get<{ reviews: IReview[] }>(`/Review/games/${id}`).subscribe({
      next: (response) => {
        this.reviews = response.reviews.sort((a, b) => {
          const dateA = new Date(a.reviewDate);
          const dateB = new Date(b.reviewDate);
          return dateB.getTime() - dateA.getTime();
        });
        this.reviewsLoaded = true;
      },
      error: (err) => {
        console.log('Error loading games: ' + err.error.message);
      }
    })
  }

  async handleAddGame(): Promise<void> {
    try {
      const userId = await this.authService.getUserIdAsync();
      if (this.game) {
        const data: IBuyGame = {
          gameId: this.game.gameId,
          userId
        };
        this.baseService.post("/GameUser", data).subscribe({
          next: (response) => {
            this.toastService.success("Game bought successfully");
            this.getPurchaseStatus(data.gameId);
          },
          error: (error) => {
            this.toastService.error(error.error.message);
          }
        });
      } else {
        console.error('No game selected.');
      }
    } catch (error) {
      console.error('Error fetching user ID:', error);
      this.toastService.error("Failed to fetch user ID.");
    }
  }

  onReviewPosted() {
    if (this.game) {
      this.loadReviewsByGameId(this.game.gameId);
    }
  }

  sortReviews() {
    if (this.filterType === '0') {
      this.reviews.sort((a, b) => {
        const dateA = new Date(a.reviewDate);
        const dateB = new Date(b.reviewDate);
        return dateB.getTime() - dateA.getTime();
      });
    } else if (this.filterType === '1') {
      this.reviews.sort((a, b) => b.rating - a.rating);
    }
  }

  onFilterChange() {
    this.sortReviews();
  }
}
