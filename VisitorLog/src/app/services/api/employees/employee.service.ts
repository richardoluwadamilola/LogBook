import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class EmployeeService {
  private apiUrl = 'https://localhost:7020/api/Employee'

  constructor(private http: HttpClient) { }

  saveEmployeeDetails(employeeData: any): Observable<any> {
    return this.http.post(this.apiUrl, employeeData);
  }
}
