import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GamesDetailsComponent } from './games-details.component';

describe('GamesDetailsComponent', () => {
  let component: GamesDetailsComponent;
  let fixture: ComponentFixture<GamesDetailsComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [GamesDetailsComponent]
    });
    fixture = TestBed.createComponent(GamesDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
