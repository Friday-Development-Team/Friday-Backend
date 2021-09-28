import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core'
import { FormBuilder, FormGroup, Validators } from '@angular/forms'
import { Item, OrderItem } from 'src/app/models/models'

@Component({
  selector: 'friday-itemcard',
  templateUrl: './itemcard.component.html',
  styleUrls: ['./itemcard.component.scss']
})
/**
 * Component for single Item
 */
export class ItemcardComponent implements OnInit {

  @Input() item: Item
  @Output() onAdd: EventEmitter<OrderItem> = new EventEmitter()
  form: FormGroup

  constructor(builder: FormBuilder) {
    this.form = builder.group({
      amount: builder.control('1', Validators.pattern('[1-9]\\d*')) // Non zero leading int
    })

  }

  ngOnInit(): void {
  }

  AddToCart(): void {
    if (this.form.invalid) return
    const amount = this.form.get('amount').value
    if (amount <= 0) return
    this.onAdd.emit(new OrderItem(this.item, amount))
  }

}
