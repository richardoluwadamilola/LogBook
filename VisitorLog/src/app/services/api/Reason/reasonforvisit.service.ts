import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ReasonForVisit } from '../models/reason-for-visit';

@Injectable({
  providedIn: 'root'
})
export class ReasonforvisitService {
  private apiUrl = 'https://localhost:7020/api/ReasonForVisit';

  constructor(private http: HttpClient) { }

  getReasonsForVisit(): Observable<any> {
    return this.http.get<ReasonForVisit[]>(`${this.apiUrl}`);
  }

  createReasonForVisit(reason: any): Observable<any> {
    return this.http.post(`${this.apiUrl}/create`, reason);
  }
}
