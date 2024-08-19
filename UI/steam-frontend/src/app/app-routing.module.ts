import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { GamesComponent } from './features/games/games.component';
import { HomeComponent } from './features/home/home.component';
import { GamesDetailsComponent } from './features/games-details/games-details.component';
import { SignInComponent } from './features/sign-in/sign-in.component';
import { SignUpComponent } from './features/sign-up/sign-up.component';
import { authGuard } from './core/guards/auth-guard.guard';
import { ProfileComponent } from './features/profile/profile.component';
import { AddGameComponent } from './features/admin/add-game/add-game.component';
import { DeleteGameComponent } from './features/admin/delete-game/delete-game.component';
import { UpdateGameComponent } from './features/admin/update-game/update-game.component';

const routes: Routes = [
  { path: 'home', component: HomeComponent, title: 'Steam - Games Shop' },
  { path: 'games/:id', component: GamesDetailsComponent, title: 'Game - Details' },
  { path: 'games', component: GamesComponent, title: 'Store', canActivate: [authGuard] },
  { path: 'profile', component: ProfileComponent, title: 'Profile', canActivate: [authGuard] },
  { path: "sign-in", component: SignInComponent, title: 'Sign-In' },
  { path: "sign-up", component: SignUpComponent, title: 'Sign-Up' },
  { path: "admin/add-game", component: AddGameComponent, title: 'Add-Game', canActivate: [authGuard], data: { role: "Admin" } },
  { path: "admin/delete-game", component: DeleteGameComponent, title: 'Delete-Game', canActivate: [authGuard], data: { role: "Admin" } },
  { path: "admin/update-game", component: UpdateGameComponent, title: 'Update-Game', canActivate: [authGuard], data: { role: "Admin" } },


  { path: '', redirectTo: 'home', pathMatch: 'full' }

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
