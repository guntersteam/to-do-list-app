import { HttpClient, HttpParams } from '@angular/common/http';
import { inject, Service } from '@angular/core';
import { environment } from '../../../environments/environment.development';
import { ApiResponse } from '../types/common/apiResponse';
import { SearchTaskResponse, Task } from '../types/task/task';
import { FilterSettings } from '../types/common/filter-settings';
import { CreateTaskRequest } from '../types/task/createTaskRequest';
import { UpdateTaskRequest } from '../types/task/updateTaskRequest';

@Service()
export class TaskService {
    private client = inject(HttpClient)
    private apiUrl = `${environment.apiUrl}/tasks`

    getUserTasks(searchRequest: FilterSettings){
        let params = new HttpParams({
            fromObject: {
                title: searchRequest.title,
                categoryIds: searchRequest.categoryIds,
                page: searchRequest.page,
                pageSize: searchRequest.pageSize,
            }
        });
        
        if(searchRequest.isCompleted !== null){
            params = params.set('isCompleted', searchRequest.isCompleted);
        }
        
        return this.client.get<ApiResponse<SearchTaskResponse>>(`${this.apiUrl}/me`, { params });
    }

    createTask(createRequest: CreateTaskRequest){
        return this.client.post<ApiResponse<null>>(this.apiUrl, createRequest)
    }

    updateTask(updateRequest: UpdateTaskRequest){
        return this.client.put<ApiResponse<null>>(this.apiUrl, updateRequest)
    }


    deleteTask(taskId: string ){
        return this.client.delete<ApiResponse<null>>(`${this.apiUrl}/${taskId}`)
    }


}
