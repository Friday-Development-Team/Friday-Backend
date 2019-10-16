import { Injectable } from '@angular/core';
import { DataService } from './data.service';
import { BehaviorSubject } from 'rxjs';
import { Cart, Item, OrderItem } from '../models/models';

@Injectable({
  providedIn: 'root'
})
export class CartService {

  cart: Cart = new Cart
  cartItems: BehaviorSubject<Cart> = new BehaviorSubject(new Cart())

  constructor(private data: DataService) { }

  addToCart(item: Item, amount: number) {
    var id = this.cart.items.map(s => s.item.id).indexOf(item.id)
    if (id === -1)
      this.cart.items.push(new OrderItem(item, amount))
    else
      this.cart.items[id].amount += amount

    this.cartItems.next(this.cart)
  }

  removeFromCart(item: Item) {
    var id = this.cart.items.map(s => s.item.id).indexOf(item.id)
    if (id === -1)
      return
    if (this.cart.items[id].amount === 1)
      this.cart.items.splice(id)
    else
      this.cart.items[id].amount--;

    this.cartItems.next(this.cart)
  }

  flushCart() {
    this.cart = new Cart()
  }
}

