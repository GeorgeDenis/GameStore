import { TestBed } from '@angular/core/testing';

import { CountryInterceptor } from './country.interceptor';

describe('CountryInterceptor', () => {
  beforeEach(() => TestBed.configureTestingModule({
    providers: [
      CountryInterceptor
      ]
  }));

  it('should be created', () => {
    const interceptor: CountryInterceptor = TestBed.inject(CountryInterceptor);
    expect(interceptor).toBeTruthy();
  });
});
