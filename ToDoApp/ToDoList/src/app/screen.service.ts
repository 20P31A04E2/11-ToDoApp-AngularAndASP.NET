import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ScreenService {
  private isMobile = new BehaviorSubject<boolean>(false);
  isMobile$ = this.isMobile.asObservable();

  constructor() {
    this.checkWidth();
    window.addEventListener('resize', () => this.checkWidth());
  }

  private checkWidth() {
    const isMobile = window.innerWidth <= 375;
    this.isMobile.next(isMobile);
  }
}
