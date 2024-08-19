import { Component } from '@angular/core';
import { FilterService } from 'src/app/core/services/filters.service';

@Component({
  selector: 'app-search-filter',
  templateUrl: './search-filter.component.html',
  styleUrls: ['./search-filter.component.scss']
})
export class SearchFilterComponent {
  availableGenres: string[] = ['Action', 'Adventure', 'Puzzle', 'Strategy'];
  selectedGenres: string[] = [];
  minPrice: number = 0;
  maxPrice: number = 60;
  startDate: Date | null = null;
  endDate: Date | null = null;

  constructor(private filterService: FilterService) { }

  toggleGenreSelection(genre: string) {
    const index = this.selectedGenres.indexOf(genre);
    if (index < 0) {
      this.selectedGenres.push(genre);
    } else {
      this.selectedGenres.splice(index, 1);
    }
  }
  isSelected(genre: string): boolean {
    return this.selectedGenres.includes(genre);
  }

  applyFilters() {
    const filters = {
      genres: this.selectedGenres,
      priceRange: { min: this.minPrice, max: this.maxPrice },
      dateRange: { start: this.startDate, end: this.endDate }
    };
    this.filterService.updateFilters(filters);
  }
  clearFilters() {
    this.selectedGenres = [];
    this.minPrice = 0;
    this.maxPrice = 60;
    this.startDate = null;
    this.endDate = null;
    const filters = {
      genres: this.selectedGenres,
      priceRange: { min: this.minPrice, max: this.maxPrice },
      dateRange: { start: this.startDate, end: this.endDate }
    };
    this.filterService.updateFilters(filters);

  }
}
