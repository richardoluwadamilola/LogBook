import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, tap } from 'rxjs';
import { Department } from '../models/department.model';

@Injectable({
  providedIn: 'root'
})
export class DepartmentService {
  private apiUrl = 'https://localhost:7020/api/Department'

  constructor(private http: HttpClient) { }

  getDepartments(): any {
    return this.http.get<Department[]>(this.apiUrl);
  }

  saveDepartment(departmentData: any): any {
    return this.http.post(this.apiUrl, departmentData)
      .pipe(
        tap((response: any) => console.log('Save Department Response:', response)),
        catchError((error: any) => {
          console.error('Error saving department details', error);
          throw error;
        })
      );
  }

  updateDepartmentDetails(departmentId: number, departmentData: any): any {
    return this.http.put(`${this.apiUrl}/${departmentId}`, departmentData)
      .pipe(
        tap((response: any) => console.log('Update Department Response:', response)),
        catchError((error: any) => {
          console.error('Error updating department details', error);
          throw error;
        })
      );
  }  

  deleteDepartment(departmentId: number): any {
    return this.http.delete(`${this.apiUrl}/${departmentId}`)
      .pipe(
        tap((response: any) => console.log('Delete Department Response:', response)),
        catchError((error: any) => {
          console.error('Error deleting department', error);
          throw error;
        })
      );
  }
  
}
