import { Component, EventEmitter, Input, Output, SimpleChanges, OnChanges, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink, Router, NavigationEnd } from '@angular/router';
import { FormsModule, FormGroup, FormControl, Validators, MaxLengthValidator } from '@angular/forms';
import { TodoTasksService, TodoTask } from '../todo-tasks.service';
import { ReactiveFormsModule } from '@angular/forms';
import { HttpErrorResponse } from '@angular/common/http';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-addtask',
  standalone: true,
  imports: [RouterLink, CommonModule, FormsModule, ReactiveFormsModule],
  templateUrl: './addtask.component.html',
  styleUrl: './addtask.component.css'
})
export class AddtaskComponent implements OnInit, OnChanges {
  @Input() showModal: boolean = false;
  @Output() showModalChange = new EventEmitter<boolean>();
  @Input() taskToEdit: TodoTask | null = null;
  isFormValid: boolean = false;
  isEditMode: boolean = false;
  currentRoute = '';

  constructor(private todoTasksService: TodoTasksService, private router: Router, private toastr: ToastrService) { }

  ngOnInit(): void {
    this.populateForm();
  }

  ngOnChanges(changes: SimpleChanges): void {
    if (changes['taskToEdit'] && changes['taskToEdit'].currentValue) {
      this.populateForm();
    }
  }

  openModal() {
    this.showModal = true;
  }

  closeModal() {
    this.showModal = false;
    this.showModalChange.emit(this.showModal);
    this.addTaskForm.reset();
    this.isFormValid = false;
  }

  addTaskForm = new FormGroup({
    taskName: new FormControl('', [Validators.required, Validators.maxLength(100)]),
    taskDescription: new FormControl('', [Validators.required, Validators.maxLength(200)])
  })

  populateForm(): void {
    if (this.taskToEdit) {
      this.addTaskForm.patchValue({
        taskName: this.taskToEdit.taskName || '',
        taskDescription: this.taskToEdit.taskDescription || ''
      });
      this.isEditMode = true;
    }
  }

  onSubmit(): void {
    this.isFormValid = true;

    if (this.addTaskForm.valid) {
      const formValues = this.addTaskForm.value;
      const newTask: TodoTask = {
        taskName: formValues.taskName || " ",
        taskDescription: formValues.taskDescription || " ",
      };
      if (this.taskToEdit) {
        newTask.taskId = this.taskToEdit.taskId;
        this.todoTasksService.updateTask(newTask).subscribe({
          next: response => {
            this.closeModal();
            this.toastr.success('Task updated successfully', 'Success', {
              timeOut: 1500,
              closeButton: true
            });
            this.router.navigate(['/sideNav/dashboard']);
          },
          error: error => {
            if (error.status === 401) {
              this.toastr.error('', 'Error', {
                timeOut: 1500,
                closeButton: true
              });
            }
          }
        });
      } else {
        this.todoTasksService.addTask(newTask).subscribe({
          next: response => {
            this.closeModal();
            this.toastr.success('Task added successfully', 'Success', {
              timeOut: 1500,
              closeButton: true
            });
            if (this.router.url === '/sideNav/dashboard') {
              window.location.reload();
            } else {
              this.router.navigate(['/sideNav/dashboard']);
            }
          },
          error: error => {
            if (error.status === 401) {
              this.toastr.error('', 'Error', {
                timeOut: 1500,
                closeButton: true
              });
            }
          }
        });
      }
    }
  }


}
