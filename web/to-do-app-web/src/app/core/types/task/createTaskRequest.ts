export interface CreateTaskRequest {
    title: string;
    note: string | null;
    dueTime: Date | string | null
    categoryIds: string[] | null
}