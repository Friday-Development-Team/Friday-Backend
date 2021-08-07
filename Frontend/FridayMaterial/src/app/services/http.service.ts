import { HttpClient } from '@angular/common/http'
import { Injectable } from '@angular/core'
import { Observable } from 'rxjs'
import { environment } from 'src/environments/environment'

@Injectable({
  providedIn: 'root'
})
export class HttpService {

  constructor(private http: HttpClient) { }

  get<type>(path: string, params?: {}): Observable<type> {
    return this.http.get<type>(`${environment.apiUrl}/${path}`, { params })
  }

  post<type>(path: string, data: {}): Observable<type> {
    return this.http.post<type>(`${environment.apiUrl}/${path}`, data)
  }

  put<type>(path: string, data: {}): Observable<type> {
    return this.http.put<type>(`${environment.apiUrl}/${path}`, data)
  }
}
