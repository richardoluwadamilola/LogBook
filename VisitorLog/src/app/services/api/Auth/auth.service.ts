import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private apiUrl = 'https://localhost:7020/api/User';
  private tokenKey = 'authToken';

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

  // Get the stored token
  getAuthToken(): string | null {
    return localStorage.getItem(this.tokenKey);
  }

  // Clear the stored token
  clearAuthToken(): void {
    localStorage.removeItem(this.tokenKey);
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
