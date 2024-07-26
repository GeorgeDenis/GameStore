import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { GamesComponent } from './features/games/games.component';
import { HomeComponent } from './features/home/home.component';
import { GamesDetailsComponent } from './features/games-details/games-details.component';
import { SignInComponent } from './features/sign-in/sign-in.component';
import { SignUpComponent } from './features/sign-up/sign-up.component';
import { authGuard } from './core/guards/auth-guard.guard';

const routes: Routes = [
  { path: 'home', component: HomeComponent, title: 'Steam - Games Shop' },
  { path: 'games/:id', component: GamesDetailsComponent, title: 'Game - Details' },
  { path: 'games', component: GamesComponent, title: 'Store', canActivate: [authGuard] },
  { path: "sign-in", component: SignInComponent, title: 'Sign-In' },
  { path: "sign-up", component: SignUpComponent, title: 'Sign-Up' },
  { path: '', redirectTo: 'home', pathMatch: 'full' }

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
