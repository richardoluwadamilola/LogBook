import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private apiUrl = 'https://localhost:7020/api/User';
  private tokenKey = 'authToken';
  private departmentKey = 'department';

  constructor(private http: HttpClient) { }

  createUser(user: any): any {
    return this.http.post(`${this.apiUrl}/create-user`, user);
  }

  login(username: string, password: string): any {
    const loginData = { username, password };
    return this.http.post(`${this.apiUrl}/login`, loginData);
  }

  // Store the token in local storage
  setAuthToken(token: string): void {
    localStorage.setItem(this.tokenKey, token);
  }

  setDepartment(department: string): void {
    localStorage.setItem(this.departmentKey, department);
  }

  // Get the stored token
  getAuthToken(): string | null {
    return localStorage.getItem(this.tokenKey);
  }

  getDepartment(): string | null {
    return localStorage.getItem(this.departmentKey);
  }

  // Clear the stored token
  clearAuthToken(): void {
    localStorage.clear();
  }

  changePassword(changePasswordDto: any): any {
    return this.http.put(`${this.apiUrl}/change-password`, changePasswordDto);
  }

  deleteUser(userDto: any): any {
    return this.http.delete(`${this.apiUrl}/delete-user`, userDto);
  }

  isAuthenticated(): boolean {
    return !!this.getAuthToken();
  }
}
