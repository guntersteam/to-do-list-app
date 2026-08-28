export interface ApiResponse<T>{
    success: boolean;
    data : T | null;
    errors: Record<string,string>[] | null;
    message: string | null;
}