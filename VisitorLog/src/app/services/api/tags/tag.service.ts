import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class TagService {
  private apiUrl = 'https://localhost:7020/api/Tag';

  constructor(private http: HttpClient) {} // Inject HttpClient here

  createTag(tag: any): Observable<any> {
    return this.http.post(`${this.apiUrl}/create`, tag).pipe(
      catchError((error: HttpErrorResponse) => {
        console.error('Error creating tag', error);
        throw error;
      })
    );
  }
  
  getTags(): Observable<any> {
    return this.http.get(`${this.apiUrl}`).pipe(
      catchError((error: HttpErrorResponse) => {
        console.error('Error fetching tags', error);
        throw error;
      })
    );
  }


  assignTagToVisitor(assignTagDto: any): Observable<any> {
    return this.http.post(`${this.apiUrl}/assign`, assignTagDto).pipe(
      catchError((error: HttpErrorResponse) => {
        console.error('Error assigning tag', error);
        return throwError(error);
      })
    );
  }

  checkOutVisitor(checkoutTagDto: any): Observable<any> {
    return this.http.put(`${this.apiUrl}/checkout`, checkoutTagDto).pipe(
      catchError((error: HttpErrorResponse) => {
        console.error('Error checking out tag', error);
        return throwError(error);
      })
    );
  }

  disableTag(tagNumber: string): Observable<any> {
    return this.http.put(`${this.apiUrl}/disable`, { tagNumber }).pipe(
      catchError((error: HttpErrorResponse) => {
        console.error('Error disabling tag', error);
        return throwError(error);
      })
    );
  }  

  
}
