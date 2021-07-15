import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ToolService {

  constructor(private http: HttpClient) { }

  addUser(username: string, password: string): Observable<any> {
    throw new Error('Method not implemented.');
  }

  adjustUserBalance(amount: number): Observable<boolean> {
    throw new Error('Method not implemented.');
  }

  changePass(pass: string): Observable<any> {
    throw new Error('Method not implemented.');
  }

}
