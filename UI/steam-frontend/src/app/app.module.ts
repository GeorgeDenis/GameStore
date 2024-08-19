import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatCardModule } from '@angular/material/card';
import { MatDialogModule } from '@angular/material/dialog';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatSelectModule } from '@angular/material/select';
import { ToastService, AngularToastifyModule } from 'angular-toastify';

import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatNativeDateModule } from '@angular/material/core';
import { MatChipsModule } from '@angular/material/chips';
import { MatExpansionModule } from '@angular/material/expansion';
import { MatMenuModule } from '@angular/material/menu';
import { MatSliderModule } from '@angular/material/slider';
import { MatBadgeModule } from '@angular/material/badge';
import { MatListModule } from '@angular/material/list';
import { TooltipPosition, MatTooltipModule } from '@angular/material/tooltip';
import { TimeagoModule } from "ngx-timeago";

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { GamesComponent } from './features/games/games.component';
import { GameCardComponent } from './features/games/components/game-card/game-card.component';
import { SiteHeaderComponent } from './features/site-header/site-header.component';
import { HomeComponent } from './features/home/home.component';
import { GamesDetailsComponent } from './features/games-details/games-details.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { GameReviewComponent } from './features/game-review/game-review.component';
import { SignInComponent } from './features/sign-in/sign-in.component';
import { SignUpComponent } from './features/sign-up/sign-up.component';
import { AuthenticationInterceptor } from './core/services/interceptors/authentication.interceptor';
import { LogoutDialogComponent } from './features/utils/logout-dialog/logout-dialog/logout-dialog.component';
import { PostReviewComponent } from './features/game-review/post-review/post-review/post-review.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ProfileComponent } from './features/profile/profile.component';
import { SearchGamesComponent } from './features/games/components/search-games/search-games/search-games.component';
import { AddGameComponent } from './features/admin/add-game/add-game.component';
import { AdminMenuComponent } from './features/admin-menu/admin-menu.component';
import { DeleteGameComponent } from './features/admin/delete-game/delete-game.component';
import { UpdateGameComponent } from './features/admin/update-game/update-game.component';
import { SearchFilterComponent } from './features/search-filter/search-filter.component';
import { CountryInterceptor } from './core/services/interceptors/country.interceptor';
import { CurrencySelectorComponent } from './features/currency-selector/currency-selector.component';
import { NotificationPanelComponent } from './features/notification-panel/notification-panel.component';
import { NotificationComponent } from './features/notification-panel/notification/notification.component';

@NgModule({
  declarations: [
    AppComponent,
    GamesComponent,
    GameCardComponent,
    GameReviewComponent,
    SiteHeaderComponent,
    HomeComponent,
    GamesDetailsComponent,
    SignInComponent,
    SignUpComponent,
    LogoutDialogComponent,
    PostReviewComponent,
    ProfileComponent,
    SearchGamesComponent,
    AddGameComponent,
    AdminMenuComponent,
    DeleteGameComponent,
    UpdateGameComponent,
    SearchFilterComponent,
    CurrencySelectorComponent,
    NotificationPanelComponent,
    NotificationComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatCardModule,
    MatButtonModule,
    MatIconModule,
    MatDialogModule,
    MatProgressSpinnerModule,
    AngularToastifyModule,
    BrowserAnimationsModule,
    MatSelectModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatChipsModule,
    MatExpansionModule,
    MatMenuModule,
    MatSliderModule,
    MatBadgeModule,
    MatListModule,
    MatTooltipModule,
    TimeagoModule.forRoot()
  ],


  providers: [ToastService, { provide: HTTP_INTERCEPTORS, useClass: AuthenticationInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: CountryInterceptor, multi: true },

  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
