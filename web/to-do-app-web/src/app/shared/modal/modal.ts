import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
  imports: [],
  selector: 'app-modal',
  styleUrl: './modal.css',
  templateUrl: './modal.html',
})
export class Modal {
  @Input({ required: true }) isOpen = false;
  @Input() title = '';
  
  @Output() close = new EventEmitter<void>();
}
