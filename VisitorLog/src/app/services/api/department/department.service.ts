import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, tap } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class DepartmentService {
  private apiUrl = 'https://localhost:7020/api/Department'

  constructor(private http: HttpClient) { }

  getDepartments(): any {
    return this.http.get(this.apiUrl);
  }

  saveDepartmentDetails(departmentData: any): any {
    return this.http.post(this.apiUrl, departmentData)
      .pipe(
        tap((response: any) => console.log('Save Department Response:', response)),
        catchError((error: any) => {
          console.error('Error saving department details', error);
          throw error;
        })
      );
  }
  

  
}
