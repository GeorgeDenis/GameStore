import { Component, Input } from '@angular/core';
import { IGame } from 'src/app/core/models/games/game';

@Component({
  selector: 'app-game-card',
  templateUrl: './game-card.component.html',
  styleUrls: ['./game-card.component.scss']
})
export class GameCardComponent {
  @Input() game?: IGame;
  @Input() imageNr?: number;
  urls: string[] = ["../../../../../assets/images/fortnite.jfif", "../../../../../assets/images/csgo.jfif", "../../../../../assets/images/cyberpunk.jfif","../../../../../assets/images/rdr2.jfif","../../../../../assets/images/among.jfif","../../../../../assets/images/the_witcher.jfif","../../../../../assets/images/lol.jfif"]
  getImageUrl() {
    if (this.imageNr)
      return this.urls[this.imageNr];
    return this.urls[0];
  }
  ngOnInit(){
    console.log(this.game);
  }
}
