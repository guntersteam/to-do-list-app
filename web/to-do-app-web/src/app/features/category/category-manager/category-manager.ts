import { Component, EventEmitter, Input, Output } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Category } from '../../../core/types/category/category';

@Component({
  imports: [FormsModule],
  selector: 'app-category-manager',
  styleUrl: './category-manager.css',
  templateUrl: './category-manager.html',
})
export class CategoryManager {
  @Input() categories: Category[] = [];
  @Output() add = new EventEmitter<string>();
  @Output() delete = new EventEmitter<string>();

  newCategoryName = '';

  onAdd() {
    if (this.newCategoryName.trim()) {
      this.add.emit(this.newCategoryName.trim());
      this.newCategoryName = '';
    }
  }
}
