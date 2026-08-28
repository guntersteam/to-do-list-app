import { Component, inject } from '@angular/core';
import { AuthInput } from "../../../shared/auth-input/auth-input";
import { Router, RouterLink } from '@angular/router';
import { FormBuilder, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { AuthService } from '../../../core/services/auth-service';
import { RegisterRequest } from '../../../core/types/auth/registerRequest';
import { ToastService } from '../../../core/services/toast-service';

@Component({
  imports: [RouterLink, AuthInput,ReactiveFormsModule],
  selector: 'app-login-page',
  styleUrl: './login-page.css',
  templateUrl: './login-page.html',
})
export class LoginPage {
  router = inject(Router)
  formBuilder = inject(FormBuilder)
  authService = inject(AuthService)
  toastService = inject(ToastService)


  loginForm = this.formBuilder.nonNullable.group({
    email: ['',[Validators.required, Validators.email]],
    password: ['',[Validators.required]]
  })

  onSubmit(){
    if(this.loginForm.invalid) return

      const loginPayload: Pick<RegisterRequest, "email" | "password"> = this.loginForm.getRawValue()
      
      this.authService.login(loginPayload).subscribe({
      next: () =>{
        this.toastService.show("Sussessfully logged in")
        this.router.navigate(["/"]);
      }
    })
  }
}
