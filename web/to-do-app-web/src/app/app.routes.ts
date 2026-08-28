import { Routes } from '@angular/router';
import { AuthLayout } from './shared/layout/auth-layout/auth-layout';
import { RegisterPage } from './pages/auth/register-page/register-page';
import { LoginPage } from './pages/auth/login-page/login-page';
import { TaskPage } from './pages/task-page/task-page';
import { authGuard } from './core/guards/auth.guard';
import { MainLayout } from './shared/layout/main-layout/main-layout';

export const routes: Routes = [
  {
    path: '',
    component: MainLayout, 
    canActivate: [authGuard],
    children:[
      {path: '', component: TaskPage}
    ]
  },

  {
    path: 'auth',
    component: AuthLayout,
    children: [
      { path: 'register', component: RegisterPage },
      { path: "login", component: LoginPage},
      { path: '', redirectTo: 'login', pathMatch: 'full' }
    ]
  }
];