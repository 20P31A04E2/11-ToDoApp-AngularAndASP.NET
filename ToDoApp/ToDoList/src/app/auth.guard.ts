import { CanActivateFn, Router } from '@angular/router';
import { inject } from '@angular/core';
import { AuthGuardService } from './auth-guard.service';


export const authGuard: CanActivateFn = (route, state) => {
  const router = inject(Router);
  const authGuardService = inject(AuthGuardService);
  const isAuthenticated = authGuardService.isAuthenticated();
  if (!isAuthenticated) {
    router.navigate(['/login']);
    return false;
  }
  return true;
};
