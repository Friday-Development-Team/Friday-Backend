import { Component, OnInit } from '@angular/core';
import { Item } from 'src/app/models/models';
import { AdminService } from 'src/app/services/admin.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'friday-additem',
  templateUrl: './additem.component.html',
  styleUrls: ['./additem.component.scss']
})
export class AdditemComponent implements OnInit {

  item: Item
  form: FormGroup

  items: string[]

  constructor(private admin: AdminService, fb: FormBuilder) {
    const decimal = '[0-9]+(\.?[0-9]+)?'
    const nodecimal = '[1-9]?[0-9]+'
    this.form = fb.group({
      name: fb.control('', [Validators.required]),
      price: fb.control('', [Validators.required, Validators.pattern(decimal)]),
      type: fb.control('', Validators.required),
      count: fb.control('', [Validators.required, Validators.pattern(nodecimal)]),
      image: fb.control('', [Validators.required, Validators.pattern('[a-z0-9]+(_[a-z0-9]+)*')]),
      details: fb.group({
        size: fb.control('', [Validators.required]),
        calories: fb.control('', [Validators.required, Validators.pattern(nodecimal)]),
        sugarcontent: fb.control('', [Validators.required, Validators.pattern(decimal)]),
        saltcontent: fb.control('', [Validators.required, Validators.pattern(decimal)]),
        allergens: fb.control('')
      })
    })
  }

  ngOnInit() {
  }



}
