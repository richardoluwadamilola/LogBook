import { Component, HostListener, OnDestroy, OnInit } from '@angular/core';
import { AuthService } from '../services/api/Auth/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-admin',
  templateUrl: './admin.component.html',
  styleUrls: ['./admin.component.css']
})
export class AdminComponent  implements OnInit, OnDestroy {

  private inactivityTimeout: any;
  private readonly inactivityPeriod = 300000; // 5 minutes

  constructor( private authService: AuthService, private router: Router) { }

  ngOnInit(): void {
    this.initInactivityTimer();
    console.log('Auto logout timer started')
  }

  ngOnDestroy(): void {
    console.log('Auto logout timer stopped');
  }

  @HostListener('window:mousemove') refreshUserState() {
    console.log('Mousemove detected');
    this.resetInactivityTimer();
  }

  @HostListener('window:keypress') refreshUserStateOnKeypress() {
    console.log('Keypress detected');
    this.resetInactivityTimer();
  }

  initInactivityTimer(): void {
    this.inactivityTimeout = setTimeout(() => {
      console.log('User inactive for 5 minutes');
      // Perform logout action here
      this.logout();
    }, this.inactivityPeriod);
  }
  
  resetInactivityTimer(): void {
    clearTimeout(this.inactivityTimeout);
    this.initInactivityTimer();
  }
  

  logout(): void {
    // Implement your logout logic here
    this.authService.clearAuthToken();
    this.router.navigateByUrl('/login');
    console.log('User logged out due to inactivity');
  }

}
