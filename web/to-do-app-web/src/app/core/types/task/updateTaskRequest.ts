import { CreateTaskRequest } from "./createTaskRequest";

export interface UpdateTaskRequest extends CreateTaskRequest{
    taskId: string
    isCompleted: boolean
}