import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { Cart, Item, OrderItem } from "../models/models";

@Injectable({
  providedIn: 'root'
})
export class CartService {

  cart: Cart
  cartChanges: BehaviorSubject<Cart>

  constructor() {
    this.cart = new Cart()
    this.cartChanges = new BehaviorSubject(this.cart)
  }

  addItem(item: Item, amount: number) {
    const orderItem: OrderItem = new OrderItem(item, amount)
    this.cart.add(orderItem)
    this.update()
  }

  update() {
    this.cartChanges.next(this.cart)
  }
}
