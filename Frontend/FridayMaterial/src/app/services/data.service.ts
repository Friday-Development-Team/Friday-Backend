import { HttpClient } from '@angular/common/http'
import { error } from '@angular/compiler/src/util'
import { Injectable } from '@angular/core'
import { BehaviorSubject, interval, Observable, Subject } from 'rxjs'
import { environment } from 'src/environments/environment'
import { CateringOrder, Configuration, Item, OrderDTO, ShopUser } from '../models/models'
import { HttpService } from './http.service'

@Injectable({
  providedIn: 'root'
})
export class DataService {

  private orderPolling: Subject<CateringOrder[]> = new BehaviorSubject([])
  private pollingClock: Observable<any> = interval(60000)
  private configPolling: Observable<any> = interval(60000)

  private config: BehaviorSubject<Configuration> = new BehaviorSubject<Configuration>(new Configuration(false, false, false))

  constructor(private http: HttpService) {
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
    return this.http.get<Item[]>('item')
  }

  addOrder(dto: OrderDTO): Observable<number> {
    return this.http.post<number>('order', dto)
  }

  getRunningOrdersUser(): Observable<CateringOrder[]> {
    return this.http.get<CateringOrder[]>('order/running')
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
    return this.http.get<Configuration>('configuration')
  }

  setConfig(config: Configuration): Observable<boolean> {
    return this.http.put<boolean>('configuration', config)
  }

  getUsers(): Observable<ShopUser[]> {
    return this.http.get<ShopUser[]>('user/all')
  }

}
