import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, interval, Observable, Subject } from 'rxjs';
import { environment } from 'src/environments/environment';
import { CateringOrder, Item, OrderDTO } from '../models/models';

@Injectable({
  providedIn: 'root'
})
export class DataService {

  private orderPolling: Subject<CateringOrder[]> = new BehaviorSubject([])
  private pollingClock: Observable<any> = interval(60000)

  constructor(private http: HttpClient) { }

  getOrderObservable(): Observable<CateringOrder[]> {
    return this.orderPolling
  }

  getAllItems(): Observable<Item[]> {
    return this.http.get<Item[]>(`${environment.apiUrl}/item/`)
  }

  addOrder(dto: OrderDTO): Observable<number> {
    console.log(dto);
    return this.http.post<number>(`${environment.apiUrl}/order/`, dto)
  }

  getRunningOrdersUser(): Observable<CateringOrder[]> {
    return this.http.get<CateringOrder[]>(`${environment.apiUrl}/order/running`)
  }

  startOrderPolling() {
    this.getRunningOrdersUser().subscribe(t => this.orderPolling.next(t))
    this.pollingClock.subscribe(s => this.getRunningOrdersUser().subscribe(t => this.orderPolling.next(t)))
  }

}
