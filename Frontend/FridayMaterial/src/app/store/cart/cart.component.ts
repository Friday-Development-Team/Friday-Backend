import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Observable } from 'rxjs';

import { Cart, OrderItem } from 'src/app/models/models';

@Component({
  selector: 'friday-cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.scss']
})
/**
 * Display of Items in cart
 */
export class CartComponent implements OnInit {
  @Input() cart: Observable<Cart>
  items: OrderItem[]
  displayedColumns: string[] = ['name', 'amount', 'cost'];
  @Output() onDelete: EventEmitter<number> = new EventEmitter()


  constructor() { }

  ngOnInit(): void {
    this.cart.subscribe(s => this.items = s.items)
  }

  delete(item: OrderItem) {
    this.onDelete.emit(item.item.id)
  }

}
