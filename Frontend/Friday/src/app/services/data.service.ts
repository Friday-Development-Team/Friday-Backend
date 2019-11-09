import { Injectable } from '@angular/core';

import { Observable, of, Subject, timer } from 'rxjs';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { OrderDTO } from './cart.service';
import { RefreshService } from './refresh.service';
import { Item } from '../models/models';


@Injectable({
  providedIn: 'root'
})
export class DataService {

  constructor(private http: HttpClient, private refresh: RefreshService) { }

  getAllItems(): Observable<Item[]> {
    return this.http.get<Item[]>(`${environment.apiUrl}/item/`)
  }

  placeOrder(dto: OrderDTO): Observable<any> {
    const headers = new HttpHeaders().set('content-type', 'application/json')
    this.refresh.trigger(5000)
    return this.http.post(`${environment.apiUrl}/order/`, dto, { headers })
  }

}
