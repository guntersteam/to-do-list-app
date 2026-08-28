import { Component, Input } from '@angular/core';
import { DatePipe, NgClass } from '@angular/common';
import { Task } from '../../../core/types/task/task';


@Component({
  imports: [DatePipe,NgClass],
  selector: 'app-task-view',
  styleUrl: './task-view.css',
  templateUrl: './task-view.html',
})
export class TaskView {
  @Input({ required: true }) task!: Task;
}
