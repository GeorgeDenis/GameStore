import { Component, Input } from '@angular/core';
import { IReview } from 'src/app/core/models/reviews/review';

@Component({
  selector: 'app-game-review',
  templateUrl: './game-review.component.html',
  styleUrls: ['./game-review.component.scss']
})
export class GameReviewComponent {
  @Input() review?: IReview;
}
