import { Component } from '@angular/core';
import { AuthInput } from "../../../shared/auth-input/auth-input";
import { RouterLink } from '@angular/router';

@Component({
  imports: [RouterLink, AuthInput],
  selector: 'app-login-page',
  styleUrl: './login-page.css',
  templateUrl: './login-page.html',
})
export class LoginPage {}
