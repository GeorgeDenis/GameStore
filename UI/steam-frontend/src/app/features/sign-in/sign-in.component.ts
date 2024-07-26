import { Component } from '@angular/core';
import { ToastService } from 'angular-toastify';
import { ISignInModel } from 'src/app/core/models/user/signIn';
import { IToken } from 'src/app/core/models/user/tokenDetails';
import { BaseService } from 'src/app/core/services/base.service';
import { Router } from '@angular/router';
import { AuthenticationService } from 'src/app/core/services/authentication.service';

@Component({
  selector: 'app-sign-in',
  templateUrl: './sign-in.component.html',
  styleUrls: ['./sign-in.component.scss']
})
export class SignInComponent {
  credentials: ISignInModel = {
    email: '',
    password: ''
  }
  constructor(private toastService: ToastService, private authService: AuthenticationService, private router: Router) { }
  //   signIn(){
  //     this.baseService.post<IToken>(this.baseUrl).subscribe({
  //       next: (response) => { console.log(response) },
  //       error: (err) => console.error("Error sign in:", err)
  //     })
  //   }
  signIn() {
    if (this.credentials.email === '' || this.credentials.password === '') {
      this.toastService.error("Both fields must be completed!");
      return;
    }

    this.authService.login(this.credentials).subscribe({
      next: (response) => {
        console.log(response);
        this.toastService.success("Login successfully!");
        localStorage.setItem('token', response.token);
        this.router.navigate(['/games']);
      },
      error: (err) => {
        console.error("Error:", err);
        this.toastService.error(err.error.error);
      },
    });
  }
}

