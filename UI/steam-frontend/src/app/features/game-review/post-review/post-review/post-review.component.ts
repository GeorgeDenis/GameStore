import { Component, EventEmitter, Input, Output } from '@angular/core';
import { ToastService } from 'angular-toastify';
import { IPostReview } from 'src/app/core/models/reviews/postReview';
import { AuthenticationService } from 'src/app/core/services/authentication.service';
import { ReviewService } from 'src/app/core/services/review.service';

@Component({
  selector: 'app-post-review',
  templateUrl: './post-review.component.html',
  styleUrls: ['./post-review.component.scss']
})
export class PostReviewComponent {
  @Input() gameId = '';
  @Output() reviewPosted = new EventEmitter<void>();

  stars: boolean[] = Array(5).fill(false);
  review: IPostReview = {
    rating: 0,
    comment: '',
    userId: '',
    gameId: ''
  };

  constructor(
    private authService: AuthenticationService,
    private reviewService: ReviewService,
    private toastService: ToastService
  ) { }

  highlightStars(index: number): void {
    this.stars = this.stars.map((_, i) => i <= index);
  }

  async postReview(): Promise<void> {
    const rating = this.calculateRating();
    try {
      const userId = await this.authService.getUserIdAsync();
      this.review = {
        ...this.review,
        userId,
        gameId: this.gameId,
        rating
      };
      if (this.review.comment === '' || this.review.rating <= 0 || this.review.gameId === '' || this.review.userId === '') {
        this.toastService.error("All fields must be completed!");
        return;
      }
      this.handlePost();
    } catch (error) {
      console.error(error);
      this.toastService.error("Failed to get user ID.");
    }
  }

  handleCancel(): void {
    this.stars.fill(false);
    this.review.comment = '';
    this.review.rating = 0;
  }

  private handlePost(): void {
    this.reviewService.postReview(this.review).subscribe({
      next: () => {
        this.toastService.success("Review posted successfully");
        this.handleCancel();
        this.reviewPosted.emit();
      },
      error: (error) => {
        this.toastService.error(error.error.message);
        this.handleCancel();

      }
    });
  }

  private calculateRating(): number {
    return this.stars.reduce((acc, current) => acc + (current ? 1 : 0), 0);
  }
}
