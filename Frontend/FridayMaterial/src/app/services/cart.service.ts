import { Injectable } from '@angular/core'
import { MatDialog } from '@angular/material/dialog'
import { BehaviorSubject, Observable } from 'rxjs'
import { Cart, Item, OrderDTO, OrderItem, OrderItemDTO } from '../models/models'
import { DataService } from './data.service'
import { SpinnerService } from './spinner.service'

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

  addItem(orderItem: OrderItem): void {
    this.cart.add(orderItem)
    this._update()
  }

  _update(): void {
    this.cartChanges.next(this.cart)
  }

  onCartChange(): Observable<Cart> {
    return this.cartChanges
  }

  placeOrder(): Observable<number> {
    this.startSpinner()
    const dto = new OrderDTO(this.cart.items.map(s => new OrderItemDTO(s.item.id, s.amount)))
    return this.data.addOrder(dto).pipe(
      s => {
        this.clearCart()
        this.stopSpinner()
        return s
      })
  }

  startSpinner(): void {
    this.spinner.startSpinner()
  }

  stopSpinner(): void {
    this.spinner.stopSpinner(3000)
  }

  clearCart(): void {
    this.cart.clear()
    this._update()
  }

  lowerCount(id: number): void {
    this.cart.remove(id, 1)
    this._update()
  }
}
