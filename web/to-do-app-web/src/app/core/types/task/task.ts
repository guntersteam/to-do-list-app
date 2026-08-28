import { Category } from '../category/category';

export interface Task {
    id: string;
    title: string;
    note: string | null;
    createdAt: Date | string;
    isCompleted: boolean;
    dueTime: Date | string;
    taskCategories: Category[];
}

export interface SearchTaskResponse {
    items: Task[];
    pageSize: number;
    page: number;
    totalCount: number;
    totalPages: number;
}