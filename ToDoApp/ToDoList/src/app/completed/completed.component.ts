import { Component, Output, EventEmitter } from '@angular/core';
import { TodoTasksService, TodoTask } from '../todo-tasks.service';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { formatDate } from '@angular/common';
import { SpinnerComponent } from '../spinner/spinner.component';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-completed',
  standalone: true,
  imports: [FormsModule, CommonModule, SpinnerComponent],
  templateUrl: './completed.component.html',
  styleUrl: './completed.component.css'
})
export class CompletedComponent {
  isLoading: boolean = false;
  @Output() isLoadingChange = new EventEmitter<boolean>();
  completedTasks: TodoTask[] = [];
  todayDate: number = 0;
  currentDate = '';


  constructor(private todoTasksService: TodoTasksService, private toastr:ToastrService) { }

  ngOnInit(): void {
    this.isLoading = true;
    this.todayDate = Date.now();
    this.currentDate = formatDate(this.todayDate, 'EEEE, dd MMMM yyyy', 'en-US');
    this.GetCompletedTasks();
  }

  GetCompletedTasks() {
    this.todoTasksService.getSpecificTasks('Completed').subscribe(data => {
      setTimeout(() => {
        this.isLoading = false;
      }, 500);
      this.completedTasks = data;
      this.completedTasks.forEach(task => {
        if (task.completedOn) {
          const createdDate = new Date(task.completedOn);
          const currentDate = new Date();
          const diffTime = Math.round((currentDate.getTime() - createdDate.getTime()) / (1000 * 3600 * 24));
          if (diffTime == 0)
            task.daysDifference = "Added today";
          else
            task.daysDifference = "Added " + diffTime + " day ago."

        } else {
          task.daysDifference = undefined;
        }
      });

    });
  }

  currentOpenTask: any = null;

  toggleOverlay(task: any) {
    if (this.currentOpenTask && this.currentOpenTask !== task) {
      this.currentOpenTask.showOverlay = false;
    }
    task.showOverlay = !task.showOverlay;
    this.currentOpenTask = task.showOverlay ? task : null;
  }

  deleteTask(taskId: number | undefined): void {
    if (confirm('Are you sure? Do you want to delete the task ?')) {
      this.todoTasksService.deletingTask(taskId).subscribe(
        () => {
          this.todoTasksService.getSpecificTasks('Completed').subscribe(data => {
            this.toastr.success('Task deleted successfully', 'Success',{
              timeOut:1500,
              closeButton: true
            });
            this.completedTasks = data;
          });
        })
    }
  }

}
