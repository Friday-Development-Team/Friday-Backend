import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { Item, OrderItem } from 'src/app/models/models';

@Component({
  selector: 'friday-itembox',
  templateUrl: './itembox.component.html',
  styleUrls: ['./itembox.component.scss']
})
export class ItemboxComponent implements OnInit {

  @Input() item: Item
  @Output() onAdd: EventEmitter<{ item: Item, amount: number }> = new EventEmitter()

  constructor() { }

  ngOnInit() {
  }

}
