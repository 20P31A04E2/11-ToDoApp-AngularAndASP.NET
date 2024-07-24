import { Routes } from '@angular/router';
import { SignUpComponent } from './sign-up/sign-up.component';
import { SignInComponent } from './sign-in/sign-in.component';
import { SidenavComponent } from './sidenav/sidenav.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { ActiveComponent } from './active/active.component';
import { CompletedComponent } from './completed/completed.component';
import { authGuard } from './auth.guard';

export const routes: Routes = [
    { path: '', component: SignUpComponent },
    { path: 'login', component: SignInComponent },
    {
        path: 'sideNav',
        component: SidenavComponent,
        canActivate: [authGuard],
        children: [
            { path: 'dashboard', component: DashboardComponent },
            { path: 'active', component: ActiveComponent },
            { path: 'completed', component: CompletedComponent },
        ]
    }
];
