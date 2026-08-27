import { Component, Input } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';

@Component({
  imports: [ReactiveFormsModule],
  selector: 'app-auth-input',
  styleUrl: './auth-input.css',
  templateUrl: './auth-input.html',
})
export class AuthInput {
  @Input() type: string = "text"
  @Input() placeholder: string  = ""
  @Input() control: any
}
