import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class FilterService {
  private filtersSource = new BehaviorSubject<any>({});
  currentFilters = this.filtersSource.asObservable();

  updateFilters(filters: any) {
    this.filtersSource.next(filters);
  }
}
