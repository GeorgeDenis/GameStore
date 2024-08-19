import { CanActivateFn, Router, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { inject } from '@angular/core';
export const authGuard: CanActivateFn = (route: ActivatedRouteSnapshot, state: RouterStateSnapshot) => {
  const router: Router = inject(Router);
  const token = localStorage.getItem('token');
  const userRole = localStorage.getItem('role');
  if (!token) {
    return router.navigate(['/sign-in']);
  }
  const expectedRole = route.data['role'] as string;
  console.log(userRole)

  if (expectedRole && userRole !== expectedRole) {
    router.navigate(['/home']);
    return false;
  }
  return true;
};
