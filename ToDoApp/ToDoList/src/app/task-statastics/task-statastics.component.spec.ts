import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TaskStatasticsComponent } from './task-statastics.component';

describe('TaskStatasticsComponent', () => {
  let component: TaskStatasticsComponent;
  let fixture: ComponentFixture<TaskStatasticsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [TaskStatasticsComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(TaskStatasticsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
