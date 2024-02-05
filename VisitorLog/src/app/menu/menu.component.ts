import { Component, OnInit } from '@angular/core';
import { AuthService } from '../services/api/Auth/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-menu',
  templateUrl: './menu.component.html',
  styleUrls: ['./menu.component.css']
})
export class MenuComponent implements OnInit{
  isloggedIn = false;
  department: string | null = null;

  constructor(private authService: AuthService, private router: Router) {}

  ngOnInit(): void {
    this.isloggedIn = this.authService.isAuthenticated();
    this.department = this.authService.getDepartment();
  }

  logout(): void {
    this.authService.clearAuthToken();
    this.router.navigateByUrl('/login');
    this.isloggedIn = false;
    this.department = null;
    console.log('Logout');
  }

}
