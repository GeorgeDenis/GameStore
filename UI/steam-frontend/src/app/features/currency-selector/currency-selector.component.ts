import { Component } from '@angular/core';
import { CurrencyService } from 'src/app/core/services/currency.service';

@Component({
  selector: 'app-currency-selector',
  templateUrl: './currency-selector.component.html',
  styleUrls: ['./currency-selector.component.scss']
})
export class CurrencySelectorComponent {
  constructor(private currencyService: CurrencyService) { }
  ngOnInit() {
    if(!localStorage.getItem('currency'))
      localStorage.setItem('currency', 'EUR');
  }
  changeCurrency(currency: string) {
    localStorage.setItem('currency', currency);
    this.currencyService.changeCurrency(currency);
  }
}
