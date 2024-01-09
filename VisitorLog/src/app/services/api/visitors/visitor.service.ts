import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class VisitorService {
  private apiUrl = 'https://localhost:7020/api/Visitor'

  constructor(private http: HttpClient) { }

  saveVisitorDetails(visitorData: any): Observable<any> {
    console.log('Saving visitor details', visitorData);
    return this.http.post(this.apiUrl, visitorData).pipe(
      catchError((error: any) => {
        console.error('Error saving visitor details', error);
        throw error;
      })
    );
  }

  getEmployees(): Observable<any> {
    return this.http.get('https://localhost:7020/api/Employee');
  }
}
