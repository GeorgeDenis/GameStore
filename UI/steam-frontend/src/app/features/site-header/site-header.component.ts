import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { ToastService } from 'angular-toastify';
import { AuthenticationService } from 'src/app/core/services/authentication.service';

@Component({
  selector: 'app-site-header',
  templateUrl: './site-header.component.html',
  styleUrls: ['./site-header.component.scss']
})
export class SiteHeaderComponent {
  constructor(private authService: AuthenticationService, private router: Router, private toast: ToastService) { }
  isAuthenticated(): boolean {
    return this.authService.isAuthenticated();
  }
  logout(): void {
    this.authService.logout().subscribe(
      {
        next: (response) => {
          console.log('Logout successful', response);
          localStorage.removeItem('token');
          this.toast.success("Logged out successfully!");
          this.router.navigate(['/sign-in']);
        },
        error: err => {
          console.error('Logout failed', err);
        }
      }
    );
  }
}
