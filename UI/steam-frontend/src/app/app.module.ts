import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatCardModule } from '@angular/material/card';
import { MatDialogModule } from '@angular/material/dialog';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';

import { ToastService, AngularToastifyModule } from 'angular-toastify';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { GamesComponent } from './features/games/games.component';
import { GameCardComponent } from './features/games/components/game-card/game-card.component';
import { SiteHeaderComponent } from './features/site-header/site-header.component';
import { HomeComponent } from './features/home/home.component';
import { GamesDetailsComponent } from './features/games-details/games-details.component';
import { FormsModule } from '@angular/forms';
import { GameReviewComponent } from './features/game-review/game-review.component';
import { SignInComponent } from './features/sign-in/sign-in.component';
import { SignUpComponent } from './features/sign-up/sign-up.component';
import { AuthenticationInterceptor } from './core/services/interceptors/authentication.interceptor';

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
    SignUpComponent
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
    AngularToastifyModule
  ],
 

  providers: [ToastService,  { provide: HTTP_INTERCEPTORS, useClass: AuthenticationInterceptor, multi: true }],
  bootstrap: [AppComponent]
})
export class AppModule { }
