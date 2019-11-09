import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { Item } from 'src/app/models/models';
import { FormGroup, FormBuilder } from '@angular/forms';

@Component({
  selector: 'friday-itembox',
  templateUrl: './itembox.component.html',
  styleUrls: ['./itembox.component.scss']
})
export class ItemboxComponent implements OnInit {

  @Input() item: Item
  @Output() onAdd: EventEmitter<{ item: Item, amount: number }> = new EventEmitter()
  form: FormGroup

  constructor(builder: FormBuilder) {
    this.form = builder.group({
      amount: builder.control('1')
    })
  }

  ngOnInit() {
  }

  getURL() {
    return `assets/${this.item.name.split(" ").join("_").toLowerCase()}.jpg`
  }

  onAddToCart() {
    if (this.form.invalid)
      return
    var amount = this.form.get('amount').value
    this.onAdd.emit({ item: this.item, amount: amount })
  }

}
