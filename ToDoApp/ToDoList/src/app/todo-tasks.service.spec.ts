import { TestBed } from '@angular/core/testing';

import { TodoTasksService } from './todo-tasks.service';

describe('TodoTasksService', () => {
  let service: TodoTasksService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(TodoTasksService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
