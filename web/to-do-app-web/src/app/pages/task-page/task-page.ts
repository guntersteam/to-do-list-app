import { ChangeDetectorRef, Component, inject, OnInit } from '@angular/core';
import { Search } from "../../shared/search/search";
import { CategoryService } from '../../core/services/category-service';
import { Category } from '../../core/types/category/category';
import { Task } from '../../core/types/task/task';
import { TaskService } from '../../core/services/task-service';
import { FilterSettings } from '../../core/types/common/filter-settings';
import { TaskItem } from "../../shared/task-item/task-item";
import { Modal } from '../../shared/modal/modal';
import { TaskForm } from '../../features/task/task-form/task-form';
import { TaskView } from '../../features/task/task-view/task-view';
import { CategoryManager } from '../../features/category/category-manager/category-manager';
import { CreateTaskRequest } from '../../core/types/task/createTaskRequest';
import { UpdateTaskRequest } from '../../core/types/task/updateTaskRequest';
import { Pagination } from '../../shared/pagination/pagination';
import { AuthService } from '../../core/services/auth-service';
import { AsyncPipe } from '@angular/common';

@Component({
  imports: [Search, TaskItem, Modal, TaskForm, TaskView, CategoryManager, Pagination,AsyncPipe],
  selector: 'app-task-page',
  styleUrl: './task-page.css',
  templateUrl: './task-page.html',
})
export class TaskPage implements OnInit {
  private categoryService = inject(CategoryService);
  private taskService = inject(TaskService);
  private cdr = inject(ChangeDetectorRef);
  
  public authService = inject(AuthService)

  categories: Category[] = [];
  tasks: Task[] = [];
  totalPages = 1;
  currentPage = 1;

  currentFilters: FilterSettings = {
    title: '',
    categoryIds: [],
    page: 1,
    pageSize: 5,
    isCompleted: null
  };

  isTaskFormModalOpen = false;
  isTaskViewModalOpen = false;
  isCategoryModalOpen = false;
  selectedTask: Task | null = null;

  ngOnInit() {
    this.fetchCategories();
    this.fetchTasks();
  }

  private fetchCategories() {
    this.categoryService.getUserCategories().subscribe({
      next: (response) => {
        this.categories = response.data || []; 
        this.cdr.detectChanges();
      }
    });
  }

  private fetchTasks() {
    this.taskService.getUserTasks(this.currentFilters).subscribe({
      next: (response) => {
        this.tasks = response.data?.items || [];
        this.totalPages = response.data?.totalPages || 1;
        
        if (response.data?.page) {
          this.currentPage = response.data.page;
          this.currentFilters.page = this.currentPage;
        } else {
          this.currentPage = this.currentFilters.page;
        }

        this.cdr.detectChanges();
      }
    });
  }

  private deleteTask(id: string) {
    this.taskService.deleteTask(id).subscribe({
      next: () => {
        this.fetchTasks();
      },
    });
  }

  private addTask(createRequest: CreateTaskRequest) {
    this.taskService.createTask(createRequest).subscribe({
      next: () => {
        this.fetchTasks();
      }
    });
  }

  private updateTask(updateRequest: UpdateTaskRequest) {
    this.taskService.updateTask(updateRequest).subscribe({
      next: () => {
        this.fetchTasks();
      }
    });
  }

  onFiltersApplied(filters: FilterSettings) {
    this.currentFilters = {
      ...this.currentFilters,
      title: filters.title,
      categoryIds: filters.categoryIds,
      isCompleted: filters.isCompleted,
      pageSize: filters.pageSize
    };
    
    this.currentFilters.page = 1;
    this.currentPage = 1;
    
    this.fetchTasks();
  }

  onPageChanged(newPage: number) {
    this.currentPage = newPage;
    this.currentFilters.page = newPage;
    this.fetchTasks();
    window.scrollTo({ top: 0, behavior: 'smooth' });
  }

  onDeleteTask($event: string) {
    this.deleteTask($event);
  }

  openCreateTaskModal() {
    this.selectedTask = null;
    this.isTaskFormModalOpen = true;
  }

  onEditTask($event: string) {
    this.selectedTask = this.tasks.find(t => t.id === $event) || null;
    this.isTaskFormModalOpen = true;
  }

  openViewTaskModal($event: string) {
    this.selectedTask = this.tasks.find(t => t.id === $event) || null;
    this.isTaskViewModalOpen = true;
  }

  onSaveTask(taskData: CreateTaskRequest | UpdateTaskRequest) {
    if ('taskId' in taskData) {
      this.updateTask(taskData);
    } else {
      this.addTask(taskData);
    }
    this.isTaskFormModalOpen = false;
  }

  openCategoryModal() {
    this.isCategoryModalOpen = true;
  }

  onAddCategory(name: string) {
    this.categoryService.addUserCategory(name).subscribe({
      next: () => {
        this.fetchCategories();
      }
    });
  }

  onDeleteCategory(id: string) {
    this.categoryService.deleteUserCategory(id).subscribe({
      next: () => {
        this.fetchCategories();
      }
    });
  }

  onToggleTask(taskId: string) {
    const task = this.tasks.find(t => t.id === taskId);
    
    if (task) {
      const updateRequest: UpdateTaskRequest = {
        taskId: task.id,
        title: task.title,
        isCompleted: !task.isCompleted,
        note: task.note ? task.note : null,
        dueTime: task.dueTime ? new Date(task.dueTime).toISOString() : null,
        categoryIds: task.taskCategories ? task.taskCategories.map(c => c.id) : []
      };

      this.updateTask(updateRequest);
    }
  } 
}