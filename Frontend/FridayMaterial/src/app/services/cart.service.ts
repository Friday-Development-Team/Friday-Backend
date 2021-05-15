import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { Cart, Item, OrderDTO, OrderItem, OrderItemDTO } from "../models/models";
import { DataService } from './data.service';

@Injectable({
  providedIn: 'root'
})
export class CartService {

  cart: Cart
  private cartChanges: BehaviorSubject<Cart>

  constructor(private data: DataService) {
    this.cart = new Cart()
    // this.cart.add(new OrderItem(new Item(1, "Test", 2, "Food", 50, null, null, ""), 3))
    this.cartChanges = new BehaviorSubject<Cart>(this.cart)
  }

  addItem(orderItem: OrderItem) {
    this.cart.add(orderItem)
    this.update()
  }

  update() {
    this.cartChanges.next(this.cart)
  }

  onCartChange(): Observable<Cart>{
    return this.cartChanges
  }

  placeOrder(){
    let dto= new OrderDTO(this.cart.items.map(s=> new OrderItemDTO(s.item.id, s.amount)))
    this.data.addOrder(dto).subscribe(s=> console.log(s))
  }
}
