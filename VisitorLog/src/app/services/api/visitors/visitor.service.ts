import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { Employee } from '../models/employee.model';
import { Visitor } from '../models/visitor';
import { DatePipe } from '@angular/common';
import { Form } from '@angular/forms';

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

  getVisitors(): Observable<Visitor[]> {
    return this.http.get<Visitor[]>('https://localhost:7020/api/Visitor/GetVisitors');
  }

  getVisitorsByCheckInDate(date: Date): Observable<Visitor[]> {
    return this.http.get<Visitor[]>(`https://localhost:7020/api/Visitor/GetVisitorsByCheckInDate?date=${date}`);
  }  

  getVisitorsByEmployeeNumber(employeeNumber: string): Observable<Visitor[]> {
    return this.http.get<Visitor[]>(`https://localhost:7020/api/Visitor/GetVisitorsByEmployeeNumber?employeeNumber=${employeeNumber}`);
  }

  getVisitorbyTagNumber(tagNumber: string): Observable<Visitor> {
    return this.http.get<Visitor>(`https://localhost:7020/api/Visitor/GetVisitorByTagNumber?tagNumber=${tagNumber}`);
  }
}
