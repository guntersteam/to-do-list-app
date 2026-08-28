import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { environment } from '../../../environments/environment.development';
import { BehaviorSubject, catchError, of, tap } from 'rxjs';
import { RegisterRequest } from '../types/auth/registerRequest';
import { ApiResponse } from '../types/common/apiResponse';
import { LoginResponse, User } from '../types/auth/loginResponse';
import { Router } from '@angular/router';

@Injectable({ providedIn: 'root' })
export class AuthService {
    private client = inject(HttpClient);
    private apiUrl = `${environment.apiUrl}/auth`;
    private router = inject(Router)

    private isAuthenticatedSubject = new BehaviorSubject<boolean>(this.hasToken());
    private currectUserSubject = new BehaviorSubject<User | null>(null);
    
    public isAuthenticated = this.isAuthenticatedSubject.asObservable();
    public currentUser$ = this.currectUserSubject.asObservable();

    constructor() {
        if (this.hasToken()) {
            this.loadCurrentUser().subscribe();
        }
    }

    loadCurrentUser() {
        return this.client.get<ApiResponse<User>>(`${this.apiUrl}/me`).pipe(
            tap(response => {
                if (response.data) {
                    this.currectUserSubject.next(response.data);
                    this.isAuthenticatedSubject.next(true);
                }
            })
        );
    }

    register(userData: RegisterRequest){
        return this.client.post<ApiResponse<null>>(`${this.apiUrl}/sign-up`, userData);
    }
    
    login(userData: Pick<RegisterRequest, "email" | "password">){
        return this.client.post<ApiResponse<LoginResponse>>(`${this.apiUrl}/sign-in`, userData).pipe(
            tap(response => {
                localStorage.setItem("token", response.data!.accessToken);
                this.isAuthenticatedSubject.next(true);

                if(response.data?.user){
                    this.currectUserSubject.next(response.data.user);
                }
            })
        );
    }

    logout(){
        this.client.post<ApiResponse<null>>(`${this.apiUrl}/logout`, null).subscribe();
        this.logoutLocally();
        this.router.navigate(["auth/login"])
        
    }

    refresh(){
        return this.client.post<ApiResponse<LoginResponse>>(`${this.apiUrl}/refresh`,null).pipe(
            tap(apiResponse => { 
                localStorage.setItem("token", apiResponse.data!.accessToken);
            })
        );
    }


    private logoutLocally() {
        localStorage.removeItem("token");
        this.isAuthenticatedSubject.next(false);
        this.currectUserSubject.next(null);
    }

    private hasToken(){
        return !!localStorage.getItem("token");
    }
}