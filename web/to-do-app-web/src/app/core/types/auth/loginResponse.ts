export interface LoginResponse {
    accessToken: string;
    user: User
}

export interface User{
    id: string
    username: string;
    email: string
}