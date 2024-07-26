export interface IGame{
    gameId: string,
    name: string,
    description: string,
    releaseDate: Date,
    genre: string[],
    price: number,
    developerId: string,
    image: string,
}