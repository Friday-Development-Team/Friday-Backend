import { Component, OnInit } from '@angular/core';
import { CartService } from 'src/app/services/cart.service';
import { Item } from 'src/app/models/models';
import { DataService } from 'src/app/services/data.service';
import { Observable } from 'rxjs';
import { PriceFilter } from 'src/app/models/pricefilter';
import { FilterService } from 'src/app/services/filter.service';
import { RefreshService } from 'src/app/services/refresh.service';
import { map } from 'rxjs/operators';

@Component({
  selector: 'friday-showcase',
  templateUrl: './showcase.component.html',
  styleUrls: ['./showcase.component.scss']
})
export class ShowcaseComponent implements OnInit {

  items: Observable<Item[]>
  priceFilter: Observable<PriceFilter>
  searchName: Observable<string>

  constructor(private cart: CartService, private data: DataService, private filter: FilterService, private refresh: RefreshService) {
    this.items = this.data.getAllItems().pipe(map(s => s.sort(t => t.price)))
    this.priceFilter = this.filter.filter
    this.searchName = this.filter.search
    this.filter.search.subscribe(s => console.log(s))

    //Triggers a refresh of items whenever a value is pushed
    this.refresh.refresh.subscribe(s => this.items = this.data.getAllItems())
  }

  ngOnInit() {
  }

  addItemToCart(event: { item: Item, amount: number }) {
    console.log(event)
    this.cart.addToCart(event.item, event.amount)
  }


}
