import { Injectable } from '@angular/core';
import { BehaviorSubject, Subject } from 'rxjs';
import { PriceFilter } from '../models/pricefilter';

@Injectable({
  providedIn: 'root'
})
export class FilterService {

  filter: BehaviorSubject<PriceFilter> = new BehaviorSubject(new PriceFilter('none', 0))
  search: Subject<string> = new Subject()

  changePriceFilter(filter: PriceFilter) {
    this.filter.next(filter)
  }

  searchName(name: string) {
    this.search.next(name)
  }
}
