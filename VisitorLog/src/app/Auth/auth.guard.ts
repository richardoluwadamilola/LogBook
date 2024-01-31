import { Inject, Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, CanActivateFn, Router, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';
import { AuthService } from '../services/api/Auth/auth.service';
import { AdminComponent } from '../admin/admin.component';
import { EntryComponent } from '../entry/entry.component';

@Injectable({
  providedIn: 'root'
})

export class authGuard implements CanActivate {

  constructor(private authService: AuthService, private router: Router) { }

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {
    // if (!this.authService.isAuthenticated()) {
    //   this.router.navigate(['/login']);
    //   return false;
    // }
    return true;
  }  

}