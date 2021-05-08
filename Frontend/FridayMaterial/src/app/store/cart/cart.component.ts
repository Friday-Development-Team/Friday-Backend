import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';

import { OrderItem } from 'src/app/models/models';

@Component({
  selector: 'friday-cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.scss']
})
/**
 * Display of Items in cart
 */
export class CartComponent implements OnInit {
  @Input() items: OrderItem[]

  @Output() onDelete: EventEmitter<number>

  constructor() { }

  ngOnInit(): void {
  }

  delete(id: number) {
    this.onDelete.emit(id)
  }

}
