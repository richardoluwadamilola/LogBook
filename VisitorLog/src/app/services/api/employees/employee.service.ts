import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
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
}
