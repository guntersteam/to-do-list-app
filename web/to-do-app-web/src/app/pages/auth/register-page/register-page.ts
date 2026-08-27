import { Component, inject } from '@angular/core';
import { AuthInput } from "../../../shared/auth-input/auth-input";
import { Router, RouterLink } from '@angular/router';
import { AuthService } from '../../../core/services/auth-service';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { RegisterRequest } from '../../../core/types/auth/registerRequest';
import { ToastService } from '../../../core/services/toast-service';
import { ApiResponse } from '../../../core/types/common/apiResponse';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  imports: [ReactiveFormsModule,RouterLink, AuthInput],
  selector: 'app-register-page',
  styleUrl: './register-page.css',
  templateUrl: './register-page.html',
})
export class RegisterPage {
  private formBuilder = inject(FormBuilder)
  private router = inject(Router)
  private authService = inject(AuthService)
  private toastService = inject(ToastService)

  registerForm = this.formBuilder.nonNullable.group({
    email: ['', [Validators.required,Validators.email]],
    username: ['', [Validators.required]],
    password: ['', [Validators.required]]
  })

  onSubmit(){
    if(this.registerForm.invalid) return;

    const registerPayload : RegisterRequest = this.registerForm.getRawValue()

    this.authService.register(registerPayload).subscribe({
      next: () =>{
        this.toastService.show("Sussessfully registered")
        this.router.navigate(["/auth/login"]);
      },
      error: (err) =>{
      }
    })
  }

}
