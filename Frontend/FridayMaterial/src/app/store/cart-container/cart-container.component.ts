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

  constructor(private data: DataService, private cartService: CartService) {
    this.cart = this.cartService.onCartChange()
  }

  ngOnInit(): void {
  }

  sendOrder() {
    console.log("Sending ...")
  }

  clear() {
    console.log("Clearing ...")
  }

}
