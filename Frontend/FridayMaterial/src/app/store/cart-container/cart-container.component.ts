import { Component, OnInit } from '@angular/core';
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

  

  constructor(private data: DataService) { }

  ngOnInit(): void {
  }

  sendOrder(){
    console.log("Sending ...")
  }

  clear(){
    console.log("Clearing ...")
  }

}
