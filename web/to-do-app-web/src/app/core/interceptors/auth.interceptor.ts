import { HttpErrorResponse, HttpHandlerFn, HttpInterceptorFn, HttpRequest } from "@angular/common/http"
import { catchError, switchMap, throwError } from "rxjs"
import { AuthService } from "../services/auth-service"
import { inject } from "@angular/core"

export const authInterceptor: HttpInterceptorFn = (req, next) =>{
    const authService = inject(AuthService)
    const token = localStorage.getItem("token")

    let authReq = req;

    if(token)
        return next(addToken(authReq,token))

    return next(authReq)
    .pipe(
        catchError((err: HttpErrorResponse) => {
            if(err.status == 401 && !req.url.includes("/refresh")){
                return refreshAndProceed(authService, authReq, next)
            }

            return throwError(() => err)
        })
    )

}

const refreshAndProceed = (authService: AuthService, request: HttpRequest<any>, next: HttpHandlerFn) => {
    return authService.refresh().pipe(
        switchMap(apiResponse => {
            return next(addToken(request, apiResponse.data!.accessToken));
        }),
        catchError((error) => {
            authService.logout();
            return throwError(() => error);
        })
    );
}
const addToken = (req: HttpRequest<any>, token: string) =>{
    return req.clone({
        setHeaders: {
            Authorization: `Bearer ${token}`
        }
    })
}