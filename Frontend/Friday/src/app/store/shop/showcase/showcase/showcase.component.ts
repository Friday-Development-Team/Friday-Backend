import { Component, OnInit } from '@angular/core';
import { CartService } from 'src/app/services/cart.service';
import { Item } from 'src/app/models/models';

@Component({
  selector: 'friday-showcase',
  templateUrl: './showcase.component.html',
  styleUrls: ['./showcase.component.scss']
})
export class ShowcaseComponent implements OnInit {

  constructor(private cart: CartService) { }

  ngOnInit() {
  }

  addItemToCart(event: { item: Item, amount: number }) {
    this.cart.addToCart(event.item, event.amount)
  }
  

}
