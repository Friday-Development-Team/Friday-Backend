import { Component, OnInit } from '@angular/core';
import { PageEvent } from '@angular/material/paginator';
import { Item, OrderItem } from 'src/app/models/models';
import { CartService } from 'src/app/services/cart.service';
import { DataService } from 'src/app/services/data.service';

@Component({
  selector: 'friday-showcase',
  templateUrl: './showcase.component.html',
  styleUrls: ['./showcase.component.scss']
})
/**
 * Contains the list of Items to be showcased
 */
export class ShowcaseComponent implements OnInit {

  items: Item[] = []
  lowVal: number = 0
  highVal: number = 10

  constructor(private data: DataService, private cart: CartService) {
    this.data.getAllItems().subscribe(s=> this.items=s)
   }

  ngOnInit(): void {
  }

  getPaginatorData(event: PageEvent): PageEvent{
    this.lowVal=event.pageIndex * event.pageSize
    this.highVal=Math.min(this.lowVal + event.pageSize, this.items.length)
    return event
  }

  addToCart(orderItem: OrderItem){
    this.cart.addItem(orderItem)
  }

}
