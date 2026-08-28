import { HttpErrorResponse, HttpInterceptorFn } from "@angular/common/http";
import { inject } from "@angular/core";
import { ToastService } from "../services/toast-service";
import { catchError, throwError } from "rxjs";

export const errorInterceptor: HttpInterceptorFn = (req, next) => {
  const toastService = inject(ToastService);

  return next(req).pipe(
    catchError((error: HttpErrorResponse) => {
      let errorMessage = 'Unhandled error happened';

      if (error.status === 0) {
        errorMessage = 'Server is not available. Check your connection';
      } 
      else if (Object.keys(error.error?.errors).length != 0) {
        errorMessage = Object.values(error.error.errors).flat().join(' | ');
      }
      
      else if (error.error?.message) {
        errorMessage = error.error.message;
      } 

      toastService.show(errorMessage, 'error');

      return throwError(() => error);
    })
  );
};