export interface IGame {
    gameId: string,
    name: string,
    description: string,
    releaseDate: Date,
    genre: string[],
    price: number,
    developerId: string,
    image: string,
}


export interface IPostGame {
    name: string;
    description: string;
    releaseDate: Date;
    price: number;
    developerId: string;
    image: File;
    genres: string[];
}


export interface IUpdateGame {
    gameId: string,
    name: string;
    description: string;
    releaseDate: Date;
    price: number;
    developerId: string;
    image: File;
    genres: string[];
}

