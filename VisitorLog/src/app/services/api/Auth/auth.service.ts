import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private apiUrl = 'https://localhost:7020/api/User';

  constructor(private http: HttpClient) { }

  createUser(user: any): any {
    return this.http.post(`${this.apiUrl}/create-user`, user);
  }

  login(username: string, password: string): any {
    const loginData = { username, password };
    return this.http.post(`${this.apiUrl}/login`, loginData);
  }

  changePassword(changePasswordDto: any): any {
    return this.http.put(`${this.apiUrl}/change-password`, changePasswordDto);
  }

  deleteUser(userDto: any): any {
    return this.http.delete(`${this.apiUrl}/delete-user`, userDto);
  }

  isAuthenticated(): boolean {
    const username = localStorage.getItem('username');
    return !!username;
  }
}
