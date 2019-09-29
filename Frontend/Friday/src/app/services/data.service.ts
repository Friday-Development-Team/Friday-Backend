import { Injectable } from '@angular/core';

import { Observable, of } from 'rxjs';
import { Item } from '../models/models';
import { map } from 'rxjs/operators';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';


@Injectable({
  providedIn: 'root'
})
export class DataService {

  constructor(private http: HttpClient) { }

  getAllItems(): Observable<Item[]> {
    return this.http.get<Item[]>(`${environment.apiUrl}/item/`)
  }

}
