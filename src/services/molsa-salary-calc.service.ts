import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Employee } from '../models/employee';
import { SalaryDetails } from '../models/salaryDetails';

@Injectable({
  providedIn: 'root'
})
export class MolsaSalaryCalcService {

   private baseUrl = 'https://localhost:44317/api/SalaryCalc'; 
 

  constructor(private http: HttpClient) { }

  // call my web API
    getUpdatedSalary(employee: any): Observable<SalaryDetails> {
        return this.http.post<SalaryDetails>(this.baseUrl, employee);
  }
}
