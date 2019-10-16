import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { PriceFilter } from '../models/pricefilter';

@Injectable({
  providedIn: 'root'
})
export class FilterService {

  filter: BehaviorSubject<PriceFilter> = new BehaviorSubject(new PriceFilter('none', 0))

  changePriceFilter(filter: PriceFilter) {
    this.filter.next(filter)
  }
}
