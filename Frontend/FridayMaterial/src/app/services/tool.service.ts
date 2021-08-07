import { HttpClient } from '@angular/common/http'
import { Injectable } from '@angular/core'
import { Observable } from 'rxjs'
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
    return this.http.put('user/updatebalance', {name, amount})
  }

  changePass(name: string, pass: string): Observable<any> {
    throw new Error('Method not implemented.')
  }

}
