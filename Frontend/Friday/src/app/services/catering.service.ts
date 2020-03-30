import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { CateringOrder } from '../models/models';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class CateringService {

  constructor(private http: HttpClient) { }

  getCateringOrders(isKitchen: boolean): Observable<CateringOrder[]> {
    return this.http.get<CateringOrder[]>(`${environment.apiUrl}/order/catering`,
      {
        params: { isKitchen: `${isKitchen}` }
      })
  }
}