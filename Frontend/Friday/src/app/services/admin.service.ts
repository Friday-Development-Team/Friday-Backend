import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ShopUser } from './user.service';
import { environment } from 'src/environments/environment';
import { Log } from '../models/models';

@Injectable({
  providedIn: 'root'
})
export class AdminService {

  constructor(private http: HttpClient) { }

  getAllUsers(): Observable<ShopUser[]> {
    return this.http.get<ShopUser[]>(`${environment.apiUrl}/user/all`)
  }

  getLogs(route: string, param?: number | string): Observable<Log[]> {
    return null
  }

  getItemAmounts(isSoldOnly: boolean): Observable<any> {
    return null
  }

  getTotalIncome(): Observable<number> {
    return null
  }
}
