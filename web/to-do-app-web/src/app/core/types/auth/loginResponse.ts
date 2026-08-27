export interface LoginResponse {
    accessToken: string;
    user: User
}

export interface User{
    username: string;
    email: string
}