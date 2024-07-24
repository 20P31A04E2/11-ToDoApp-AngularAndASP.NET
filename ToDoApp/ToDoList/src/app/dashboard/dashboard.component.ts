import { Component, Output, EventEmitter } from '@angular/core';
import { TaskStatasticsComponent } from '../task-statastics/task-statastics.component';
import { HeroSectionComponent } from '../hero-section/hero-section.component';
import { TodoTasksService, TodoTask } from '../todo-tasks.service';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { formatDate } from '@angular/common';
import { SpinnerComponent } from '../spinner/spinner.component';
import { ToastrService } from 'ngx-toastr';
@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [TaskStatasticsComponent, HeroSectionComponent, FormsModule, CommonModule, SpinnerComponent],
  templateUrl: './dashboard.component.html',
  styleUrl: './dashboard.component.css'
})
export class DashboardComponent {
  allTasks: TodoTask[] = [];
  todayDate: number = 0;
  currentDate: string='';
  @Output() isLoadingChange = new EventEmitter<boolean>();
  isLoading: boolean = false;

  constructor(private todoTasksService: TodoTasksService, private  toastr: ToastrService) { }

  ngOnInit(): void {
    this.isLoading = true;
    this.todayDate = Date.now();
    this.currentDate = formatDate(this.todayDate, 'EEEE, dd MMMM yyyy', 'en-US');
    this.GetAllTasks();
  }

  GetAllTasks() {
      this.todoTasksService.getUserTasks().subscribe(data => {
        setTimeout(() => {
          this.isLoading = false;
        }, 500); 
        this.allTasks = data;
        console.log(this.allTasks);
      });
  }

  getTaskItemBackgroundStyles(task: TodoTask): any {
    if (task.taskStatus == 'Active') {
      return {
        'background-color': 'white',
      };
    } else if (task.taskStatus == 'Completed') {
      return {
        'background-color': '#EDB046',
      };
    }
  }

  getTaskItemIcon(task: TodoTask): string {
    return task.taskStatus === 'Active' ? 'fa-regular fa-square-check' : 'fa-solid fa-square-check';
  }

  getTaskIconColor(task: TodoTask): string {
    return task.taskStatus === 'Active' ? '#EDB046' : '#BA5112'; 
  }


  deleteAllTasks(): void {
    if(confirm('Are you sure? Do you want to delete all tasks ?'))
    {
      this.todoTasksService.deletingAllTask().subscribe(
        () => {
          this.toastr.success('All tasks deleted successfully','Success',{
            timeOut:1500,
            closeButton: true
          });
          this.GetAllTasks();
        })
    }
  }
}
