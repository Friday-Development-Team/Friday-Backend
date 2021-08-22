import { HttpClient } from '@angular/common/http'
import { Injectable } from '@angular/core'
import { Observable } from 'rxjs'
import { map } from 'rxjs/operators'
import { ItemAmount, Log } from '../models/models'
import { HttpService } from './http.service'

@Injectable({
  providedIn: 'root'
})
export class ToolService {

  constructor(private http: HttpService) { }

  addUser(username: string, password: string): Observable<any> {
    return this.http.post('user/register', { username, password })
  }

  adjustUserBalance(name: string, amount: number): Observable<boolean> {
    return this.http.put('user/updatebalance', { name, amount })
  }

  // changePass(name: string, pass: string): Observable<any> {
  //   throw new Error('Method not implemented.')
  // }

  getLogs(route: string, params?: any): Observable<Log[]> {
    return this.http.get<Log[]>(`logs/${route}`, { params })
      .pipe(
        map(s => s.map(t => {
          const temp = t
          temp.type = temp.type.toLowerCase()
          return temp
        })))
  }

  getItemAmounts(isSoldOnly: boolean): Observable<ItemAmount[]> {
    return this.http.get<ItemAmount[]>(`/${'logs/stock/' + (isSoldOnly ? 'sold' : 'remaining')}`)
  }

  getTotalIncome(): Observable<number> {
    return this.http.get<number>(`logs/total`)
  }

}
