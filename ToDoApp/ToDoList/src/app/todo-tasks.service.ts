import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class TodoTasksService {
  private todoTaskApiUrl = 'https://api2do.azurewebsites.net/api/TodoTasks';
  constructor(private http: HttpClient) { }

  getUserTasks(): Observable<TodoTask[]> {
    return this.http.get<TodoTask[]>(`${this.todoTaskApiUrl}`);
  }

  getSpecificTasks(status: string): Observable<TodoTask[]> {
    return this.http.get<TodoTask[]>(`${this.todoTaskApiUrl}/${status}`);
  }

  addTask(newTask: TodoTask): Observable<TodoTask> {
    return this.http.post<TodoTask>(`${this.todoTaskApiUrl}`, newTask)
  }

  updateTask(task: TodoTask): Observable<TodoTask> {
    return this.http.put<TodoTask>(`${this.todoTaskApiUrl}`, task);
  }

  ChangingTaskStatus(taskId: number | undefined, completedDate: string): Observable<void> {
    return this.http.put<void>(`${this.todoTaskApiUrl}/${taskId}/${completedDate}`, {});
  }

  deletingTask(taskId: number | undefined): Observable<void> {
    return this.http.delete<void>(`${this.todoTaskApiUrl}/${taskId}`);
  }

  deletingAllTask(): Observable<void> {
    return this.http.delete<void>(`${this.todoTaskApiUrl}/DeleteAllTask`);
  }

}

export interface TodoTask {
  taskId?: number,
  taskName: string,
  taskDescription: string,
  createdOn?: Date,
  daysDifference?: string;
  userId?: number,
  taskStatus?: string,
  showOverlay?: boolean,
  completedOn?: Date
}
