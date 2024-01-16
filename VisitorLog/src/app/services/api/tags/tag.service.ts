import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, catchError } from 'rxjs';

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
}

