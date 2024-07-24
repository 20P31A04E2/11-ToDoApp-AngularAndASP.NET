import { Component } from '@angular/core';
import { TodoTasksService, TodoTask } from '../todo-tasks.service';

@Component({
  selector: 'app-task-statastics',
  standalone: true,
  imports: [],
  templateUrl: './task-statastics.component.html',
  styleUrl: './task-statastics.component.css'
})
export class TaskStatasticsComponent {

  allTasks: TodoTask[] = [];
  activeTasks: TodoTask[] = [];
  allTasksLength: number = 0;
  activeTasksLength: number = 0;
  activeTasksStatastics: number = 0;
  completedTaskStatastics: number = 0;

  constructor(private todoTasksService: TodoTasksService) { }

  ngOnInit(): void {

      this.todoTasksService.getUserTasks().subscribe(data => {
        this.allTasks = data;
        this.allTasksLength = this.allTasks.length;

        this.todoTasksService.getSpecificTasks('Active').subscribe(data => {
          this.activeTasks = data;
          this.activeTasksLength = this.activeTasks.length;
          if(this.allTasksLength==0)
          {
            this.activeTasksStatastics=0;
            this.completedTaskStatastics=0;
          }
          else
          {
            this.activeTasksStatastics =Math.round( (this.activeTasksLength / this.allTasksLength) * 100);
            this.completedTaskStatastics = 100 - this.activeTasksStatastics;
          }
        });
      });

    }
}
