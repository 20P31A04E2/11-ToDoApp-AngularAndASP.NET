import { Component } from '@angular/core';
import { RouterModule, RouterLink, Router, NavigationEnd } from '@angular/router';
import { CommonModule } from '@angular/common';
import { AddtaskComponent } from '../addtask/addtask.component';
import { ScreenService } from '../screen.service';
@Component({
  selector: 'app-sidenav',
  standalone: true,
  imports: [RouterLink, RouterModule, CommonModule, AddtaskComponent],
  templateUrl: './sidenav.component.html',
  styleUrl: './sidenav.component.css'
})
export class SidenavComponent {
  currentRoute = '';
  routerName = '';
  isModalOpen = false;
  isMobile: boolean = false;

  constructor(private router: Router, private screenService: ScreenService) {
    this.router.events.subscribe(event => {
      if (event instanceof NavigationEnd) {
        this.currentRoute = event.urlAfterRedirects;
        if (this.currentRoute == '/sideNav/dashboard') {
          this.routerName = 'Dashboard';
        }
        if (this.currentRoute == '/sideNav/active') {
          this.routerName = 'Active';
        }
        if (this.currentRoute == '/sideNav/completed') {
          this.routerName = 'Completed';
        }
      }
    });
  }

  ngOnInit() {
    this.screenService.isMobile$.subscribe((isMobile) => {
      this.isMobile = isMobile;
    });
  }

  openAddTaskModal() {
    this.isModalOpen = true;
  }

  updateModalChange(modalChange: boolean) {
    this.isModalOpen = modalChange;
  }


  onSelect(event: Event): void {
    const target = event.target as HTMLSelectElement;
    const selectedPath = target.value;
    this.router.navigateByUrl(selectedPath);
  }

  SignOut(){
    localStorage.removeItem('token');
    localStorage.removeItem('Username');
    this.router.navigate(['/login']);
  }

}
