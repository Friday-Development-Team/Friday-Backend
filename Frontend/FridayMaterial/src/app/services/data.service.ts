import { HttpClient } from '@angular/common/http';
import { error } from '@angular/compiler/src/util';
import { Injectable } from '@angular/core';
import { BehaviorSubject, interval, Observable, Subject } from 'rxjs';
import { environment } from 'src/environments/environment';
import { CateringOrder, Configuration, Item, OrderDTO } from '../models/models';

@Injectable({
  providedIn: 'root'
})
export class DataService {

  private orderPolling: Subject<CateringOrder[]> = new BehaviorSubject([])
  private pollingClock: Observable<any> = interval(60000)
  private configPolling: Observable<any> = interval(60000)

  private config: BehaviorSubject<Configuration> = new BehaviorSubject<Configuration>(new Configuration(false, false, false))

  constructor(private http: HttpClient) {
    this.loadConfig()
    this.configPolling.subscribe(s => this.loadConfig())
  }


  loadConfig(): void {
    this._getConfig().subscribe(s => this.config.next(s))
  }

  getOrderObservable(): Observable<CateringOrder[]> {
    return this.orderPolling
  }

  getAllItems(): Observable<Item[]> {
    return this.http.get<Item[]>(`${environment.apiUrl}/item/`)
  }

  addOrder(dto: OrderDTO): Observable<number> {
    console.log(dto)
    return this.http.post<number>(`${environment.apiUrl}/order/`, dto)
  }

  getRunningOrdersUser(): Observable<CateringOrder[]> {
    return this.http.get<CateringOrder[]>(`${environment.apiUrl}/order/running`)
  }

  startOrderPolling(): void {
    this.getRunningOrdersUser().subscribe(t => this.orderPolling.next(t))
    this.pollingClock.subscribe(s => this.getRunningOrdersUser().subscribe(t => this.orderPolling.next(t)))
  }

  getConfigChanges(): Observable<Configuration> {
    return this.config
  }

  getConfig(): Configuration {
    return this.config.getValue()
  }

  _getConfig(): Observable<Configuration> {
    return this.http.get<Configuration>(`${environment.apiUrl}/configuration`)
  }

  setConfig(config: Configuration): Observable<boolean> {
    return this.http.put<boolean>(`${environment.apiUrl}/configuration`, config)
  }

}
