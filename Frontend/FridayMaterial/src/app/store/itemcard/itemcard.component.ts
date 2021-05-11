import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Item } from 'src/app/models/models';

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
  form: FormGroup

  constructor(builder: FormBuilder) {
    this.form = builder.group({
      amount: builder.control('1', Validators.pattern("[1-9]\\d*")) // Non zero leading int
    })

  }

  ngOnInit(): void {
  }

  getUrl(){
    return `assets/${this.item.normalizedImageName}.jpg`
  }

  AddToCart(){
    console.log("Submitting");
  }

}
