import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, catchError, tap } from 'rxjs';
import { Department } from '../models/department.model';

@Injectable({
  providedIn: 'root'
})
export class EmployeeService {
  private apiUrl = 'https://localhost:7020/api/Employee'

  constructor(private http: HttpClient) { }

  saveEmployeeDetails(employeeData: any): Observable<any> {
    return this.http.post(this.apiUrl, employeeData);
  }

  getDepartments(): Observable<Department[]> {
    return this.http.get<Department[]>('https://localhost:7020/api/Department');
  }

  getEmployees(): Observable<any> {
    return this.http.get(this.apiUrl);
  }

  updateEmployeeDetails(employeeData: any): Observable<any> {
    return this.http.put(this.apiUrl, employeeData)
    .pipe(
      tap((response: any) => console.log('Update Employee Response:', response)),
      catchError((error: any) => {
        console.error('Error updating employee details', error);
        throw error;
      })
      );
  }

  deleteEmployee(employeeNumber: string): Observable<any> {
    return this.http.delete(`${this.apiUrl}/${employeeNumber}`)
    .pipe(
      tap((response: any) => console.log('Delete Employee Response:', response)),
      catchError((error: any) => {
        console.error('Error deleting employee', error);
        throw error;
      })
      );
  }
}
