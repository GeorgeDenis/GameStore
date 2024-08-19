import { Component, inject } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { ToastService } from 'angular-toastify';
import { AuthenticationService } from 'src/app/core/services/authentication.service';

@Component({
  selector: 'app-logout-dialog',
  templateUrl: './logout-dialog.component.html',
  styleUrls: ['./logout-dialog.component.scss']
})
export class LogoutDialogComponent {
  readonly dialogRef = inject(MatDialogRef<LogoutDialogComponent>);

  constructor(private authService: AuthenticationService, private toast: ToastService, private router: Router) { }

  onNoClick(): void {
    this.dialogRef.close();
  }
  logout(): void {
    this.authService.logout().subscribe(
      {
        next: (response) => {
          this.dialogRef.close();
          console.log('Logout successful', response);
          localStorage.removeItem('token');
          localStorage.removeItem('role');
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
