import { Component, Inject } from '@angular/core';
import {
  FormControl,
  FormGroup,
  ValidationErrors,
  ValidatorFn,
  Validators,
} from '@angular/forms';
import { Router } from '@angular/router';
import { AppComponent } from 'src/app/app.component';
import { LoginSubscriber } from 'src/app/models/login-subscriber';
import { Subscriber } from 'src/app/models/subscriber';
import { SubscriberService } from 'src/app/services/subscriber.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css'],
})
export class RegisterComponent {
  showForm: boolean = true;
  datesError: boolean = false;
  numberRegEx = /[0-9].[0-9]/;

  RegisterForm = new FormGroup({
    email: new FormControl('', {
      validators: [Validators.required, Validators.email],
    }),
    password: new FormControl('', {
      validators: [Validators.required, Validators.minLength(6)],
    }),
    firstName: new FormControl('', { validators: [Validators.required] }),
    lastName: new FormControl('', { validators: [Validators.required] }),
    height: new FormControl('', {
      validators: [
        Validators.required,
        Validators.min(0.2),
        Validators.max(2.5),
        Validators.pattern(this.numberRegEx),
        this.numberValidator(),
      ],
    }),
  });

  constructor(
    private subscriberService: SubscriberService,
    @Inject(AppComponent) private parent: AppComponent,
    private route: Router
  ) {}

  errorMessage: string = '';

  onFormSubmit() {
    let newSubscriber: Subscriber = {
      email: this.RegisterForm.value.email ?? '',
      password: this.RegisterForm.value.password ?? '',
      firstName: this.RegisterForm.value.firstName ?? '',
      lastName: this.RegisterForm.value.lastName ?? '',
      height: Number(this.RegisterForm.value.height),
    };

    this.subscriberService.postSubscriber(newSubscriber).subscribe(
      (res) => {
        alert('Subscriber was saved successfully');
        const loginSubscriber: LoginSubscriber = {
          email: newSubscriber.email,
          password: newSubscriber.password,
        };

        this.subscriberService.Signin(loginSubscriber).subscribe(
          (res) => {
            sessionStorage.setItem('cardId', res.toString());
            this.parent.authorized = true;
            this.route.navigate(['#']);
          },
          (err) => {
            console.log(err);
          }
        );
      },
      (err) => {
        console.log(err);
      }
    );
    this.showForm = false;
  }

  getError(control: string): string {
    switch (control) {
      case 'email':
      case 'password':
      case 'firstName':
      case 'lastName':
      case 'height': {
        if (this.RegisterForm.controls[control].touched) {
          if (this.RegisterForm.controls[control].value == '') {
            return control + ' is required';
          } else if (control == 'height') {
            if (
              Number.isNaN(Number(this.RegisterForm.controls[control].value))
            ) {
              return (
                '"' +
                this.RegisterForm.controls[control].value +
                '" is not a number'
              );
            }
            if (
              Number(this.RegisterForm.controls[control].value) < 0.2 ||
              Number(this.RegisterForm.controls[control].value) > 2.5
            ) {
              return control + ' has to be between 0.2 and 2.5 meters.';
            }
          } else if (control == 'email') {
            if (this.RegisterForm.controls[control].errors != null) {
              return control + 'is not a valid email address.';
            }
          }
        }

        break;
      }
    }
    return '';
  }

  numberValidator(): ValidatorFn {
    return (form: FormGroup | any): ValidationErrors | null => {
      const height: string = form.get('height')?.value;
      if (height) {
        const isNumberValid = !Number.isNaN(Number(height));
        return isNumberValid ? null : { height: true };
      }
      return null;
    };
  }
}
