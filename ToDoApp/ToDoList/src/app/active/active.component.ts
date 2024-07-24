import { Component, Output, EventEmitter } from '@angular/core';
import { TodoTasksService, TodoTask } from '../todo-tasks.service';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { AddtaskComponent } from '../addtask/addtask.component';
import { formatDate } from '@angular/common';
import { SpinnerComponent } from '../spinner/spinner.component';
import { ToastrService } from 'ngx-toastr';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'app-active',
  standalone: true,
  imports: [FormsModule, CommonModule, AddtaskComponent, SpinnerComponent],
  templateUrl: './active.component.html',
  styleUrl: './active.component.css'
})
export class ActiveComponent {
  activeTasks: TodoTask[] = [];
  todayDate: number = 0;
  taskId: number = 0;
  currentDate = '';
  isModalOpen = false;
  taskToEdit: TodoTask | null = null;
  @Output() isLoadingChange = new EventEmitter<boolean>();
  isLoading: boolean = false;
  completedDate: Date = new Date();
  formattedDate:string='';

  constructor(private todoTasksService: TodoTasksService, private toastr: ToastrService, private datePipe: DatePipe) { }

  ngOnInit(): void {
    this.isLoading = true;
    this.todayDate = Date.now();
    this.currentDate = formatDate(this.todayDate, 'EEEE, dd MMMM yyyy', 'en-US');
    this.GetActiveTasks();
  }


  GetActiveTasks() {
    this.todoTasksService.getSpecificTasks('Active').subscribe(data => {
      setTimeout(() => {
        this.isLoading = false;
      }, 500);
      this.activeTasks = data;
      this.activeTasks.forEach(task => {
        if (task.createdOn) {
          const createdDate = new Date(task.createdOn);
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
    if (confirm('Are you sure? Do you want to delete the task ?'))
      this.todoTasksService.deletingTask(taskId).subscribe(
        () => {
          this.toastr.success('Task deleted successfully', 'Success', {
            timeOut: 1500,
            closeButton: true
          });
          this.GetActiveTasks();
        })
  }

  changeTaskStatus(taskId: number | undefined): void {
    this.formattedDate = this.datePipe.transform(this.completedDate, 'yyyy-MM-dd HH:mm:ss.SSS')!;
    this.todoTasksService.ChangingTaskStatus(taskId, this.formattedDate).subscribe(() => {
      this.toastr.success('Moved to completed tasks', 'Success', {
        timeOut: 1500,
        closeButton: true
      });
      this.GetActiveTasks();
    });
  }

  openAddTaskModal(task?: TodoTask): void {
    if (task) {
      this.taskToEdit = { ...task };
    } else {
      this.taskToEdit = null;
    }
    this.isModalOpen = true;
  }

  updateModalChange(modalChange: boolean) {
    this.isModalOpen = modalChange;
    this.taskToEdit = null;
  }

}
