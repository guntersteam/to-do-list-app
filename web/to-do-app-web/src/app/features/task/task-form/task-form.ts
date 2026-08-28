import { Component, EventEmitter, inject, Input, OnInit, Output } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Category } from '../../../core/types/category/category';
import { Task } from '../../../core/types/task/task';
import { CreateTaskRequest } from '../../../core/types/task/createTaskRequest';
import { UpdateTaskRequest } from '../../../core/types/task/updateTaskRequest';

@Component({
  imports: [ReactiveFormsModule],
  selector: 'app-task-form',
  styleUrl: './task-form.css',
  templateUrl: './task-form.html',
})
export class TaskForm implements OnInit {
  @Input() categories: Category[] = [];
  @Input() task: Task | null = null;
  @Output() save = new EventEmitter<CreateTaskRequest | UpdateTaskRequest>();

  private fb = inject(FormBuilder);

  form: FormGroup = this.fb.nonNullable.group({
    title: ['', Validators.required],
    note: [''],
    dueTime: [''],
    categoryIds: [[] as string[]]
  });

  ngOnInit() {
    if (this.task) {
      let formattedDate = null
      
      if(this.task.dueTime){
      formattedDate = this.task.dueTime 
        ? new Date(this.task.dueTime).toISOString().slice(0, 16) 
        : '';
      }


      this.form.patchValue({
        title: this.task.title,
        note: this.task.note,
        dueTime: formattedDate,
        categoryIds: this.task.taskCategories.map(c => c.id)
      });
    }
  }

  onSubmit() {
    if (this.form.valid) {
      const rawValue = this.form.getRawValue();
  
      const requestBody: any = {
        title: rawValue.title
      };

      if (rawValue.note && rawValue.note.trim() !== '') {
        requestBody.note = rawValue.note.trim();
      }
      if (rawValue.dueTime) {
        requestBody.dueTime = new Date(rawValue.dueTime).toISOString();
      }
      if (rawValue.categoryIds && rawValue.categoryIds.length > 0) {
        requestBody.categoryIds = rawValue.categoryIds;
      }

      if (this.task) {
        requestBody.taskId = this.task.id;
        this.save.emit(requestBody as UpdateTaskRequest); 
      } else {
        this.save.emit(requestBody as CreateTaskRequest);
      }
    }
  }
}
