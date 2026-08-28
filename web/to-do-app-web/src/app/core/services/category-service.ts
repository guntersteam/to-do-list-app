import { HttpClient } from '@angular/common/http';
import { inject, Service } from '@angular/core';
import { environment } from '../../../environments/environment.development';
import { ApiResponse } from '../types/common/apiResponse';
import { Category } from '../types/category/category';
import { UpdateCategoryRequest } from '../types/category/updateCategoryRequest';

@Service()
export class CategoryService {
    private client = inject(HttpClient)
    private apiUrl = `${environment.apiUrl}/categories`

    getUserCategories(){
        return this.client.get<ApiResponse<Category[]>>(`${this.apiUrl}/me`)
    }

    addUserCategory(categoryName: string){
        return this.client.post<ApiResponse<null>>(this.apiUrl,{categoryName})
    }

    updateUserCategory(updateRequest: UpdateCategoryRequest){
        return this.client.patch<ApiResponse<null>>(this.apiUrl, updateRequest)
    }

    deleteUserCategory(categoryId: string){
        return this.client.delete<ApiResponse<null>>(`${this.apiUrl}/${categoryId}`)
    }

}
