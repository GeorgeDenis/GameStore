import { Component, Input, SimpleChanges } from '@angular/core';
import { IReview } from 'src/app/core/models/reviews/review';
import { IUser } from 'src/app/core/models/user/user';
import { ReviewService } from 'src/app/core/services/review.service';

@Component({
  selector: 'app-game-review',
  templateUrl: './game-review.component.html',
  styleUrls: ['./game-review.component.scss']
})
export class GameReviewComponent {
  @Input() review?: IReview;
  reviewer?: IUser;
  constructor(private reviewService: ReviewService) { }

  ngOnChanges(changes: SimpleChanges): void {
    if (changes['review'] && this.review) {
      this.loadReviewerName(this.review.userId);
    }
  }

  loadReviewerName(id: string): void {
    this.reviewService.getReviewer(id).subscribe({
      next: (user: IUser) => {
        this.reviewer = user;
      },
      error: (err) => {
        console.error(err);
      }
    });
  }
}
