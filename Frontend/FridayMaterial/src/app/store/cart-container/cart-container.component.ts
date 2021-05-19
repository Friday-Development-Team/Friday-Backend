import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { Cart } from 'src/app/models/models';
import { CartService } from 'src/app/services/cart.service';
import { DataService } from 'src/app/services/data.service';

@Component({
  selector: 'friday-cart-container',
  templateUrl: './cart-container.component.html',
  styleUrls: ['./cart-container.component.scss']
})
/**
 * Smart component for everything involving cart
 */
export class CartContainerComponent implements OnInit {

  cart: Observable<Cart>
  totalItems: number = 0

  constructor(private data: DataService, private cartService: CartService) {
    this.cart = this.cartService.onCartChange()
    this.cart.subscribe(s => this.totalItems = s.items.map(t => t.amount).reduce((acc, curr) => +acc + +curr, 0))
  }

  ngOnInit(): void {
  }

  sendOrder() {
    this.cartService.placeOrder()
  }

  clear() {
    this.cartService.clearCart()
  }

  lowerCount(id: number) {
    this.cartService.lowerCount(id)
  }

}
