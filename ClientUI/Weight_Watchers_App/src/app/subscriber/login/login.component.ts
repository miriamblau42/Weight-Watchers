import { Component, Inject, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AppComponent } from 'src/app/app.component';
import { LoginSubscriber } from 'src/app/models/login-subscriber';
import { Subscriber } from 'src/app/models/subscriber';
import { SubscriberService } from 'src/app/services/subscriber.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
})
export class LoginComponent {
  email: string = '';
  password: string = '';
  errors: boolean = false;

  constructor(
    private subscriberServise: SubscriberService,
    private route: Router,
    @Inject(AppComponent) private parent: AppComponent
  ) {}

  Signin(): void {
    if (!this.email || !this.password) {
      this.errors = true;
      return;
    }

    const loginSubscriber: LoginSubscriber = {
      email: this.email,
      password: this.password,
    };

    this.subscriberServise.Signin(loginSubscriber).subscribe(
      (res) => {
        sessionStorage.setItem('cardId', res.toString());
        this.parent.authorized = true;
        this.route.navigate(['']);
      },
      (err) => {
        console.log(err);
      }
    );
  }

  goToRegister() {
    this.parent.register = true;
  }
}
