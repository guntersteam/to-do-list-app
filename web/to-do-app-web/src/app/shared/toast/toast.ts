import { AsyncPipe, NgClass } from '@angular/common';
import { Component, inject } from '@angular/core';
import { ToastService } from '../../core/services/toast-service';

@Component({
  imports: [AsyncPipe, NgClass],
  selector: 'app-toast',
  styleUrl: './toast.css',
  templateUrl: './toast.html',
})
export class Toast {
  public toastService = inject(ToastService)
}
