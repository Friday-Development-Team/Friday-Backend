import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Item, OrderDTO } from '../models/models';

@Injectable({
  providedIn: 'root'
})
export class DataService {

  constructor(private http: HttpClient) { }

  getAllItems(): Observable<Item[]> {
    return this.http.get<Item[]>(`${environment.apiUrl}/item/`)
  }

  addOrder(dto: OrderDTO): Observable<number>{
    console.log(dto);
    return this.http.post<number>(`${environment.apiUrl}/order/`, dto)
  }

}
