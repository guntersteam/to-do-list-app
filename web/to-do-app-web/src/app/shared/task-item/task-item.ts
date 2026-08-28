import { Component, EventEmitter, Input, Output } from '@angular/core';
import { Task } from '../../core/types/task/task';
import { DatePipe, NgClass } from '@angular/common';

@Component({
  imports: [DatePipe, NgClass],
  selector: 'app-task-item',
  styleUrl: './task-item.css',
  templateUrl: './task-item.html',
})
export class TaskItem {
  @Input({ required: true }) task!: Task;

  @Output() toggleComplete = new EventEmitter<string>();
  @Output() edit = new EventEmitter<string>();
  @Output() delete = new EventEmitter<string>();
  @Output() view = new EventEmitter<string>();
}
