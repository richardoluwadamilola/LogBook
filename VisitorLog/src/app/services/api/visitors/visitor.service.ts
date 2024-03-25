import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { Employee } from '../models/employee.model';
import { Visitor } from '../models/visitor';
import { DatePipe } from '@angular/common';
import { Form } from '@angular/forms';
import { Department } from '../models/department.model';

@Injectable({
  providedIn: 'root'
})
export class VisitorService {
  private apiUrl = 'https://localhost:7020/api/Visitor'
  
  constructor(private http: HttpClient, private datePipe: DatePipe) { }


  saveVisitorDetails(visitorData: FormData): Observable<any> {
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

  getDepartments(): Observable<Department[]> {
    return this.http.get<Department[]>('https://localhost:7020/api/Department');
  }

  getVisitors(): Observable<Visitor[]> {
    return this.http.get<Visitor[]>('https://localhost:7020/api/Visitor/GetVisitors');
  }

  getVisitorbyTagNumber(tagNumber: string): Observable<Visitor> {
    return this.http.get<Visitor>(`https://localhost:7020/api/Visitor/GetVisitorsByTagNumber?tagNumber=${tagNumber}`);
  }

  searchVisitors(searchRequestDTO: any ): Observable<Visitor[]> {
    return this.http.get<Visitor[]>(`${this.apiUrl}/SearchVisitors?`, { params: searchRequestDTO });
  }
}
