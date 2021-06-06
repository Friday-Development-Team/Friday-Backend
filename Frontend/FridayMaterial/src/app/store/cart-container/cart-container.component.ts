import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { Cart } from 'src/app/models/models';
import { CartService } from 'src/app/services/cart.service';
import { DataService } from 'src/app/services/data.service';
import { ErrordialogComponent } from '../errordialog/errordialog.component';

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

  constructor(private data: DataService, private cartService: CartService, private router: Router, private dialog: MatDialog) {
    this.cart = this.cartService.onCartChange()
    this.cart.subscribe(s => this.totalItems = s.items.map(t => t.amount).reduce((acc, curr) => +acc + +curr, 0))
  }

  ngOnInit(): void {
  }

  sendOrder() {
    this.cartService.placeOrder().subscribe(
      s => this.router.navigate(["main/store/running"]),
      err => {
        this.dialog.open(ErrordialogComponent, {
          data: "Er is iets misgegaan met het plaatsen van je order. Probeer het opnieuw of vraag hulp aan de verantwoordelijke."
        })
      }
    )
  }

  clear() {
    this.cartService.clearCart()
  }

  lowerCount(id: number) {
    this.cartService.lowerCount(id)
  }

}
