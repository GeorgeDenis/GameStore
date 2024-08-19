import { Component, Input } from '@angular/core';
import { IGame } from 'src/app/core/models/games/game';
import { CurrencyService } from 'src/app/core/services/currency.service';

@Component({
  selector: 'app-game-card',
  templateUrl: './game-card.component.html',
  styleUrls: ['./game-card.component.scss']
})
export class GameCardComponent {
  @Input() game?: IGame;
  @Input() imageNr?: number;
  currency: string = localStorage.getItem('currency') || 'EUR';
  urls: string[] = ["../../../../../assets/images/fortnite.jfif", "../../../../../assets/images/csgo.jfif", "../../../../../assets/images/cyberpunk.jfif", "../../../../../assets/images/rdr2.jfif", "../../../../../assets/images/among.jfif", "../../../../../assets/images/the_witcher.jfif", "../../../../../assets/images/lol.jfif"]
  constructor(private readonly currencyService: CurrencyService){}
  
  ngOnInit(){
    this.currencyService.currentCurrency.subscribe(currentCurrency => {
      this.currency = currentCurrency;
      console.log(this.currency)
    })
  }

  getImageUrl() {
    if (this.imageNr)
      return this.urls[this.imageNr];
    return this.urls[0];
  }

}
