import { Routes } from '@angular/router';
import { AuthLayout } from './shared/layout/auth-layout/auth-layout';
import { RegisterPage } from './pages/auth/register-page/register-page';
import { LoginPage } from './pages/auth/login-page/login-page';

export const routes: Routes = [
  {
    path: 'auth',
    component: AuthLayout,
    children: [
      { path: 'register', component: RegisterPage },
      {path: "login", component: LoginPage},
      { path: '', redirectTo: 'login', pathMatch: 'full' }
    ]
  }
];