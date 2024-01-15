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
    console.log('Request Payload:', visitorData);
    
    return this.http.post(this.apiUrl, visitorData).pipe(
      catchError((error: HttpErrorResponse) => {
        console.error('Error saving visitor details', error);
        console.error('Error Details:', error.error);
        throw error;
      })
    );
  }
  

  getEmployees(): Observable<Employee[]> {
    return this.http.get<Employee[]>('https://localhost:7020/api/Employee');
  }
}
