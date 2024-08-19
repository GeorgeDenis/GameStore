import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CurrencyService {
  private currencySource = new BehaviorSubject<string>(localStorage.getItem('currency') || 'EUR'); 
  currentCurrency = this.currencySource.asObservable();

  changeCurrency(currency: string) {
    this.currencySource.next(currency);
  }
}
