import { Component, ElementRef, EventEmitter, HostListener, inject, Input, OnDestroy, OnInit, Output } from '@angular/core';
import { Category } from '../../core/types/category/category';
import { FormBuilder, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { debounce, debounceTime, Subject, takeUntil } from 'rxjs';
import { FilterSettings } from '../../core/types/common/filter-settings';

@Component({
  selector: 'app-search',
  standalone: true,
  imports: [ReactiveFormsModule],
  templateUrl: './search.html'
})
export class Search implements OnInit, OnDestroy {
  @Input() categories: Category[] = [];
  @Output() filtersChanged = new EventEmitter<FilterSettings>();

  private fb = inject(FormBuilder);
  private destroy$ = new Subject<void>();

  constructor(private elementRef: ElementRef) {}

  isCategoryMenuOpen = false;

  filterForm: FormGroup = this.fb.nonNullable.group({
    title: [''],
    categoryIds: [[] as string[]],
    isCompleted: [null as boolean | null],
    pageSize: [5]
  });

  ngOnInit() {
    this.filterForm.valueChanges.pipe(
      debounceTime(300),
      takeUntil(this.destroy$)
    ).subscribe(values => {
      this.filtersChanged.emit(values as FilterSettings);
    });
  }

  @HostListener('document:click', ['$event'])
  onClickOutside(event: MouseEvent) {
    if (!this.elementRef.nativeElement.contains(event.target as Node)) {
      this.isCategoryMenuOpen = false;
    }
  }

  toggleCategoryMenu() {
    this.isCategoryMenuOpen = !this.isCategoryMenuOpen;
  }

  toggleCategory(categoryId: string) {
    const currentSelected = this.filterForm.get('categoryIds')?.value as string[];
    const index = currentSelected.indexOf(categoryId);
    
    if (index === -1) {
      this.filterForm.patchValue({ categoryIds: [...currentSelected, categoryId] });
    } else {
      const newSelected = currentSelected.filter(id => id !== categoryId);
      this.filterForm.patchValue({ categoryIds: newSelected });
    }
  }

  ngOnDestroy() {
    this.destroy$.next();
    this.destroy$.complete();
  }
}