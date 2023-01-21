import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RegisterComponent } from './register/register.component';
import { LoginComponent } from './login/login.component';
import { ViewCardComponent } from './view-card/view-card.component';
import { RouterModule, Routes } from '@angular/router';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RegisterFormComponent } from './register-form/register-form.component';
import { AppModule } from '../app.module';

const routes: Routes = [
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
  { path: 'view-card', component: ViewCardComponent },
  { path: 'register-form', component: RegisterFormComponent },
];

@NgModule({
  declarations: [
    RegisterComponent,
    LoginComponent,
    ViewCardComponent,
    RegisterFormComponent,
  ],
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    ReactiveFormsModule,
    FormsModule,
  ],
  exports: [LoginComponent, RegisterFormComponent],
})
export class SubscriberModule {}
