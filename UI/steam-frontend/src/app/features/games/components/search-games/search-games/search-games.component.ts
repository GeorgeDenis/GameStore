import { Component, EventEmitter, Output } from '@angular/core';

@Component({
  selector: 'app-search-games',
  templateUrl: './search-games.component.html',
  styleUrls: ['./search-games.component.scss']
})
export class SearchGamesComponent {
  @Output() searchValueChange = new EventEmitter<string>();
  value: string = '';

  onValueChange(newValue: string): void {
    this.value = newValue;
    this.searchValueChange.emit(newValue);
  }

  clearValue(): void {
    this.value = '';
    this.searchValueChange.emit('');
  }
}
