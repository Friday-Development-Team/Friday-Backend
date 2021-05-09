import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { Cart, Item, OrderItem } from "../models/models";

@Injectable({
  providedIn: 'root'
})
export class CartService {

  cart: Cart
  private cartChanges: BehaviorSubject<Cart>

  constructor() {
    this.cart = new Cart()
    this.cart.add(new OrderItem(new Item(1, "Test", 2, "Food", 50, null, null, ""), 3))
    this.cartChanges = new BehaviorSubject<Cart>(this.cart)
  }

  addItem(item: Item, amount: number) {
    const orderItem: OrderItem = new OrderItem(item, amount)
    this.cart.add(orderItem)
    this.update()
  }

  update() {
    this.cartChanges.next(this.cart)
  }

  onCartChange(): Observable<Cart>{
    return this.cartChanges
  }
}
