import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'friday-store-base',
  templateUrl: './store-base.component.html',
  styleUrls: ['./store-base.component.scss']
})
/**
 * Container component for all Store elements, such as Shop or History
 */
export class StoreBaseComponent implements OnInit {

  constructor() { }

  ngOnInit(): void {
  }

}
