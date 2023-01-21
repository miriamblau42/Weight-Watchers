import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { SubscriberService } from 'src/app/services/subscriber.service';
import { Subscriber } from 'src/app/models/subscriber';
import { LoginSubscriber } from 'src/app/models/login-subscriber';

// import custom validator to validate that password and confirm password fields match
import { MustMatch } from '../helpers/MustMatch.validator';
import { Router } from '@angular/router';
import { AppComponent } from 'src/app/app.component';

@Component({
  selector: 'registerForm',
  templateUrl: './register-form.component.html',
  styleUrls: ['./register-form.component.css'],
})
export class RegisterFormComponent implements OnInit {
  submitted: boolean = false;
  showError: boolean = false;
  registerForm = this.formBuilder.group(
    {
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      height: [
        '',
        [Validators.required, Validators.pattern(/^-?([1-9].[1-9]\d*)?$/)],
      ],
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(6)]],
      confirmPassword: ['', Validators.required],
      acceptTerms: [false, Validators.requiredTrue],
    },
    {
      validator: MustMatch('password', 'confirmPassword'),
    }
  );
  constructor(
    private formBuilder: FormBuilder,
    private subscriberService: SubscriberService,
    private route: Router,
    @Inject(AppComponent) private parent: AppComponent
  ) {}

  ngOnInit() {}

  // convenience getter for easy access to form fields
  get f() {
    return this.registerForm.controls;
  }

  onSubmit() {
    this.submitted = true;

    // stop here if form is invalid
    if (this.registerForm.invalid) {
      return;
    }

    let newSubscriber: Subscriber = {
      email: this.f['email'].value,
      password: this.f['password'].value,
      firstName: this.f['firstName'].value,
      lastName: this.f['lastName'].value,
      height: this.f['height'].value,
    };

    const loginSubscriber: LoginSubscriber = {
      email: newSubscriber.email,
      password: newSubscriber.password,
    };

    this.subscriberService.postSubscriber(newSubscriber).subscribe(
      (res) => {
        this.subscriberService.Signin(loginSubscriber).subscribe(
          (res) => {
            sessionStorage.setItem('cardId', res.toString());
            this.parent.authorized = true;
            this.route.navigate(['']);
          },
          (err) => {
            console.log(err);
          }
        );
      },
      (err) => {
        console.log(err);
        this.showError = true;
      }
    );
  }

  onReset() {
    this.submitted = false;
    this.registerForm.reset();
  }

  goToLogin() {
    this.parent.register = false;
  }
}
