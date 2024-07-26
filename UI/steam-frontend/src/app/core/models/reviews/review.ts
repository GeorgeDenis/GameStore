export interface IReview{
    reviewId: string,
    rating: number,
    comment: string,
    reviewDate: Date,
    genre: string[],
    userId: string,
    gameId: string,
}