import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class VisitorService {
  private apiUrl = 'https://localhost:7020/api/Visitor'

  constructor(private http: HttpClient) { }

  saveVisitorDetails(visitorData: any): Observable<any> {
    return this.http.post(this.apiUrl, visitorData);
  }

  getEmployees(): Observable<any> {
    return this.http.get('https://localhost:7020/api/Employee');
  }
}
