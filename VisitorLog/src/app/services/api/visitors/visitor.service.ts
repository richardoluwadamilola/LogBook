import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { Employee } from '../models/employee.model';

@Injectable({
  providedIn: 'root'
})
export class VisitorService {
  private apiUrl = 'https://localhost:7020/api/Visitor'

  constructor(private http: HttpClient) { }

  saveVisitorDetails(visitorData: any): Observable<any> {
    // Ensure employeeId is a number, and check for NaN
    if (visitorData.employeeId !== null && !isNaN(visitorData.employeeId)) {
      visitorData.employeeId = Number(visitorData.employeeId);
    } else {
      console.error('Invalid employeeId:', visitorData.employeeId);
      throw Error('Invalid employeeId');
    }
  
    console.log('Request Payload:', visitorData);
    
    return this.http.post(this.apiUrl, visitorData).pipe(
      catchError((error: HttpErrorResponse) => {
        console.error('Error saving visitor details', error);
        throw error;
      })
    );
  }
  

  getEmployees(): Observable<Employee[]> {
    return this.http.get<Employee[]>('https://localhost:7020/api/Employee');
  }
}
