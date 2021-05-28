import { Injectable } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { BehaviorSubject, Observable } from 'rxjs';
import { Cart, Item, OrderDTO, OrderItem, OrderItemDTO } from "../models/models";
import { DataService } from './data.service';
import { SpinnerService } from './spinner.service';

@Injectable({
  providedIn: 'root'
})
export class CartService {

  cart: Cart
  private cartChanges: BehaviorSubject<Cart>

  constructor(private data: DataService, private spinner: SpinnerService) {
    this.cart = new Cart()
    this.cartChanges = new BehaviorSubject<Cart>(this.cart)
  }

  addItem(orderItem: OrderItem) {
    this.cart.add(orderItem)
    this._update()
  }

  _update() {
    this.cartChanges.next(this.cart)
  }

  onCartChange(): Observable<Cart> {
    return this.cartChanges
  }

  placeOrder() {
    this.startSpinner()
    let dto = new OrderDTO(this.cart.items.map(s => new OrderItemDTO(s.item.id, s.amount)))
    this.data.addOrder(dto).subscribe(s => {
      this.clearCart()
      this.stopSpinner()
    })
  }

  startSpinner() {
    this.spinner.startSpinner()
  }

  stopSpinner() {
    this.spinner.stopSpinner(3000)
  }

  clearCart() {
    this.cart.clear()
    this._update()
  }

  lowerCount(id: number) {
    this.cart.remove(id, 1)
    this._update()
  }
}
