import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Routes } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';

const routes: Routes = [
  {
    path: 'auth', children: [
      { path: 'login' },
      { path: 'register' },
      { path: '', redirectTo: 'login', pathMatch: 'full' }
    ]
  }
]

@NgModule({
  declarations: [LoginComponent, RegisterComponent],
  imports: [
    CommonModule
  ]
})
export class AuthenticationModule { }
