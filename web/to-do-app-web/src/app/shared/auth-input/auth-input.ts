import { Component, Input } from '@angular/core';

@Component({
  imports: [],
  selector: 'app-auth-input',
  styleUrl: './auth-input.css',
  templateUrl: './auth-input.html',
})
export class AuthInput {
  @Input() type: string = "text"
  @Input() placeholder: string  = ""
}
