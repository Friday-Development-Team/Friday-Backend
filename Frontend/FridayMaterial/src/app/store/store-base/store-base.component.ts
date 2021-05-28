import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { CartService } from 'src/app/services/cart.service';
import { DialogComponent } from '../dialog/dialog.component';

@Component({
  selector: 'friday-store-base',
  templateUrl: './store-base.component.html',
  styleUrls: ['./store-base.component.scss']
})
/**
 * Container component for all Store elements, such as Shop or History
 */
export class StoreBaseComponent implements OnInit {

  constructor() {
  }

  ngOnInit(): void {
  }

}