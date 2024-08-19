import { Injectable } from '@angular/core';
import { BaseService } from './base.service';
import { IGame, IPostGame, IUpdateGame } from '../models/games/game';

@Injectable({
  providedIn: 'root'
})
export class GameService {

  constructor(private baseService: BaseService) { }

  getGamesByUserId(id: string) {
    return this.baseService.get<{ games: IGame[] }>("/GameUser");
  }
  postGame(game: IPostGame) {
    const formData = new FormData();

    formData.append('name', game.name);
    formData.append('description', game.description);
    formData.append('releaseDate', game.releaseDate.toISOString());
    formData.append('price', game.price.toString());
    formData.append('developerId', game.developerId);

    if (game.image) {
      formData.append('image', game.image);
    }

    game.genres.forEach((genre, index) => {
      formData.append(`genres[${index}]`, genre);
    });

    return this.baseService.postForm("/Game", formData);
  }

  putGame(game: IUpdateGame) {
    const formData = new FormData();
    
    formData.append('gameId', game.gameId);
    formData.append('name', game.name);
    formData.append('description', game.description);
    formData.append('price', game.price.toString());
    formData.append('developerId', game.developerId);

    if (game.image) {
      formData.append('image', game.image);
    }

    game.genres.forEach((genre, index) => {
      formData.append(`genres[${index}]`, genre);
    });

    return this.baseService.putForm("/Game", formData);
  }

  getAllGames(){
    return this.baseService.get<{ games: IGame[] }>("/Game");
  }

  deleteGameById(id: string){
    return this.baseService.delete(`/Game/${id}`);
  }

  getGameById(id: string){
    return this.baseService.get<{game: IGame}>(`/Game/${id}`);
  }

}
