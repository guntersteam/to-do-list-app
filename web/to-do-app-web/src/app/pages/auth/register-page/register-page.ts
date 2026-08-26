import { Component } from '@angular/core';
import { AuthInput } from "../../../shared/auth-input/auth-input";
import { RouterLink } from '@angular/router';

@Component({
  imports: [RouterLink,AuthInput],
  selector: 'app-register-page',
  styleUrl: './register-page.css',
  templateUrl: './register-page.html',
})
export class RegisterPage {}
