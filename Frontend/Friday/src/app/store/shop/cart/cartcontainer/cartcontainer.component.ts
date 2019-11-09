import { Component, OnInit } from '@angular/core';
import { Cart } from 'src/app/models/models';
import { CartService } from 'src/app/services/cart.service';

@Component({
  selector: 'friday-cartcontainer',
  templateUrl: './cartcontainer.component.html',
  styleUrls: ['./cartcontainer.component.scss']
})
export class CartcontainerComponent implements OnInit {

  cart: Cart

  constructor(private cartServ: CartService) {
    this.cartServ.cartItems.subscribe(s => this.cart = s)
  }

  ngOnInit() {
  }

  deleteItem(id: number) {
    this.cartServ.removeFromCart(id)
  }

  placeOrder() {
    this.cartServ.placeOrder()
  }

  clearCart() {
    this.cartServ.flushCart()
  }
}
