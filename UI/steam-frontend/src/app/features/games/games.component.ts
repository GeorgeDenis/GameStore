import { Component, OnInit } from '@angular/core';
import { IGame } from 'src/app/core/models/games/game';
import { BaseService } from 'src/app/core/services/base.service';
import { CurrencyService } from 'src/app/core/services/currency.service';
import { FilterService } from 'src/app/core/services/filters.service';

@Component({
  selector: 'app-games',
  templateUrl: './games.component.html',
  styleUrls: ['./games.component.scss']
})
export class GamesComponent implements OnInit {
  baseUrl = "/Game";
  games: IGame[] = [];
  filteredGames: IGame[] = [];
  gamesLoaded: boolean = false;
  errorMessage: string = '';
  currentSearchValue: string = '';

  constructor(private readonly baseService: BaseService, private readonly filterService: FilterService,private readonly currencyService: CurrencyService) { }

  ngOnInit(): void {
    this.loadGames();
    this.filterService.currentFilters.subscribe(filters => {
      this.applyFilters(filters);
    });
    this.currencyService.currentCurrency.subscribe(currency => {
      this.loadGames();
    })
  }

  loadGames() {
    this.baseService.get<{ games: IGame[] }>(this.baseUrl).subscribe({
      next: (response) => {
        this.games = response.games;
        this.applyFilters(this.filterService.currentFilters);
        this.gamesLoaded = true;
      },
      error: (err) => console.error("Error loading games:", err)
    });
  }

  applyFilters(filters: any): void {
    const genres = filters.genres || [];
    const priceRange = filters.priceRange || { min: 0, max: Number.MAX_VALUE };
    const dateRange = filters.dateRange || { start: null, end: null };

    this.filteredGames = this.games.filter(game => {
      const matchesGenre = genres.length === 0 ||
        game.genre.some((g: string) => genres.includes(g.trim()));
      const matchesPrice = game.price >= priceRange.min && game.price <= priceRange.max;
      const matchesDate = (!dateRange.start || new Date(game.releaseDate) >= new Date(dateRange.start)) &&
        (!dateRange.end || new Date(game.releaseDate) <= new Date(dateRange.end));
      const matchesSearch = game.name.toLowerCase().includes(this.currentSearchValue.toLowerCase());

      return matchesGenre && matchesPrice && matchesDate && matchesSearch;
    });
  }


  handleSearchValueChange(searchValue: string): void {
    this.currentSearchValue = searchValue;
    this.applyFilters(this.filterService.currentFilters);
  }
}
