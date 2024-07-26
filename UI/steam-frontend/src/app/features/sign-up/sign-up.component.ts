import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { ToastService } from 'angular-toastify';
import { ISignUpModel } from 'src/app/core/models/user/signup';
import { AuthenticationService } from 'src/app/core/services/authentication.service';

@Component({
  selector: 'app-sign-up',
  templateUrl: './sign-up.component.html',
  styleUrls: ['./sign-up.component.scss']
})
export class SignUpComponent {
  credentials: ISignUpModel = {
    name: '',
    email: '',
    password: ''
  }
  constructor(private toastService: ToastService, private authService: AuthenticationService, private router: Router) { }
  signUp() {
    if (this.credentials.name == '' || this.credentials.email == '' || this.credentials.password == '') {
      this.toastService.error("All fields must be completed!");
      return;
    }
    this.authService.register(this.credentials).subscribe({
      next: (response) => {
        console.log(response);
        this.toastService.success("Register succesfully!");
        this.router.navigate(['/sign-in'])
      },
      error: (err) => { console.error("Error:", err), this.toastService.error(err.error.errors) },
    })
  }
}
